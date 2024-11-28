using PGGE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    [HideInInspector]
    public CharacterController mCharacterController;
    public Animator mAnimator;

    public float mWalkSpeed = 1.5f;
    public float mRotationSpeed = 50.0f;
    public bool mFollowCameraForward = false;
    public float mTurnRate = 10.0f;

#if UNITY_ANDROID
    public FixedJoystick mJoystick;
#endif

    private float hInput;
    private float vInput;
    private float speed;
    //private bool jump = false;
    //private bool crouch = false;
    public float mGravity = -30.0f;
    public float mJumpHeight = 2.0f;

    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        mCharacterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleInputs();
        Move();
        
    }

    private void FixedUpdate()
    {
        ApplyGravity();
    }

    public void HandleInputs()
    {
        // We shall handle our inputs here.
#if UNITY_STANDALONE
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
#endif

#if UNITY_ANDROID
        hInput = 2.0f * mJoystick.Horizontal;
        vInput = 2.0f * mJoystick.Vertical;
#endif

        speed = mWalkSpeed;
        
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            mAnimator.SetBool("IsWalk", false);
            mAnimator.SetBool("IsWalkBack", false);
            speed = mWalkSpeed * 10.0f;
            mAnimator.SetBool("IsRunning",true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            mAnimator.SetBool("IsRunning", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            mAnimator.SetBool("IsJump", true);

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
                         
            mAnimator.SetBool("IsJump", false);
            


        }

        
        if (Input.GetKey(KeyCode.P))
        {
            mAnimator.SetBool("IsAttack", true);
        }

        if (Input.GetKeyUp(KeyCode.P)) 
        {
            mAnimator.SetBool("IsAttack",false);
        }
    }

    public void Move()
    {
        

        // We shall apply movement to the game object here.
        if (mAnimator == null) return;
        if (mFollowCameraForward)
        {
            // rotate Player towards the camera forward.
            Vector3 eu = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.RotateTowards(transform.rotation,Quaternion.Euler(0.0f, eu.y, 0.0f),mTurnRate * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0.0f, hInput * mRotationSpeed * Time.deltaTime, 0.0f);
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        mCharacterController.Move(forward * vInput * speed * Time.deltaTime);
        
        
        if (vInput > 0)
        {
            mAnimator.SetBool("IsWalk", true);
            mAnimator.SetBool("IsWalkBack", false);
        }
        
        else if (vInput < 0)
        {
            mAnimator.SetBool("IsWalkBack", true);
            mAnimator.SetBool("IsWalk", false);
        }

        else
        {
            mAnimator.SetBool("IsWalk", false);
            mAnimator.SetBool("IsWalkBack", false);
        }

        
    }

    

    void ApplyGravity()
    {
        // apply gravity.
        mVelocity.y += mGravity * Time.deltaTime;
        if (mCharacterController.isGrounded && mVelocity.y <= 0)
        {
            mVelocity.y = 0f;
        }
       

    }
}

