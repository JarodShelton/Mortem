using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leach : MonoBehaviour
{
    public GameObject deathExplosion;
    public float timeLoss = 5f;
    TimeManager timer;
    Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<TimeManager>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(body.velocity.magnitude);
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag != "Scythe"){
            //Debug.Log("Leach hit something");
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
            if(collision.gameObject.tag == "Player"){
                timer.AddTime(-timeLoss);
            }
            Destroy(gameObject);
        }
    }
}
