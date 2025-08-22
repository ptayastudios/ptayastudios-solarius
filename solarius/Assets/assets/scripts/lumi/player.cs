using UnityEngine;

public class player : MonoBehaviour
{
    //movimento
    public float mov;
    public float spd;
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
    
    


    void Start()
    {
        Debug.Log("teste");
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    void Update(){
        grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);

        //rig.linearVelocity = new Vector2(mov * spd, rig.linearVelocity.y);
        
        //muda a forma de movimentção, a forma atual impede inercia e kanockback na horizontal

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