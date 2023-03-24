using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject deathExplosion;
    public GameObject hurtEffect;
    //private GameObject timeMgmt;
    public float invincibilityDuration = 0.3f;
    public float hp = 20;          //enemy hp
    public float timeGain = 1f;

    float iFrameTimer = 0f;
    TimeManager timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(iFrameTimer > 0)
            iFrameTimer -= Time.deltaTime;
        if (hp <= 0){
            //timeMgmt = GameObject.FindWithTag("time");
           // timeMgmt.GetComponent<TimeManager>().addTime(5f);
            //explode (only visually, and give player more time)
            timer.AddTime(timeGain);
            Instantiate(deathExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            //Destroy(gameObject);
        }
    }

    public void takeDmg(float dmg)
    {
        if(iFrameTimer <= 0){
            hp = hp - dmg;
            //Debug.Log("Took "+dmg+" Damage");
            iFrameTimer = invincibilityDuration;
        }
        if(!isDead())
            Instantiate(hurtEffect, transform.position, Quaternion.identity);
    }

    public bool isDead(){
        return hp <= 0;
    }
}
