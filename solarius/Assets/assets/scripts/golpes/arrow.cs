using UnityEngine;

public class arrow : MonoBehaviour
{


    void Start()
    {
        transform.Rotate(0, 0, -45);
    }

    void Update()
    {
        Vector2 vel = GetComponent<Rigidbody2D>().linearVelocity;
        if (vel.sqrMagnitude > 0.1f)
        {
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle -45);
        }
    }

    void OnCollisionEnter2D(Collision2D coll){
        Destroy(this.gameObject);
    }
}
