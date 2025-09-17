using UnityEngine;
using System.Collections.Generic;

public class lumi_golpes : MonoBehaviour
{

    //public Vector2 mouse;
    //public Vector2 dir;
    public float angle;
    public int Dir;
    public Animator anim;
    public Transform lumiTransform;
    public GameObject lumiObj;




    /////////////////soco/////////////////
    public float cldwSoco;
    public float cldwSocoDflt;
    public bool cnShtSoco;
    public Transform shtPoint;
    public GameObject punch;








    /////////////////corte/////////////////
    public float cldwCorte;
    public float cldwCorteDflt;
    public bool cnShtCorte;
    public GameObject corte;





    /////////////////arco/////////////////
    public bool hold;
    public bool shoot;
    public string bowButton;
    public GameObject arrow;
    public float arrowF;




    /////////////////explosao/////////////////
    public GameObject explosion;






    public string[] habilities = new string[16];
    public int aHability = 1;
    public int[] slots = new int[5];
    public int aSlot;
    public int fire;
    public string hAdd;

    public static lumi_golpes Instance;

    private void Awake()
    {
        Instance = this;
    }



    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 dir = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
        transform.right = dir;


        if (fire == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                fire = slots[0];
                if (fire == 3) { bowButton = "Fire1"; }
            }
            if (Input.GetButtonDown("Fire2"))
            {
                fire = slots[1];
                if (fire == 3) { bowButton = "Fire2"; }
            }
            if (Input.GetButtonDown("Fire3"))
            {
                fire = slots[2];
                if (fire == 3) { bowButton = "Fire3"; }
            }
        }



        switch (fire)
        {
            case 1:
                if (cnShtSoco)
                {
                    Instantiate(punch, shtPoint.transform.position, this.gameObject.transform.rotation);
                    cldwSoco = cldwSocoDflt;
                    cnShtSoco = false;
                    fire = 0;
                }
                break;

            case 2:
                if (cnShtCorte)
                {
                    Instantiate(corte, shtPoint.transform.position, this.gameObject.transform.rotation);
                    cldwCorte = cldwSocoDflt;
                    cnShtCorte = false;
                    fire = 0;
                }
                break;

            case 3:
              /*  anim.SetInteger("transition", 2);
                hold = false;
                if (Input.GetButtonUp(bowButton))
                {
                    anim.SetInteger("transition", 3);
                    GameObject arw = Instantiate(arrow, shtPoint.position, this.gameObject.transform.rotation);
                    Vector2 baseDir = new Vector2(1, -1).normalized;
                    Vector2 dir_ = shtPoint.rotation * baseDir;
                    Rigidbody2D rb = arw.GetComponent<Rigidbody2D>();
                    rb.AddForce(dir_ * arrowF, ForceMode2D.Impulse);
                    arw.transform.Rotate(0, 0, -45);
                    fire = 0;
                }*/
                break;

        }





        if (cldwSoco > 0)
        {
            cldwSoco--;
        }
        if (cldwSoco == 0)
        {
            cnShtSoco = true;
        }

        if (cldwCorte > 0)
        {
            cldwCorte--;
        }
        if (cldwCorte == 0)
        {
            cnShtCorte = true;
        }






        if (hAdd != "" && aHability < habilities.Length)
        {
            habilities[aHability] = hAdd;
            aHability++;
            hAdd = "";
        }
    }

    public void HabilitySet(int index, int name_)
    {
        slots[index] = name_;
        Debug.Log($"Slot {index} definido com: {name_}");
    }

}





/*

---hability index---

- 1 - punch
- 2 - simple attack
- 3 - bow




*/