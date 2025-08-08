using UnityEngine;

public class explosao : MonoBehaviour
{
    public float damageVaule;
    void Start()
    {
        Destroy(gameObject, 0.250f);
    }

}
