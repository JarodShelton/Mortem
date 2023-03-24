using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemyAI : MonoBehaviour
{
    Player target; //reference to the player
    //public GameObject timeMgmt;
    public GameObject attackExplosion;
    public GameObject backProjectile;
    public UnityEngine.AI.NavMeshAgent agent;      //reference to the NavMeshAgent
    public float knockBack = 100f;
    public float moveSpeed = 20.0f;  //movement speed of the flying enemy
    private Rigidbody rigidBody;    //the enemy's rigidbody
    public float range = 2.0f;      //when distance between player and enemy equals range, enemy will explode
    public float timeLoss = 5f;
    TimeManager timer;
    EnemySpawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.mass = 2;
        rigidBody.drag = 10;

        target = FindObjectOfType<Player>();
        timer = FindObjectOfType<TimeManager>();
        spawner = FindObjectsOfType<EnemySpawner>()[1];

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(target.CurrentPosition());
        if (Vector3.Distance(target.CurrentPosition(), transform.position) <= range)
        {
            timer.AddTime(-timeLoss);
            //explode (deal dmg and maybe knockback to player)
            Vector3 direction = (transform.position - target.CurrentPosition()).normalized;
            target.ApplyForce(-direction*knockBack);
            Instantiate(attackExplosion, transform.position, Quaternion.identity);
            if(transform.childCount > 1){
                var projectileObj = Instantiate(backProjectile, transform.position, Quaternion.identity) as GameObject;
            }
            target.TakeDamage();
            Destroy(gameObject);
            spawner.death();

        }
        if (Vector3.Distance(target.CurrentPosition(), transform.position) > range)
        {
            //find the direction Player is in
            Vector3 direction = target.CurrentPosition() - transform.position;
            direction.Normalize();

            rigidBody.MovePosition(transform.position + (direction * moveSpeed * Time.fixedDeltaTime));
            
            //agent.SetDestination(target.CurrentPosition());
        }
    }

    public void incrementSpeed()
    {
        moveSpeed = moveSpeed + 0.2f;
    }
}
