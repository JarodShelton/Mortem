using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemyAI : MonoBehaviour
{
    public float SCYTHE_DMG = 10f;      //dmg that the scythe does

    public Player target; //reference to the player
    public UnityEngine.AI.NavMeshAgent agent;      //reference to the NavMeshAgent

    public float moveSpeed = 20.0f;  //movement speed of the flying enemy
    private Rigidbody rigidBody;    //the enemy's rigidbody
    public float range = 2.0f;      //when distance between player and
                                    //enemy equals range, enemy will explode

    public float hp;          //enemy hp


    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.mass = 2;
        rigidBody.drag = 10;

        target = FindObjectOfType<Player>();

        hp = 20f;
        //agent.Warp(new Vector3(26, 5.5f, 22));
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.CurrentPosition());
        if (Vector3.Distance(target.CurrentPosition(), transform.position) == range)
        {
            //explode (deal dmg and maybe knockback to player)
        }
        if (Vector3.Distance(target.CurrentPosition(), transform.position) > range)
        {
            //find the direction Player is in
            Vector3 direction = target.CurrentPosition() - transform.position;
            direction.Normalize();

            rigidBody.MovePosition(transform.position + (direction * moveSpeed * Time.deltaTime));
            
            //agent.SetDestination(target.CurrentPosition());
        }

        //enemy death
        if (hp <= 0)      
        {
            //explode (only visually, and give player more time)
            Destroy(gameObject);
        }
    }

    public void takeDmg()
    {
        hp = hp - SCYTHE_DMG;
    }

    private void onColliderEnter(Collider other)
    {
        if (other.CompareTag("Scythe")) //if collides with scythe
        {
            takeDmg();
        }
    }
}
