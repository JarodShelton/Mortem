using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{ 
    public CharacterController controller;

    public float acceleration = 1f;
    public float maxVelocity = 12f;
    public float gravity = -5f;
    public float jumpVelocity = 5F;

    public Transform groundCheck;
    public float groundCheckDistance = 0.55f;
    public float jumpCheckDistance = 0.6f;
    public LayerMask groundMask;

    public AudioSource source;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip hitSound;
    public AudioClip hurtSound;

    public float mouseSensitivity = 600f;

    public Camera cam;
    public CameraShaker camShaker;
    public GameObject projectile;
    public Transform firePoint;
    public GameObject scythe;
    public Transform playerBody;

    float cameraPitch = 0f;
    float cameraTurn = 0f;

    Vector3 destination;

    float landTimer = 0;
    float maxRoll = 3f;
    float drag = 0.1f;
    Vector3 velocity;
    Vector3 appliedVelocity;

    bool isGrounded;
    bool canJump;
    bool landed = true;
    bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        drag = acceleration/maxVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        move();
        shoot();
        look();
    }

    void shoot(){
        if(Input.GetButtonDown("Fire1") && canFire){
            CanFire(false);
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
                destination = hit.point;
            else
                destination = ray.GetPoint(1000);

            var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
            projectileObj.transform.LookAt(transform.position);
            projectileObj.transform.Rotate(30f, -90f, 0f);
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized;
            Shake(0.1f, 0.085f);
        }
    }

    void look(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        
        cameraTurn += mouseX;
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        transform.localRotation = Quaternion.Euler(cameraPitch, cameraTurn, -velocity.x/maxVelocity*maxRoll);
    }

    void move(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckDistance, groundMask);
        canJump = Physics.CheckSphere(groundCheck.position, jumpCheckDistance, groundMask);
        //drag = acceleration/maxVelocity;

        float xAcceleration = 0f;
        float zAcceleration = 0f;
        if(Input.GetKey(KeyCode.A))
            xAcceleration -= acceleration;
        if(Input.GetKey(KeyCode.D))
            xAcceleration += acceleration;
        if(Input.GetKey(KeyCode.S))
            zAcceleration -= acceleration;
        if(Input.GetKey(KeyCode.W))
            zAcceleration += acceleration;
        velocity.x = velocity.x + (xAcceleration - velocity.x*drag)*Time.deltaTime;
        velocity.z = velocity.z + (zAcceleration - velocity.z*drag)*Time.deltaTime;

        //handle gravity
        landTimer -= Time.deltaTime;
        if(!landed && canJump && landTimer <= 0){
            Shake(0.03f, 0.02f);
            source.PlayOneShot(landSound, 0.5f);
            landed = true;
        }
        if(isGrounded){
            if(velocity.y < 0){
                velocity.y = 0f;
            }
        }else{
            velocity.y += gravity * Time.deltaTime;
        }

        if(canJump && Input.GetButtonDown("Jump")){
                landTimer = 0.3f;
                landed = false;
                velocity.y = jumpVelocity;
                Shake(0.03f, 0.02f);
                source.PlayOneShot(jumpSound, 0.5f);
        }
        Vector3 move = transform.right * velocity.x + transform.forward * velocity.z;
        move.y = velocity.y;
        move += appliedVelocity;
        controller.Move(move * Time.deltaTime);
        appliedVelocity -= appliedVelocity*drag*Time.deltaTime;
    }

    public bool IsGrounded(){
        return isGrounded;
    }

    public void Shake(float duration, float magnitude){
        StartCoroutine(camShaker.Shake(duration, magnitude));
    }

    public void CanFire(bool canFire){
        this.canFire = canFire;
        scythe.SetActive(canFire);
    }

    public void ApplyForce(Vector3 force){
        appliedVelocity = force;
    }

    public Vector3 HorizonalVelocity(){
        return velocity;
    }

    //returns Player's current position (so FlyingEnemyAI.cs can access it) 
    public Vector3 CurrentPosition()
    {
        return transform.position;
    }

    public void TakeDamage(){
        Debug.Log("Playing sound");
        source.PlayOneShot(hurtSound, 0.5f);
    }

}
