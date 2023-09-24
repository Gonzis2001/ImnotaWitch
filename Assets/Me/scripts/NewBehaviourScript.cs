
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;

    public float apuntado;
    public bool apuntadoMirar;
    public GameObject mirador;
    public Vector3 miradorF;

    private Vector3 platerInput;

    public CharacterController player;
    public float speed;
    public float gravity = 9.8f;
    public float fallVelocity;
    public float JumpForce;
    public bool salto2= false;

    public Camera mainCamera;
    private Vector3 camFoward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    public bool isOnSlope=false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    public GameObject freeCamm;
    public GameObject aimCamm;

    public Animator PlayerAnimator;

   [SerializeField] private Transform debugTransform;
    [SerializeField] private LayerMask aimcolliderMask = new LayerMask();

   

     private const float _threshold = 0.01f;
     // cinemachine
     private float _cinemachineTargetYaw;
     private float _cinemachineTargetPitch;
     public float lookSensibilyty = 1f;
     public float TopClamp = 70.0f;

    public float CameraAngleOverride = 0.0f;

    [Tooltip("For locking the camera position on all axis")]
    public bool LockCameraPosition = false;



    public float BottomClamp = -30.0f;
    public GameObject CinemachineCameraTarget;
   
    private PlayerInput _playerInput;

    private StrarterAssestsInputs strarterAssestsInputs;

    public Transform BulletPosition;


    public CinemachineVirtualCamera vcam;
    [SerializeField] private float  mirado;

    private bool agarrado=false;
    private float gravityGuardado;
    private float speedGuardado;

   
    private bool IsCurrentDeviceMouse
    {
        get
        {
#if ENABLE_INPUT_SYSTEM
            return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        PlayerAnimator= GetComponent<Animator>();
      
        strarterAssestsInputs=GetComponent<StrarterAssestsInputs>();

        freeCamm.SetActive(true);
        aimCamm.SetActive(false);
        apuntadoMirar=false;

        _playerInput = GetComponent<PlayerInput>();


        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        platerInput = new Vector3(horizontalMove, 0, verticalMove);
        platerInput.Normalize();

        PlayerAnimator.SetFloat("Axis x", horizontalMove);
        PlayerAnimator.SetFloat("Axis y", verticalMove);

        PlayerAnimator.SetFloat("PlayerVElocity", platerInput.magnitude * speed);

        CamDirection();
        movePlayer = platerInput.x * camRight + platerInput.z * camFoward;

        movePlayer = movePlayer * speed;
        if (freeCamm.activeInHierarchy == true)
        {
            player.transform.LookAt(player.transform.position + movePlayer);
        }
        SetGravity();
        Playerskills();
     
            Apuntar();
        
        player.Move(movePlayer * Time.deltaTime);
        if (freeCamm.activeInHierarchy != true)
        {
            Cambiodebrazo();

        }
    }
    private void LateUpdate()
    {
        if (freeCamm.activeInHierarchy != true)
        {
            
            CameraRotation();
        }
       
        
    }

    private void FixedUpdate()
    {
    }
    void CamDirection()
    {
      
            camFoward = mainCamera.transform.forward;
            camRight = mainCamera.transform.right;
            camFoward.y = 0; camRight.y = 0;
            camFoward.Normalize();
            camRight.Normalize();
    }
    void Playerskills()
    {
        if (player.isGrounded)
        {
            salto2 = true;
        }
        if(player.isGrounded&&( Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Joystick1Button0)))
         {
            
            fallVelocity = JumpForce;
            movePlayer.y = fallVelocity;
            PlayerAnimator.SetTrigger("Jump");
        }

        if (player.isGrounded== false && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && salto2== true)
        {
            salto2= false;
            fallVelocity = JumpForce;
            movePlayer.y = fallVelocity;
            
        }
       
    }

    void SetGravity()
    {
        if(player.isGrounded)
        {
           fallVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;

        }
        else
        {
           fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallVelocity;
            PlayerAnimator.SetFloat("PlayerVeticalVelocity", player.velocity.y);
        }
        PlayerAnimator.SetBool("Isgrounded",player.isGrounded);
        SlideDown();
    }
    

    public void SlideDown()
    {
        isOnSlope = Vector3.Angle(Vector3.up,hitNormal)>=player.slopeLimit;
       if (isOnSlope)
        {
            movePlayer.x += (1f-hitNormal.y)* hitNormal.x * slideVelocity;
            movePlayer.z += (1f - hitNormal.y) * hitNormal.z * slideVelocity;
            movePlayer.y -= slopeForceDown;
        }
    }

    void Apuntar()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector3 mouseWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        


     
        if (strarterAssestsInputs.aim)
        {
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimcolliderMask))
            {
                debugTransform.position = raycastHit.point;
                mouseWorldPosition = raycastHit.point;
            }
            if (apuntadoMirar == false)
            {
                miradorF = new Vector3(mirador.transform.position.x, player.transform.position.y, mirador.transform.position.z);


                mirado = Vector3.Angle(new Vector3(0, 0, 1), miradorF);
                
                if(player.transform.position.x > mainCamera.transform.position.x)
                {
                    _cinemachineTargetYaw = mirado;
                }
                else if (player.transform.position.x< mainCamera.transform.position.x)
                {
                    _cinemachineTargetYaw =-mirado;

                }
                print(_cinemachineTargetYaw);
                
                _cinemachineTargetPitch = 0;

                player.transform.LookAt(miradorF);
                apuntadoMirar = true;
            }

           

           
            
            

            
            BulletPosition.rotation=mainCamera.transform.rotation;




            Vector3 worldAimTarget = mouseWorldPosition;
           
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
          
          transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

            freeCamm.SetActive(false);
            aimCamm.SetActive(true);



        }
     
       else
        {
          
            freeCamm.SetActive(true);
            aimCamm.SetActive(false);




            apuntadoMirar = false;
            BulletPosition.rotation =transform.rotation;

            
            CinemachineCameraTarget.transform.LookAt(mouseWorldPosition);
        }
        PlayerAnimator.SetBool("CameraAImOn", aimCamm.activeInHierarchy);
    }

   


    private void OnAnimatorMove()
    {
        
    }

    private void Cambiodebrazo()
    {
        //no funciona 
        var aimcameraVirtual = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            
            if (aimcameraVirtual.CameraSide >=1)
            {
                aimcameraVirtual.CameraSide = 0;
            }
            if (aimcameraVirtual.CameraSide <= 0)
            {
                aimcameraVirtual.CameraSide = 1;
            }
        }
    }
    
   




    private void CameraRotation()
    { 
        if(strarterAssestsInputs.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

            _cinemachineTargetYaw+= strarterAssestsInputs.look.x * lookSensibilyty * deltaTimeMultiplier;
            _cinemachineTargetPitch += strarterAssestsInputs.look.y * lookSensibilyty * deltaTimeMultiplier;

        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

       
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
            _cinemachineTargetYaw, 0.0f);







    }
    


        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit2)
    {
        hitNormal = hit2.normal;
    }
    

}


