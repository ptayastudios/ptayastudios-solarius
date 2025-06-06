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
    public GameObject PlayerObject;
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


    public Rigidbody2D rig;
    public SpriteRenderer sr;
    public Animator anim;

    public LayerMask gl;
    public Transform groundCheck;
    public Transform visionPoint;    
    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        PlayerObject = GameObject.Find("lumi");
        player = PlayerObject.transform;
    }

    void Update()
    {
        var ground = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        playerDist = Vector2.Distance(transform.position, player.position);


        if (playerDist < dd && state == "walk")
        {
            Vector2 directionToPlayer = (player.position - visionPoint.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(visionPoint.position, directionToPlayer, dd);

            Debug.DrawRay(visionPoint.position, directionToPlayer * dd, Color.red); // ajuda na depuração

            if (hit.collider != null && hit.collider.gameObject == PlayerObject)
            {
                state = "oooh";

                if (player.transform.position.x > transform.position.x)
                {
                    dir = 1;
                }
                else
                {
                    dir = -1;
                }
            }
        }

        if (dir < 0)
            { sr.flipX = false;
            }
            else { sr.flipX = true; }

        if(ooohTime > 0 && state == "oooh"){
            ooohTime -= 0.1f;
        }


        



    








        switch (state)
        {
            case "chase":
                dmgTimer = 0;

                if (ground)
                {
                    if (aspd < spdM * dir * 1.2f) { aspd += spdI; }
                    else { aspd -= spdI; }
                }

                rig.linearVelocity = new Vector2(aspd, rig.linearVelocity.y);


                anim.SetInteger("transition", 3);
                oh = false;
                if (playerDist > lstD)
                {
                    state = "walk";
                }
                break;

            case "walk":
                if (ground)
                {
                    if (aspd < spdM * dir) { aspd += spdI; }
                    else { aspd -= spdI; }
                }
                rig.linearVelocity = new Vector2(aspd, rig.linearVelocity.y);
                anim.SetInteger("transition", 1);
                break;

            case "oooh":
                anim.SetInteger("transition", 2);
                if (ooohTime <= 0)
                {
                    if (oh)
                    {
                        state = "chase";
                    }
                    else
                    {
                        ooohTime = ooohTimeMax;
                        oh = true;
                    }
                }
                break;

            case "damage":
                life--;
                rig.linearVelocity = new Vector2(dmgspd * dir, jmpF);
                if (dmgTimer == 0) { dmgTimer = dTmax; }
                dmgTimer--;
                if (dmgTimer <= 1)
                {
                    state = "chase";
                }
                break;
        }

    }

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.gameObject.tag == "wall"){
            dir = dir * -1;
        }

        if (coll.gameObject.tag == "ground")

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
