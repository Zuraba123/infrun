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
    public int LaneIndex = 1;
    public float[] LaneZPostition;
    public float SideSpeed;
    public float maxSpeed;
    public float moveAcceleration;
    public float jumpAcceleration;
    public float RCD;
    public Vector3 sidemov;
    private bool isGrounded = false;
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
        PlayerMover();
    }
    void groundChecker()
    {
        Ray ray = new Ray();
        ray.origin = transform.position;
        ray.direction = Vector3.down;
        isGrounded = Physics.Raycast(ray, RCD);
    }
    void LaneChooser ()
    {
	    if(Input.GetKeyDown(KeyCode.A) && LaneIndex > 0)
        {
            LaneIndex = LaneIndex - 1;
        }
        else if(Input.GetKeyDown(KeyCode.D) && LaneIndex < 2)
        {
            LaneIndex = LaneIndex + 1;
        }
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
    }
    void PlayerMover()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.z = LaneZPostition[LaneIndex];
        Debug.Log(targetPosition);
        transform.position = Vector3.Lerp(transform.position, targetPosition, SideSpeed * Time.deltaTime);
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
