/*using UnityEngine;
using System.Collections.Generic;

public class habiltySlots : MonoBehaviour
{

    //public Vector2 mouse;
    //public Vector2 dir;
    public float angle;
    public int Dir;




    /////////////////soco/////////////////
    public float cldwSoco;
    public float cldwSocoDflt;
    public bool cnSht;
    public Transform shtPoint;
    public GameObject punch;













    public int[] slots = new int[5]; // Cada slot armazena o ID da habilidade equipada (0 = vazio)
    private int slotSelecionado = 0; // Qual slot estamos preenchendo no momento

    private Dictionary<KeyCode, int> teclaParaHabilidade = new Dictionary<KeyCode, int>()
    {
        { KeyCode.I, 1 },
        { KeyCode.O, 2 },
        { KeyCode.P, 3 }
    };

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 dir = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
        transform.right = dir;





        if(Input.GetKeyDown() && cnSht){
            Instantiate(punch, shtPoint.transform.position, this.gameObject.transform.rotation);
            cnSht = false;
        }


        if(!cnSht){
            cldwSoco --;
        }
        if(cldwSoco <= 0){
            cnSht = true;
            cldwSoco = cldwSocoDflt;
        }   


































        if (slotSelecionado < slots.Length)
        {
            foreach (var par in teclaParaHabilidade)
            {
                if (Input.GetKeyDown(par.Key))
                {
                    int habilidade = par.Value;
                    if (!HabilidadeJaSelecionada(habilidade))
                    {
                        slots[slotSelecionado] = habilidade;
                        slotSelecionado++;
                        break;
                    }
                }
            }
        }

        // Desfazer com tecla L
        if (Input.GetKeyDown(KeyCode.L) && slotSelecionado > 0)
        {
            slotSelecionado--;
            slots[slotSelecionado] = 0;
        }
    }

    bool HabilidadeJaSelecionada(int habilidade)
    {
        foreach (int s in slots)
        {
            if (s == habilidade) return true;
        }
        return false;
    }
}*/