using System.Drawing;
using UnityEngine;

public class player : MonoBehaviour
{
    //movimento////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public float mov;
    public float spd;
    public float jmpF;
    public bool cnJmp;
    public bool cnDash;
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
    public bool jmpPressed;

    //fisica e mouse////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private Vector3 mouse;
    private Vector2 Mdir;
    public bool grd;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        state = "idle";
    }


    void Update()
    {
        grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Mdir = new Vector2(Mira.position.x - transform.position.x, Mira.position.y - transform.position.y);
        mov = Input.GetAxisRaw("Horizontal");

        if (grd)
        {
            cnJmp = true;
            cnDash = false;
        }
        else { cnJmp = false; }

        if (Input.GetButtonDown("Jump")) { jmpPressed = true; }
    }




    void FixedUpdate()
    {
        switch (state)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "idle":
                if (mov != 0) { state = "walk"; }
                if (jmpPressed)
                {
                    if (cnJmp)
                    {
                        state = "jump";
                        cnJmp = false;
                    }
                    else if (cnDash)
                    {
                        airJumpTime = airJumpTimeMax;
                        rig.linearVelocity = Vector2.zero;
                        state = "airJump";
                    }
                    jmpPressed = false;
                }

                rig.linearVelocity = new Vector2(0, rig.linearVelocity.y);
                anim.SetInteger("transition", 1);
                break;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "walk":
                if (mov == 0) { state = "idle"; }
                if (jmpPressed)
                {
                    if (cnJmp)
                    {
                        state = "jump";
                        cnJmp = false;
                    }
                    else if (cnDash)
                    {
                        airJumpTime = airJumpTimeMax;
                        rig.linearVelocity = Vector2.zero;
                        state = "airJump";
                    }
                    jmpPressed = false;
                }

                rig.linearVelocity = new Vector2(mov * spd, rig.linearVelocity.y);

                if (rig.linearVelocity.y == 0) { anim.SetInteger("transition", 2); }
                else if (rig.linearVelocity.y > 0) { anim.SetInteger("transition", 3); }
                else { anim.SetInteger("transition", 4); }
                break;

            case "jump":
                cnDash = true;
                if (mov != 0) { state = "walk"; } else if (mov == 0) { state = "idle"; }
                if (jmpPressed && cnDash)
                {
                    airJumpTime = airJumpTimeMax;
                    rig.linearVelocity = Vector2.zero;
                    state = "airJump";
                    jmpPressed = false;
                }

                rig.linearVelocity = new Vector2(mov * spd, jmpF);

                break;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "airJump":
                airJumpTime -= Time.deltaTime;

                Vector2 dash = Mdir.normalized * airJmpMovM;
                rig.linearVelocity = new Vector2(dash.x, dash.y);


                if (airJumpTime <= 0)
                {
                    if (mov == 0) { state = "idle"; } else { state = "walk"; }
                    cnDash = false;
                }

                break;

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            case "knockback":
                knkTimer -= Time.deltaTime;

                if (knkTimer <= 0)
                {
                    if (mov == 0) { state = "idle"; } else { state = "walk"; }
                    if (jmpPressed)
                    {
                        if (cnJmp)
                        {
                            state = "jump";
                            cnJmp = false;
                        }
                        else if (cnDash)
                        {
                            airJumpTime = airJumpTimeMax;
                            rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                            state = "airJump";
                        }
                        jmpPressed = false;
                    }
                }
                break;

                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        }

        if (Input.mousePosition.x > transform.position.x) { anim.SetBool("isR", true); }
        else if (Input.mousePosition.x < transform.position.x) { anim.SetBool("isR", false); }
    }
}