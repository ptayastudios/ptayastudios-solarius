using System.Drawing;
using UnityEngine;

public class player : MonoBehaviour
{
    //movimento////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public float mov;
    public float spd;
    public float jmpF;
    public int jmps;
    public float airJmpMovM;
    public float airJumpTime;
    public float airJumpTimeMax;


    //objetos////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public LayerMask gl;
    public Transform groundCheck;

    //relacionados a habilidades////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public GameObject habilityManager;
    public string[] habilities = new string[16];
    public int aHability = 1;
    public int[] slots = new int[5];
    public Transform Mira;

     //componentes////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sr;

    //dano////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public float knkTimer;
    public float knkTimerMax;

    //state machine////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string state;

    //fisica e mouse////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private Vector3 mouse;
    private Vector2 Mdir;
    public bool grd;

    void Start(){
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }


    void Update()
    {
        grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Mdir = new Vector2(Mira.position.x - transform.position.x, Mira.position.y - transform.position.y);
        mov = Input.GetAxisRaw("Horizontal");

        if (grd) { jmps = 2; }
        if(!grd && jmps == 2){ jmps--; }
    }



    
    void FixedUpdate()
    {
        switch (state)
        {
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "idle":
                if(mov != 0){ state = "walk"; }
                if(Input.GetButtonDown("Jump")){
                    if (jmps == 2){
                        state = "jump";
                        jmps--;
                    }else if(jmps == 1){
                        rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                        jmps--;
                        state = "airJump";
                    }
                }

                rig.linearVelocity = new Vector2(0, rig.linearVelocity.y);
                anim.SetInteger("transition", 1);
            break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "walk":
                if(mov == 0){ state = "idle"; }
                if(Input.GetButtonDown("Jump")){
                    if (jmps == 2){
                        state = "jump";
                        jmps--;
                    }else if(jmps == 1){
                        rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                        jmps--;
                        state = "airJump";
                    }
                }

                rig.linearVelocity = new Vector2(mov * spd, rig.linearVelocity.y);
                
                if (rig.linearVelocity.y == 0) { anim.SetInteger("transition", 2); }
                else if (rig.linearVelocity.y > 0) { anim.SetInteger("transition", 3); }
                else { anim.SetInteger("transition", 4); }
            break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "jump":
                if(mov != 0){ state = "walk"; }else if(mov == 0){ state = "idle"; }
                if(Input.GetButtonDown("Jump") && jmps == 1){
                    airJumpTime = airJumpTimeMax;
                    state = "airJump";
                }

                rig.linearVelocity = new Vector2(mov * spd, jmpF);
                
            break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "airJump":
                airJumpTime -= Time.deltaTime;

                Vector2 dash = Mdir.normalized * airJmpMovM;
                rig.linearVelocity = new Vector2(dash.x, dash.y);


                if (airJumpTime <= 0){
                    if(mov == 0){ state = "idle"; }else{ state = "walk"; }
                }

            break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "knockback":
                knkTimer -= Time.deltaTime;
                
                if (knkTimer <= 0)
                {
                    if (mov == 0) { state = "idle"; } else { state = "walk"; }
                    if (Input.GetButtonDown("Jump")){
                        if (jmps == 2){
                            state = "jump";
                            jmps--;
                        }
                        else if (jmps == 1){
                            rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                            jmps--;
                            state = "airJump";
                        }
                    }
                }
            break;

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        if (mouse.x > transform.position.x) { anim.SetBool("isR", true); }
        else if(mouse.x < transform.position.x){ anim.SetBool("isR", false); }
    }
}