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

    //objetos
    public LayerMask gl;
    public Transform groundCheck;

    //relacionados a habilidades
    public string clctdHability;
    public GameObject habilityManager;

    //componentes
    public Rigidbody2D rig;
    public Animator anim;
    public SpriteRenderer sr;





    public string[] habilities = new string[16];
    public int aHability = 1;
    public int[] slots = new int[5];
    public int aSlot;
    public int fire;

    public string hAdd;
    public Transform Mira;



    public bool grd;



    
    
    public Vector2 externalVelocity;    // guarda knockbacks, empurrões etc.
    public float externalDecay = 5f;    // taxa de "perda" dessas forças


    void Start()
    {
        Debug.Log("teste");
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }


    void FixedUpdate()
    {

        /*alguma coisa ta impedindo dash na horizontal e o knockback na vertical, no geral, o knockback ta com uma fisica boa
        tirando o fato de que nao funciona na vertical bah*/
        grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);



        Vector2 lV = rig.linearVelocity;

        // input só mexe no X
        float inputX = mov * maxSpd;

        // soma input + external no X
        float finalX = inputX + externalVelocity.x;

        // no ar, corta inércia horizontal se não há input nem external
        if (!grd && mov == 0 && Mathf.Abs(externalVelocity.x) < 0.1f)
        {
            finalX = 0f;
        }

        // Y agora também recebe external
        float finalY = lV.y + externalVelocity.y;

        // aplica velocidade resultante
        rig.linearVelocity = new Vector2(finalX, finalY);

        // decaimento da external
        externalVelocity = Vector2.MoveTowards(externalVelocity, Vector2.zero, externalDecay * 3f * Time.fixedDeltaTime);


    }


    void Update(){
        grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);

        










        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 Mdir = new Vector2(Mira.position.x - transform.position.x, Mira.position.y - transform.position.y);


       
        

        if (state != "airJmp")
        {
            mov = Input.GetAxisRaw("Horizontal");
        }


        if (mov == 0)
        {
            state = "idle";
        }
        else { state = "walk"; }


        if (Input.GetButtonDown("Jump") && jmps > 0)
        {
            state = "jump";
        }


        if (airJmp)
        {
            state = "airjump";
        }

        var trigger = false;
        if (airJmp)
        {
            trigger = true;
        }
        if (trigger)
        {
            airJumpTime = airJumpTimeMax;
            trigger = false;
        }
        if (airJumpTime > 0)
        {
            airJumpTime -= 0.1f;
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








        switch (state)
        {
            case "idle":
                anim.SetInteger("transition", 1);
                break;

            case "walk":
                anim.SetInteger("transition", 2);
                break;

            case "jump":

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

                Vector2 dash = Mdir.normalized * airJmpMovM;
                rig.linearVelocity = new Vector2(dash.x, dash.y);

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
        }else if(coll.gameObject.tag == "hability"){
            
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
        }
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