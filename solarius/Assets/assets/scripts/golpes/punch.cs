using UnityEngine;

public class punch : MonoBehaviour {
    public float time;
    public float spd;
    public float dmg;

    void FixedUpdate() {
        transform.position += transform.right * spd * Time.deltaTime;
        spd += 0.5f;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject, 0.01f);
        Debug.Log("colisao");
    }
    void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject, 0.01f);
        Debug.Log("colisao2");
    }
}
