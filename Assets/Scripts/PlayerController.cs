using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Audio
    public AudioClip jumpUp;
    public AudioClip jumpDown;
    public AudioClip gameOver;
    public AudioSource SFXP;
    //Movement
    public Rigidbody myBody;
    public float maxSpeed;
    public float moveAcceleration;
    public float jumpAcceleration;
    public float RCD;
    public Vector3 sidemov;
    private bool isGrounded = false;
    private bool isLeft = false;
    private bool isRight = false;
    private bool isMiddle = true;
    //Misc
    public GameObject Menu;
    private bool IGO;
    public float GOH = -5f;
    void Start ()
    {
        Menu.SetActive(false);
    }
    void FixedUpdate()
    {
        groundChecker();
        ConstantMove();
    }
    void groundChecker()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;
        isGrounded = Physics.Raycast(ray, RCD);
    }
    void Update()
    {
        if (transform.position.y < GOH && IGO == false)
        {
            IGO = true;
            SFXP.PlayOneShot(gameOver);
            Menu.SetActive(true);        
        }
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space)))
        {
            Jump();
        }
        if (isMiddle && (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            sidemov.x = sidemov.x - 2f;
            sidemov.y = transform.position.y;
            sidemov.z = transform.position.z;
            transform.position = sidemov;
            isMiddle = false;
            isLeft = true;
            isRight = false;
        }
        if (isMiddle && (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            sidemov.x = sidemov.x + 2f;
            sidemov.y = transform.position.y;
            sidemov.z = transform.position.z;
            transform.position = sidemov;
            isMiddle = false;
            isRight = true;
            isLeft = false;
        }
        if (isRight && (Input.GetKeyDown(KeyCode.LeftArrow)))
        {
            sidemov.x = sidemov.x - 2f;
            sidemov.y = transform.position.y;
            sidemov.z = transform.position.z;
            transform.position = sidemov;
            isMiddle = true;
            isRight = false;
            isLeft = false;
        }
        if (isLeft && (Input.GetKeyDown(KeyCode.RightArrow)))
        {
            sidemov.x = sidemov.x + 2f;
            sidemov.y = transform.position.y;
            sidemov.z = transform.position.z;
            transform.position = sidemov;
            isMiddle = true;
            isLeft = false;
            isRight = false;
        }
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        //isGrounded = true;
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        //isGrounded = false;
    }
    void ConstantMove()
    {
        Vector3 newVelocity = myBody.velocity; if (newVelocity.z >= maxSpeed)
        {
            newVelocity.z = maxSpeed;
        }
        else
        {
            newVelocity.z = newVelocity.z + moveAcceleration;
        }
        myBody.velocity = newVelocity;
    }
    void Jump()
    {
        SFXP.PlayOneShot(jumpUp);
        Vector3 jumpVelocity = myBody.velocity;
        jumpVelocity.y = jumpVelocity.y + jumpAcceleration;
        myBody.velocity = jumpVelocity;
    }
}
