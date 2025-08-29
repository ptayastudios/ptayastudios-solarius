using UnityEngine;
using UnityEngine.UI;
public class vida : MonoBehaviour
{

    public int vidas;
    public GameObject[] heart = new GameObject[3];
    public GameObject dmgObj;
    public Rigidbody2D rig;
    public float knockbackForce;


    public bool cnTimer;
    public float timer;
    public float totalTimer;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
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

            /*
            float horizontalDir = Mathf.Sign(transform.position.x - dmgObj.transform.position.x); 
            Vector2 knockback = new Vector2(horizontalDir * knockbackForce, knockbackForce * 0.1f);
            rig.AddForce(knockback, ForceMode2D.Impulse);*/
            
            float horizontalDir = Mathf.Sign(transform.position.x - dmgObj.transform.position.x);
            Vector2 knockback = new Vector2(horizontalDir * knockbackForce, knockbackForce * 0.1f);

            // pega referência do script de movimento
            principalScript.externalVelocity += knockback;
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


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "enemy")
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


/*
sistema de knockback do player nao funciona.
o timer funciona mas o player ainda pode se mover livremente durante
o knockback e nao sai voando


*/