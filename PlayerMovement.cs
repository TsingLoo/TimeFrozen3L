using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] FlowManager flowManager;
    [SerializeField] SoundPlayController spc;

    [Header("Audio")]
    public AudioClip jps0;
    public AudioClip jps1;
    AudioSource ads;
   // WallRun wr;
   // bool flag = false;


    public CapsuleCollider capsule;
    public Transform cam;


    [Header("Respawn")]
    [SerializeField] Transform respawn0;
    [SerializeField] Transform respawn1;
    [SerializeField] Transform respawn2;
    [SerializeField] Transform respawn3;
    public Transform respawnTr;
    int currentRespowan;

    [Header("Movement")]

    float playerHeight = 4f;

    [SerializeField] Transform orientation;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    
    [SerializeField] KeyCode DashKey = KeyCode.E;


    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 12f;
    [SerializeField] float acceleration = 10f;

    [Header("Crouching")]
    public bool isCrouched;
    [SerializeField] float crouchSpeed = 2.5f;
    [SerializeField] float originHeight = 2f;
    [SerializeField] float CrouchHeight = 1f;


    [Header("Jumping")]
    public float jumpForce = 5f;
    
    [SerializeField] float jumpMultiplier;

    [Header("Drag")]
    float groundGrag = 6f;
    float airDrag = 1f;

    [Header("Dash")]
    float dashSpeed = 15f;
    float dashTime = 0.12f;
    float chromaTo = 0.8f;
    [SerializeField] float DashCD = 0.8f;
    float times = 0;
    public  bool ableTodash = false;

    [Header("Camera")]
    [SerializeField] private Camera camR;
    [SerializeField] private float fov;
    [SerializeField] private float wallRunfov;
    [SerializeField] private float wallRunfovTime;
    [SerializeField] private float camTilt;
    [SerializeField] private float camTiltTime;


    public float moveSpeed = 6f;

    float multiplyMovement = 10f;
    [SerializeField] float airMultiplier = 0.4f;



    float horizontalMovement;
    float verticalMovement;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;
    bool isGrounded;


    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;


    private void ControlSpeed()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Start()
    {
       
        ads = GetComponent<AudioSource>();
        capsule = GetComponentInChildren<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }



    private void Update()
    {
        
        // Debug.Log( == getCameraDir());
        times += Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (flowManager.GameStage >= 0)
        {
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                Jump();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                var tmp_CurrentHeight = isCrouched ? originHeight : CrouchHeight;
                StartCoroutine(DoCrouch(tmp_CurrentHeight));
                isCrouched = !isCrouched;
            }


            if (times >= DashCD)
            {
                ableTodash = true;
                if (Input.GetKeyDown(DashKey))
                {

                    StartCoroutine(Dash());
                    ableTodash = false;
                    times = 0;
                   
                }
            }

        }
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);


        MyInput();
        ControlDrag();
        ControlSpeed();

    }

    
    void Jump()
    {
        
        if (isGrounded)
        {

            spc.JumpSound();
            StopCoroutine(TimeScaleControl.instance.ActionE(0.6f));
            StartCoroutine(TimeScaleControl.instance.ActionE(0.6f));
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce * jumpMultiplier, ForceMode.Impulse);
        }
    }

    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");//return 1 when pressing D key, return -1 when pressing A key
        verticalMovement = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;// moveDirection be relative to where the player is looking  
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundGrag;
        }
        else
        {
            rb.drag = airDrag;
        }

    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(flowManager.NextPlayer)) 
        {
            
            gameObject.transform.position = respawn0.position;
            respawnTr = respawn0;
            currentRespowan = 0;
            flowManager.needRespawn = false;
        }

        if (flowManager.GameStage >= 0)
        {
            
            MovePlayer();

            if (flowManager.needRespawn) 
            {
                if (flowManager.GameStage == 0) 
                {
                    gameObject.transform.position = respawn0.position;
                    respawnTr = respawn0;
                    flowManager.needRespawn = false;
                }
                if (flowManager.GameStage == 1)
                {
                    gameObject.transform.position = respawn1.position;
                    respawnTr = respawn1;
                    flowManager.needRespawn = false;
                }
                if (flowManager.GameStage == 2)
                {
                    gameObject.transform.position = respawn2.position;
                    respawnTr = respawn2;
                    flowManager.needRespawn = false;
                }
                if (flowManager.GameStage == 3)
                {
                    gameObject.transform.position = respawn3.position;
                    respawnTr = respawn3;
                    flowManager.needRespawn = false;
                }
            }
        }
    }

    void MovePlayer()
    {

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * multiplyMovement, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * multiplyMovement, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * multiplyMovement * airMultiplier, ForceMode.Acceleration);
        }

    }

    private IEnumerator DoCrouch(float _target)
    {
        float tmp_CurrentHeight = 0;
        Vector3 currentV = gameObject.GetComponent<Rigidbody>().velocity;
        while (Mathf.Abs(capsule.height - _target) > 0.1f)
        {
            yield return null;
            capsule.height = Mathf.SmoothDamp(capsule.height, _target, ref tmp_CurrentHeight, Time.deltaTime * 5);
            cam.position = new Vector3(cam.position.x,Mathf.SmoothDamp(cam.position.y, _target, ref tmp_CurrentHeight, Time.deltaTime * 50f),cam.position.z);

        }
        gameObject.GetComponent<Rigidbody>().velocity = currentV;

    }

    public Vector3 getCameraDir() 
    {
        Vector3 CameraDir = new Vector3(horizontalMovement, 0, verticalMovement);
        float y = camR.transform.rotation.eulerAngles.y;
        CameraDir = Quaternion.Euler(0, y, 0) * CameraDir;
        return CameraDir;
    }

    IEnumerator Dash() 
    {
        
        rb.useGravity = false;
        camR.fieldOfView = Mathf.Lerp(camR.fieldOfView, wallRunfov, dashTime/3 * Time.deltaTime);
        StopCoroutine(TimeScaleControl.instance.ActionE(dashTime));
        StartCoroutine(TimeScaleControl.instance.ActionE(dashTime));
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            rb.AddForce(getCameraDir().normalized * dashSpeed, ForceMode.Acceleration);
            camR.fieldOfView = Mathf.Lerp(camR.fieldOfView, fov, dashTime/3 * Time.deltaTime);

            yield return null;
        }
        rb.useGravity = true;
    }





}
