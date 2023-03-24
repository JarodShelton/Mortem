using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackProjectile : MonoBehaviour
{
    public float damage = 20f;
    public float returnSpeed = 20f;
    public float spinRate = 0.1f;
    public Rigidbody rigidBody; 

    float noDamageTimer = 0.03f;
    float range = 1.5f;
    float spinAmount = 0f;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(noDamageTimer > 0)
            noDamageTimer-=Time.fixedDeltaTime;
        spinAmount += spinRate*Time.fixedDeltaTime;
        spinAmount %= 360;
        transform.LookAt(player.CurrentPosition());
        transform.Rotate(-30f, 90f, spinAmount);
        if (Vector3.Distance(player.CurrentPosition(), transform.position) <= range)
        {
            player.CanFire(true);
            player.Shake(0.05f, 0.1f);
            Destroy(gameObject);
        }
        if (Vector3.Distance(player.CurrentPosition(), transform.position) > range)
        {
            //find the direction Player is in
            Vector3 direction = player.CurrentPosition() - transform.position;
            direction.Normalize();

            rigidBody.MovePosition(transform.position + (direction * returnSpeed * Time.fixedDeltaTime));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && noDamageTimer <=0)
        {
            collision.gameObject.GetComponent<EnemyHealth>().takeDmg(damage);
            //player.Shake(0.055f, 0.09f);
        }
    }

    }
