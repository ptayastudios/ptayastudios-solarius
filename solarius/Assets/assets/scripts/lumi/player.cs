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
    public bool airJmpMov;
    public float airJmpMovM;

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
        mov = Input.GetAxisRaw("Horizontal");
        rig.linearVelocity = new Vector2(mov * spd, rig.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && jmps > 0)
        {
            jump();
        }

        if (airJmp)
        {
            airJump();
        }






 
        if (grd) { jmps = 2; }


        if (airJmp && jmpM < 1) {
            jmpM += jmpMf;
        }

        if (jmpM >= 1) {
            airJmp = false;
            jmpM = 0;
            airJmpMov = false;
        }




        if (mov < 0) {
            dir = -1;
        } else if (mov > 0) {
            dir = 1;
        }















        if (grd || ground)
        {
            if (mov != 0)
            {
                anim.SetInteger("transition", 2);
            }
            else
            {
                anim.SetInteger("transition", 1);
            }

        }
        else
        {
            if (!airJmp)
            {
                if (rig.linearVelocity.y > 0)
                {
                    anim.SetInteger("transition", 3);
                }
                else
                {
                    anim.SetInteger("transition", 4);
                }
            }
            else
            {
                if (airJmpMov)
                {
                    anim.SetInteger("transition", 6);
                }
                else
                {
                    anim.SetInteger("transition", 5);
                }
            }
    }

    
        if(dir > 0){anim.SetBool("isR", true);
        }else{anim.SetBool("isR", false);}



        
            
    }

    void jump(){
            if (jmps == 2){
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, jmpF);
            }
            else{
                if (mov != 0) { airJmpMov = true; }
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                jmps--;
                airJmp = true;
            }
    }    

    void airJump(){
        if (airJmpMov){
                rig.linearVelocity = new Vector2(dir * airJmpMovM, jmpF * jmpM / 2);
            }
            else{
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, jmpF * jmpM);
            }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            jmps = 2;
            ground = true;
        }
        


        if(coll.gameObject.tag == "hability"){
            
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