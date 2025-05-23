using UnityEngine;

public class pequenoSentinela : MonoBehaviour
{
    public int dir;
    public float spdM;
    public float aspd;
    public float dmgspd;
    public float spdI;


    public float dd;
    public string state;
    public Transform player;
    public bool dead;
    public float ooohTime;
    public float ooohTimeMax;
    public float playerDist;
    public float lstD;
    public bool oh;

    public float life;
    public float jmpF;

    public float dmgTimer;
    public float dTmax;

    public bool ground;

    public Rigidbody2D rig;
    public SpriteRenderer sr;
    public Animator anim;

    public LayerMask gl;
    public Transform groundCheck;
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var grd = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        playerDist = Vector2.Distance(transform.position, player.position);


        if(playerDist < dd && state == "walk"){
            state = "oooh";
        }

        if(dir < 0){sr.flipX = false;
        }else{sr.flipX = true;}

        if(ooohTime > 0 && state == "oooh"){
            ooohTime -= 0.1f;
        }


        if(player.transform.position.x > transform.position.x){
            dir = 1;
        }else{
            dir = -1;
        }

       

        

        switch(state){
            case "chase":
                dmgTimer = 0;
                
                if(grd){
                    if(aspd < spdM*dir*1.2f){aspd+=spdI;}
                    else{aspd-=spdI;}
                }

                rig.linearVelocity = new Vector2(aspd, rig.linearVelocity.y);
                

                anim.SetInteger("transition", 3);
                oh = false;
                if(playerDist > lstD){
                    state = "walk";
                }
            break;

            case "walk":
                if(grd){
                    if(aspd < spdM*dir){aspd+=spdI;}
                    else{aspd-=spdI;}
                }
                rig.linearVelocity = new Vector2(aspd, rig.linearVelocity.y);
                anim.SetInteger("transition", 1);
            break;

            case "oooh":
                anim.SetInteger("transition", 2);
                if(ooohTime <= 0){
                    if(oh){
                        state = "chase";              
                    }else{
                        ooohTime = ooohTimeMax;
                        oh = true;
                    }
                }
            break;

            case "damage":
                life--;
                rig.linearVelocity = new Vector2(dmgspd*dir, jmpF);
                if(dmgTimer == 0){dmgTimer = dTmax;}
                dmgTimer--;
                if(dmgTimer <= 1){
                    state = "chase";
                }
            break;
        }

    }

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "wall"){
            dir = dir * -1;
        }

        if(coll.gameObject.tag == "ground")

        if(coll.gameObject.tag == "dmgEnemy"){
            state = "damage";
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if(coll.gameObject.tag == "dmgEnemy"){
            state = "damage";
        }
    }
}
