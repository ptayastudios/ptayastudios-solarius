using UnityEngine;

public class punch : MonoBehaviour
{
    public float time;
    public float spd;
    public float capazMan;
    public float dmg;
    void Start()
    {
        Destroy(gameObject, 0.6875f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += transform.right * spd * capazMan * Time.deltaTime;
        capazMan -= 0.1f;
//        time -=- 1*Time.deltaTime;
//        if(time <= 0){instanceDestroy()}
    }


}
