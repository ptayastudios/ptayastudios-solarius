using UnityEngine;

public class corte : MonoBehaviour
{
    public float dmg;
    void Start()
    {
        Destroy(gameObject, 0.375f);
    }
}
