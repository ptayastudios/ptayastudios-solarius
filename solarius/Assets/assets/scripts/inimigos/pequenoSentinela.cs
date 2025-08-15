using UnityEngine;
using UnityEngine.Rendering.Universal;

public class pequenoSentinela : MonoBehaviour
{
    public int dir;
    public float spdM;
    public float aspd;
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
    public float maxLife;
    public Transform lifeBar;
    public Vector3 initialScale;
    public float acDmg;
    public bool cnKncb;
    public bool knbTrigger;
    public float knbTimer;
    public float knbMax;



    public float jmpF;
    public float dmgTimer;
    public float maxDmgTimer;
    public float dmgspd;
    public bool dmgTrg;
    public float deadTimer;
    public float maxDeadTimer;
    public bool deadTrg;



    public Rigidbody2D rig;
    public SpriteRenderer sr;
    public Animator anim;
    public Collider2D colider;



    public LayerMask gl;
    public Transform groundCheck;
    public Transform visionPoint;


    public GameObject visionObject;
    public GameObject light_;


    public GameObject dmgObj;



    public bool ground;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rig = GetComponent<Rigidbody2D>();
        colider = GetComponent<CapsuleCollider2D>();


        PlayerObject = GameObject.Find("lumi");
        player = PlayerObject.transform;

    }

    void Update()
    {
        Light2D lightScript = light_.GetComponent<Light2D>();
        ground = Physics2D.OverlapCircle(groundCheck.position, 0.2f, gl);
        playerDist = Vector2.Distance(transform.position, player.position);

        Vector2 directionToPlayer = (player.position - visionPoint.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(visionPoint.position, directionToPlayer, dd);

        Vector2 distanceToPlayer = (player.position - visionPoint.position).normalized;
        RaycastHit2D hit2 = Physics2D.Raycast(visionPoint.position, distanceToPlayer, playerDist);

        Debug.DrawRay(visionPoint.position, directionToPlayer * dd, Color.red);
        Debug.DrawRay(visionPoint.position, distanceToPlayer * playerDist, Color.yellow);


        visionObject.transform.position = new Vector3(transform.position.x + 0.1f * dir, transform.position.y, 0f);




        if (playerDist < dd && state == "walk" && !dead)
        {
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
        {
            sr.flipX = false;
        }
        else { sr.flipX = true; }







        if (ooohTime > 0 && !dead && state == "oooh")
        {
            ooohTime -= 0.1f;
        }


        if (dmgTimer > 0) { dmgTimer--; }

        if (life <= 0) { state = "die"; }












        switch (state)
        {
            case "chase":
                dmgTrg = true;
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


                lightScript.color = new Color(1f, 0f, 0f);


                if (hit.collider != null && hit.collider.gameObject != PlayerObject)
                {
                    state = "walk";
                }

                if (player.transform.position.x > transform.position.x)
                {
                    dir = 1;
                }
                else { dir = -1; }

                break;

            case "walk":
                lightScript.color = new Color(1f, 0.5f, 0f);
                dmgTrg = true;
                if (ground)
                {
                    if (aspd < spdM * dir) { aspd += spdI; }
                    else { aspd -= spdI; }
                }
                rig.linearVelocity = new Vector2(aspd, rig.linearVelocity.y);
                anim.SetInteger("transition", 1);
                break;

            case "oooh":
                lightScript.color = new Color(1f, 0.3f, 0f);
                dmgTrg = true;
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


                if (dmgTimer == 0 && dmgTrg == true) { dmgTimer = maxDmgTimer; }

                if (dmgTrg == true)
                {
                    life -= acDmg;
                    dmgTrg = false;
                }

                if (dmgTimer <= 0)
                {
                    lightScript.color = new Color(1f, 0f, 0f);


                    if (acDmg != 0)
                    {
                        acDmg = 0;
                        dmgTimer = maxDmgTimer;
                    }



                    dmgTimer--;
                    if (dmgTimer <= 1 && ground && !dmgTrg)
                    {
                        if (playerDist > lstD)
                        {
                            state = "walk";
                        }
                        else
                        {
                            state = "chase";
                        }
                    }


                    UpdateLifeBar();
                }
                else
                {
                    lightScript.color = new Color(0f, 0f, 1f);
                    rig.linearVelocity = new Vector2(dmgspd * -dir, jmpF);
                }

                if (dmgObj != null && cnKncb)
                {
                    Vector2 knockbackObj = new Vector2(dmgObj.transform.position.x, dmgObj.transform.position.y);

                    Vector2 dmgDir = new Vector2(knockbackObj.x - transform.position.x, knockbackObj.y - transform.position.y);


                    Vector2 knockback = dmgDir.normalized * 5;
                    rig.linearVelocity = new Vector2(knockback.x * -1, knockback.y * -1);
                    //rig.AddForce(knockback, ForceMode2D.Impulse);
                }

                break;



            case "die":
                lightScript.color = new Color(0f, 0f, 0f);
                dead = true;
                anim.SetInteger("transition", 4);

                if (deadTimer < 0) { rig.linearVelocity = new Vector2(dmgspd * -dir, jmpF); }
                if (deadTimer > 0) { deadTimer--; }

                break;
        }

    }


    void UpdateLifeBar()
    {
        if (lifeBar != null)
        {
            float ratio = life / maxLife;
            lifeBar.localScale = new Vector3(initialScale.x * ratio, initialScale.y, initialScale.z);
        }
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "wall")
        {
            dir = dir * -1;

            if (dead)
            {
                colider.isTrigger = true;
            }
        }

        if (coll.gameObject.tag == "ground")

            if (coll.gameObject.tag == "dmgEnemy")
            {
                state = "damage";

                dmg dmgS = coll.gameObject.GetComponent<dmg>();
                acDmg = dmgS.damageVaule;
            }


    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "dmgEnemy")
        {
            state = "damage";


            dmg dmgS = coll.gameObject.GetComponent<dmg>();
            acDmg = dmgS.damageVaule;
        }
        if (coll.gameObject.tag == "explode")
        {
            dmgObj = coll.gameObject;
            state = "damage";
            cnKncb = true;

            dmg dmgS = coll.gameObject.GetComponent<dmg>();
            acDmg = dmgS.damageVaule;
        }
        if (coll.gameObject.tag == "wall")
        {
            dir = dir * -1;

            if (dead)
            {
                colider.isTrigger = true;
            }
        }
    }
    
    
    
   
}


/*
   







*/