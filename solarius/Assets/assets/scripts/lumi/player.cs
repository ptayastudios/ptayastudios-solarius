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
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void fixedUpdate(){
        var grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        if(grd){jmps = 2;}
    }
    void Update()
    {
        
        mov = Input.GetAxisRaw("Horizontal");
        rig.linearVelocity = new Vector2(mov*spd, rig.linearVelocity.y);
        
        if(Input.GetButtonDown("Jump") && jmps > 0){
            if(jmps == 2){
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, jmpF);
            }else{
                if(mov != 0){airJmpMov = true;}
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0);
                jmps--;
                airJmp = true;
            }
        }
        
        if(airJmp)
        {
            if(airJmpMov){
                rig.linearVelocity = new Vector2(dir*airJmpMovM, jmpF*jmpM/2);
            }else{
                rig.linearVelocity = new Vector2(rig.linearVelocity.x, jmpF*jmpM);
            }
        }

        if(airJmp && jmpM < 1){
            jmpM += jmpMf;
        }
        
        if(jmpM >= 1){
            airJmp = false;
            jmpM = 0;  
            airJmpMov = false;
        }



        if(slide){
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, sldF);
        }



        if(mov < 0){
            dir = -1;
        }else if(mov > 0){
            dir = 1;
        }

        if(ground){
            if(mov!=0){
                anim.SetInteger("transition", 2);
            }else{
                anim.SetInteger("transition", 1);
            }

        }else{

            if(!airJmp){
                if(rig.linearVelocity.y > 0){
                     anim.SetInteger("transition", 3);
                }else{
                      anim.SetInteger("transition", 4);
                }
            }else{
                if(airJmpMov){
                    anim.SetInteger("transition", 6);
                }else{
                    anim.SetInteger("transition", 5);
                }
            }
            
        }
        //transform.localScale = new Vector3(dir,1,1);
        if(dir > 0){sr.flipX = false;
        }else{sr.flipX = true;}
        
}





    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "ground"){
            jmps = 2;
            ground = true;
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

/*
        if(coll.gameObject.tag == "hability"){
            HabilityItem item = coll.GetComponent<HabilityItem>();
            if (item != null)
            {
                lumi_golpes managerScript = habilityManager.GetComponent<lumi_golpes>();

                if(managerScript != null)
                {
                    managerScript.hAdd = item.nome;
                }
            }
        }
     */   
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