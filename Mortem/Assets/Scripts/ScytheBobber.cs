using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScytheBobber : MonoBehaviour
{
    public Player player;
    public float walkingBobbingSpeed = 10f;
    public float bobbingAmount = 0.05f;
    public float walkingSwaySpeed = 5f;
    public float swayAmount = 0.025f;

    float trueDefaultPosY = 0;
    float defaultPosY = 0;
    float trueDefaultPosX = 0;
    float defaultPosX = 0;
    float bobTimer = 0;
    float swayTimer = 0;

    int endTimer = 0;
    int timeToStop = 20;
    // Start is called before the first frame update
    void Start()
    {
        trueDefaultPosY = transform.localPosition.y;
        defaultPosY = trueDefaultPosY + bobbingAmount;
        trueDefaultPosX = transform.localPosition.x;
        defaultPosX = transform.localPosition.x + swayAmount;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.IsGrounded() && (isMoving() || endTimer>0)){
            if(player.IsGrounded() && isMoving())
                endTimer = timeToStop;
            bobTimer += Time.deltaTime * walkingBobbingSpeed;
            swayTimer += Time.deltaTime * walkingSwaySpeed;
            float xSign = 1;
            if(swayTimer == 0){
                if(player.HorizonalVelocity().x<0)
                    xSign = -1;
                defaultPosX = trueDefaultPosX + swayAmount*xSign;
            }

            transform.localPosition = new Vector3(transform.localPosition.x, defaultPosY + -Mathf.Cos(bobTimer) * bobbingAmount, transform.localPosition.z);
            transform.localPosition = new Vector3(defaultPosX + -Mathf.Cos(swayTimer) * swayAmount * xSign, transform.localPosition.y,  transform.localPosition.z);
        }else{
            bobTimer = 0;
            swayTimer = 0;
            transform.localPosition = new Vector3(transform.localPosition.x, Mathf.Lerp(transform.localPosition.y, trueDefaultPosY, Time.deltaTime * walkingBobbingSpeed), transform.localPosition.z);
            transform.localPosition = new Vector3(Mathf.Lerp(transform.localPosition.x, trueDefaultPosX, Time.deltaTime * walkingSwaySpeed), transform.localPosition.y, transform.localPosition.z);
        }
        endTimer--;
    }

    bool isMoving(){
        return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S);
    }
}
