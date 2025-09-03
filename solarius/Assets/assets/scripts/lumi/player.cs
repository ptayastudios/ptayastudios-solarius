using System.Drawing;
using UnityEngine;

public class player : MonoBehaviour
{
    //movimento
    public float mov;
    public float spd;
    public float maxSpd;
    public float jmpF;
    public int jmps;
    public bool slide;
    public float sldF;
    public bool ground;
    public int dir;
    public float jmpM;
    public float jmpMf;
    public bool airJmp;
    public float airJmpMovM;
    public float airJumpTime;
    public float airJumpTimeMax;
    public string state;
    public bool grd;



    //objetos
    public LayerMask gl;
    public Transform groundCheck;



    //relacionados a habilidades
    public string clctdHability;
    public GameObject habilityManager;

   
    public string[] habilities = new string[16];
    public int aHability = 1;
    public int[] slots = new int[5];
    public int aSlot;
    public int fire;

    public string hAdd;
    public Transform Mira;




     //componentes
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sr;





    //dano
    public float knkTimer;
    public float knkTimerMax;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }


    void Update(){

        if (state != "knockback"){
            
            if (state != "airJmp"){
            mov = Input.GetAxisRaw("Horizontal");
            }

            if (mov == 0){
                state = "idle";
            }
            else { state = "walk"; }

            if (Input.GetButtonDown("Jump") && jmps > 0)
            {
                state = "jump";
                Debug.Log("imput enviado");
            }

            if (airJmp)
            {
                state = "airjump";
                airJumpTime = airJumpTimeMax;
            }   
        
        }
        
        if(Input.GetButtonDown("Jump")){ Debug.Log("imput recebido"); }




        if (airJumpTime <= 0.1)
        {
            state = "airjump";
            
        }








        if (grd) { jmps = 2; }

        if (airJmp && jmpM < 1)
        {
            jmpM += jmpMf;
        }

        if (jmpM >= 1)
        {
            airJmp = false;
            jmpM = 0;
        }
    
    }



    
    void FixedUpdate()
    {
        /*a state machine ta com problema em iniiar o state de pulo, a unity recebe o imput e tenta abrir o state, mas por algum
        motivo ele nao responde, provavelmente pq tem alguma sobreposição que impede a troca de estados*/



        if(mov == 0 && state != "airJmp" && state != "knockback")
        { rig.linearVelocity = new Vector2(0, rig.linearVelocity.y); }



        grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 Mdir = new Vector2(Mira.position.x - transform.position.x, Mira.position.y - transform.position.y);

        switch (state)
        {
            case "idle":
                anim.SetInteger("transition", 1);
                break;




            case "walk":
                anim.SetInteger("transition", 2);
                rig.linearVelocity = new Vector2(mov * spd, rig.linearVelocity.y);
                break;




            case "jump":
                Debug.Log("imput usado");
                rig.linearVelocity = new Vector2(mov * spd, rig.linearVelocity.y);
                if (jmps == 2)
                {
                    rig.linearVelocity = new Vector2(rig.linearVelocity.x, jmpF);
                }
                else
                {
                    rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                    jmps--;
                    airJmp = true;
                }



                if (rig.linearVelocity.y > 0)
                {
                    anim.SetInteger("transition", 3);
                }
                else
                {
                    anim.SetInteger("transition", 4);
                }
                break;




            case "airjump":
                airJumpTime -= Time.deltaTime;

                Vector2 dash = Mdir.normalized * airJmpMovM;
                rig.linearVelocity = new Vector2(dash.x, dash.y);


                if(airJumpTime <= 0){ state = "idle"; }
                break;




            case "knockback":
                knkTimer -= Time.deltaTime;

                if (knkTimer <= 0){ state = "idle"; }
                break;
        }

        

        if (mouse.x > 0) { anim.SetBool("isR", true); }
        else { anim.SetBool("isR", false); }
    }




    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "ground")
        {
            jmps = 2;
            ground = true;
        }/*else if(coll.gameObject.tag == "hability"){
            
            var item = coll.gameObject.GetComponent<habilityItem>();
            if (item != null)
            {
                
                lumi_golpes managerScript = habilityManager.GetComponent<lumi_golpes>();

                if (managerScript != null)
                {
                    managerScript.hAdd = item.name_;


                    if (hAdd != "" && aHability < habilities.Length)
                    {
                        habilities[aHability] = hAdd;
                        aHability++;
                        hAdd = "";
                    }
                }
            }
        }*/
    }

    void OnCollisionExit2D(Collision2D coll){
        if(coll.gameObject.tag == "ground"){
            jmps --;
            ground = false;
        }
    }

    void OnCollisionStay2D(Collision2D coll){
        if(coll.gameObject.tag == "ground"){
            jmps = 2;
            ground = true;
        }
    }   


    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "ground"){
            slide = true;
        }


        
    }

    void OnTriggerExit2D(Collider2D coll){
        if(coll.gameObject.tag == "ground"){
            slide = false;
        }
    }

    void OnTriggerStay2D(Collider2D coll){
        if(coll.gameObject.tag == "ground"){
            slide = true;
        }
    }
}