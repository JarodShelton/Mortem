using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullTurret : MonoBehaviour
{
    public GameObject leach;
    public Transform firePoint;
    public float leachSpeed;
    public float shootDelay;
    
    Player player;
    float shootTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.CurrentPosition());
        transform.Rotate(0f, 90f, 0f);
        if(shootTimer >= shootDelay){
            var projectileObj = Instantiate(leach, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.transform.LookAt(player.CurrentPosition());
            projectileObj.transform.Rotate(0f, -90f, 0f);
            Vector3 destination = player.CurrentPosition();
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * leachSpeed * Time.fixedDeltaTime;
            shootTimer = 0;
        }else{
            shootTimer += Time.deltaTime;
        }
        
    }
}
