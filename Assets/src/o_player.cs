using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class o_player : o_character
{
    public float speed = 6f;
    public float speedSmoothTime = 0.1f;
    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;
    float speedSmoothVelocity;
    public float gravity = -12;
    float velocityY;
    float currentSpeed;
    [Range(0, 1)]
    public float airControlPercent;

    public Vector3 movedirFin;
     Vector3 oldDir;
    public Vector3 facingDirection { get; set; }
    //public Vector3 direction { get; private set; }
    public enum CHARACTER_STATE {
        IDLE,
        MOVING
    }
    public CHARACTER_STATE charState;

    CharacterController charContr;
    public Transform camT;
    public Transform groundDetSphere;
    public float groundDist = 0.5f;
    public LayerMask groundMask;
    public bool isControl;

    public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //direction = Vector3.right;
        camT = Camera.main.transform;
        charContr = GetComponent<CharacterController>();
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        Vector3 direction = new Vector3();

        if (isControl)
        {
            float horiz = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (Input.GetKey(s_globals.GetKeyPref("left")))
            {
                horiz = -1;
            }
            else if (Input.GetKey(s_globals.GetKeyPref("right")))
            {
                horiz = 1;
            }
            else {
                horiz = 0;
            }
            
            if (Input.GetKey(s_globals.GetKeyPref("down")))
            {
                vertical = -1;
            }
            else if (Input.GetKey(s_globals.GetKeyPref("up")))
            {
                vertical = 1;
            }
            else
            {
                vertical = 0;
            }

            if (horiz == 0 && vertical == 0)
            {
                movedirFin = new Vector3(0, movedirFin.y, 0);
            }
            direction = new Vector3(horiz, 0f, vertical).normalized;
        }
        if (direction.magnitude > 0)
        {
            anim.SetBool("isWalk", true);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
        if (direction.magnitude >= 0.1f)
        {
            float targAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camT.eulerAngles.y;

            float speedMult = 1;
            if (!charContr.isGrounded)
                speedMult = 0.85f;

            float turnTimeMult = 1f;
            if (!charContr.isGrounded)
                turnTimeMult = 2.85f;

            float ang = Mathf.SmoothDampAngle(transform.eulerAngles.y, targAngle, ref turnSmoothVelocity, (turnSmoothTime * turnTimeMult));
            transform.rotation = Quaternion.Euler(0f, ang, 0f);

            Vector3 movedir = Quaternion.Euler(0f, targAngle, 0f) * Vector3.forward;
            movedirFin = new Vector3(movedir.normalized.x * (speed * speedMult), movedirFin.y, movedir.normalized.z * (speed * speedMult)) ;
        }        

        //bool checkSphere = Physics.CheckSphere(groundDetSphere.position, groundDist, groundMask);
        if (charContr.isGrounded)
        {
            anim.SetBool("isJump", false);
            anim.SetBool("isFall", false);
            movedirFin.y = -1;
            if (Input.GetKeyDown(s_globals.GetKeyPref("jump")))
            {
                Jump();
            }
        }
        else
        {
            anim.SetBool("isFall", true);
            movedirFin.y += gravity * Time.deltaTime;
        }
        
        charContr.Move(movedirFin * Time.deltaTime);
        if (transform.position.y < -30)
        {
            movedirFin.y = 0;
            s_camera.GetInstance().GetCamToPlayer();
            s_camera.GetInstance().LockOnPlayer();
            transform.position = s_globals.GetInstance().spawnPoint;
        }
    }
    /*
    public void Move(Vector2 inputDir) {
        if (inputDir != Vector2.zero) {
            float targetRot = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + camT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRot, ref turnSmoothVelocity, turnSmoothTime);
        }
        velocityY += Time.deltaTime * gravity;
        float targetSpeed = speed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

        Vector3 vel = transform.forward * currentSpeed + Vector3.up * velocityY;
        charContr.Move(vel * Time.deltaTime);

        currentSpeed = new Vector2(charContr.velocity.x, charContr.velocity.z).magnitude;
        if (charContr.isGrounded) {
            velocityY = 0;
        }
    }
    */

    public void Jump()
    {
        anim.SetBool("isJump", true);
        movedirFin.y = 7;
    }

    float GetModifiedSmoothTime(float smoothTime)
    {
        if (charContr.isGrounded)
        {
            return smoothTime;
        }

        if (airControlPercent == 0)
        {
            return float.MaxValue;
        }
        return smoothTime / airControlPercent;
    }
}
