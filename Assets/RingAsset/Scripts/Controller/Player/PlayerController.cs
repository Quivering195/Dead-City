using System.Collections;
using Ring;
using UnityEngine;

public class PlayerController : RingSingleton<PlayerController>
{
    public PlayerComponent playerComponent;
    public PlayerMovement playerMovement;
    [Header("Cinemachine")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    public GameObject CinemachineCameraTarget;
    // cinemachine
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _threshold = 0.01f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -30.0f;
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 70.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        Movement();
    }
    private void LateUpdate()
    {
        CameraRotation();
    }
    private void CameraRotation()
    {
        // if there is an input and camera position is not fixed
        if (transform.forward.sqrMagnitude >= _threshold)
        {
            //Don't multiply mouse input by Time.deltaTime;
            float deltaTimeMultiplier =  Time.deltaTime;

            _cinemachineTargetYaw += transform.forward.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += transform.forward.y * deltaTimeMultiplier;
        }

        // clamp our rotations so our values are limited 360 degrees
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        // Cinemachine will follow this target
        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + 0,
            _cinemachineTargetYaw, 0.0f);
    }
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
    #region Move
    private void Movement()
    {
        playerMovement._moveX = Input.GetAxis("Horizontal");
        playerMovement._moveY = Input.GetAxis("Vertical");
        playerMovement._direction = transform.forward * playerMovement._moveY + transform.right * playerMovement._moveX;
        playerComponent._rigidbody.velocity = playerMovement._direction * playerMovement._speed * Time.deltaTime;
    }
    #endregion
}
