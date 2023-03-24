using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectile : MonoBehaviour
{
    public float damage = 10f;
    public float spinSpeed = 8f;
    public float velocity = 5000;
    public AudioClip hitObjectSound;
    public GameObject backProjectile;
    public GameObject projectile;

    bool hitSomething = false;
    float timeToStop = 0.02f;
    Rigidbody body;
    Vector3 initialVelocity;
    Quaternion initialRotation;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        initialVelocity = body.velocity;
        initialRotation = body.rotation;
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            var projectileObj = Instantiate(backProjectile, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
    }

    void FixedUpdate(){
        body.MovePosition(transform.position + (initialVelocity * velocity * Time.fixedDeltaTime));
        if(timeToStop >= 0){
            transform.Rotate(0f, 0f, spinSpeed*Time.fixedDeltaTime);
            if(hitSomething)
                timeToStop -= Time.fixedDeltaTime;
        }else{
            velocity = 0;
            body.isKinematic = true;
        } 
    }

    void OnCollisionEnter(Collision collision) {
        bool killedEnemy = false;
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.takeDmg(damage);
            killedEnemy = enemy.isDead();
            //player.Shake(0.055f, 0.09f);
        }
        if (collision.gameObject.tag != "Player" && !killedEnemy)
        {
            GetComponent<AudioSource>().Stop();
            if(collision.gameObject.tag != "Enemy")
                GetComponent<AudioSource>().PlayOneShot(hitObjectSound, 0.5f);
            hitSomething = true;
            projectile.transform.parent = collision.transform;
            body.detectCollisions = false;
            //player.Shake(0.04f, 0.05f);
        }
    }
}
