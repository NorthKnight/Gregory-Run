using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartMenuPlayer : MonoBehaviour
{


    [SerializeField] float jumpSpeed = 10f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeet;





    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeet = GetComponent<BoxCollider2D>();
    }


    void Update() {Jump();}


    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                myRigidBody.velocity += new Vector2(0, jumpSpeed); 
            }
        }
    }

}