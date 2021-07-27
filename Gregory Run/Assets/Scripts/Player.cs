using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] Vector2 deathKick = new Vector2(5f,5f);
    [SerializeField] float jumpTime ;
    private float jumpTimeCounter ;
    bool isJumping;
   // [SerializeField] GameObject playerPrefab;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;
    bool canDoubleJump=false;
    public bool isAlive = true;
    [SerializeField] List<Transform> checkpoints;





    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }

 
    void Update()
    {
        if (!isAlive) { return;}
        Run();
        FlipRightSide();
        Jump();
       StartCoroutine(Die()) ;
       
        
    }


    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal");
        myRigidBody.velocity = new Vector2(speed*controlThrow, myRigidBody.velocity.y);
        bool runflag = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        
        myAnimator.SetBool("run", runflag);
    }

    private void Jump()
    {
        bool jumpflag = Mathf.Abs(myRigidBody.velocity.y) > 0.1f;
        myAnimator.SetBool("jumping", jumpflag);


        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            myRigidBody.velocity += new Vector2(myRigidBody.velocity.x, jumpSpeed);

        }
        if (Input.GetButton("Jump") && isJumping == true)
        {
            if (jumpTimeCounter > 0 )
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }


















            //if (Input.GetButtonDown("Jump"))
            //{

            if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")) && Input.GetButton("Jump"))
            {
                 if (jumpTimeCounter > 0)
                 {
                     myRigidBody.velocity = new Vector2(0, jumpSpeed);
                     jumpTimeCounter -= Time.deltaTime;
                 }

            
            }


            //if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            //{
            //    myRigidBody.velocity += new Vector2(0, jumpSpeed);
            //    canDoubleJump = true;
            //}
            //else if (canDoubleJump)
            //{
            //    if (myRigidBody.velocity.y < 0)
            //    {
            //        myRigidBody.velocity += new Vector2(0, jumpSpeed * 1.4f);
            //    }
            //    canDoubleJump = false;
            //}

        
       

    }

    IEnumerator Respawn()
    {
       
        myAnimator.SetBool("die", false);
        myRigidBody.transform.position = FindCheckpoint();
        yield return new WaitForSeconds(0.05f);
        isAlive = true;
    }


    IEnumerator Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetBool("die", true);
            myRigidBody.velocity = deathKick;
            yield return new WaitForSeconds(3);
            StartCoroutine(Respawn());

        }
    }

    Vector2 FindCheckpoint()
    {
        for (int i = checkpoints.Count-1; i >= 0; i--)
        {
            if (myRigidBody.transform.position.x >= checkpoints[i].transform.position.x)
            {
                return checkpoints[i].transform.position;
            }
        }
        return checkpoints[0].transform.position;


    }

    void FlipRightSide()
    {
        
        if (Mathf.Abs(myRigidBody.velocity.x)>Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
        }
    }

}


