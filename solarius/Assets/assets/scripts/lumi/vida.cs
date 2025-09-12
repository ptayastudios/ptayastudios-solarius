using UnityEngine;
using UnityEngine.UI;
public class vida : MonoBehaviour
{

    public int vidas;
    public GameObject[] heart = new GameObject[3];
    public GameObject dmgObj;
    public Rigidbody2D rig;
    public float knockbackForce;
    public float KnockbackForceY;


    public bool cnTimer;
    public float timer;
    public float totalTimer;


    public float coldown;
    public float maxColdown;
    public bool onColdwon;
    private Image selfImage;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        selfImage = GetComponent<Image>();
    }

    void Update()
    {
        player principalScript = GetComponent<player>();
        if (cnTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                cnTimer = false;
                principalScript.state = "knockback";
                timer = totalTimer;
            }
        }


        if (onColdwon)
        {
            coldown -= Time.deltaTime;
            if (coldown <= 0f)
            {
                onColdwon = false;
                coldown = 0f;
            }
        }

        if (selfImage != null)
        {
            Color coldownColor = selfImage.color;
            coldownColor.a = onColdwon ? 0.3f : 1f;
            selfImage.color = coldownColor;
        }
    }


    void dano()
    {



        if (vidas > 0)
        {
            player principalScript = GetComponent<player>();


            cnTimer = true;

            var heart_ = heart[vidas - 1];
            var heartImage = heart_.GetComponent<Image>();
            if (heartImage != null)
            {
                Color heartColor = heartImage.color;
                heartColor.a = 0.3f;
                heartImage.color = heartColor;
            }
            vidas--;
            Debug.Log("Dano sofrido. Vidas restantes: " + vidas);


            float horizontalDir = Mathf.Sign(transform.position.x - dmgObj.transform.position.x);
            float verticalDir = principalScript.grd ? 1f : 0.5f;

            Vector2 knockback = new Vector2(horizontalDir * knockbackForce, knockbackForce * KnockbackForceY * verticalDir);
            rig.linearVelocity = knockback;

            principalScript.state = "knockback";
            principalScript.knkTimer = principalScript.knkTimerMax;

            onColdwon = true;
            coldown = maxColdown;
        }
    }

    void regeneração()
    {
        if (vidas < 3)
        {
            var heart_ = heart[vidas];
            var heartImage = heart_.GetComponent<Image>();
            if (heartImage != null)
            {
                Color heartColor = heartImage.color;
                heartColor.a = 1f;
                heartImage.color = heartColor;
            }
            vidas++;
            Debug.Log("Dano sofrido. Vidas restantes: " + vidas);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "enemy" && !onColdwon)
        {
            dmgObj = coll.gameObject;
            dano();
        }
        else if (coll.gameObject.tag == "regen")
        {
            regeneração();
        }
    }
}


