using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed_Pitch = 5.0f;
    [SerializeField] private float Speed_Roll = 5.0f;
    [SerializeField] private float Speed_Yaw = 5.0f;
    [SerializeField] private float Speed_Vertical = 100.0f;
    [SerializeField] private float Speed_Lateral = 20.0f;

    [SerializeField] private float Lerp_Rotate = 50.0f;
    [SerializeField] private float Lerp_Move = 50.0f;

    private Rigidbody rb;
    private PlayerInputActions actions;

    private Camera cam;

    private bool Changing_Lateral;
    private bool Changing_Vertical;
    private bool Changing_Yaw;
    private bool Changing_PitchRoll;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        actions = new PlayerInputActions();
        cam = GetComponent<Camera>();

        actions.Enable();

        actions.Player.LookPitchRoll.started += ctx => BeginPitchRoll();
        actions.Player.LookPitchRoll.canceled += ctx => EndPitchRoll();
        actions.Player.LookPitchRoll.performed += ctx => EndPitchRoll();

        actions.Player.LookYaw.started += ctx => BeginYaw();
        actions.Player.LookYaw.canceled += ctx => EndYaw();
        actions.Player.LookYaw.performed += ctx => EndYaw();

        actions.Player.Lateral.started += ctx => BeginLateral();
        actions.Player.Lateral.canceled += ctx => EndLateral();
        actions.Player.Lateral.performed += ctx => EndLateral();

        actions.Player.Vertical.started += ctx => BeginVertical();
        actions.Player.Vertical.canceled += ctx => EndVertical();
        actions.Player.Vertical.performed += ctx => EndVertical();
    }

    private void FixedUpdate()
    {
        if(Changing_PitchRoll)
        {
            HandlePitchRoll();
        }
        if(Changing_Yaw)
        {
            HandleYaw();
        }
        if(Changing_Lateral)
        {
            HandleLateral();
        }
        if(Changing_Vertical)
        {
            HandleVertical();
        }
    }

    internal void BeginPitchRoll()
    {
        Changing_PitchRoll = true;
    }

    internal void EndPitchRoll()
    {
        Changing_PitchRoll = false;
    }

    internal void HandlePitchRoll()
    {
        

        Vector2 Look_PitchRoll_Vector = actions.Player.LookPitchRoll.ReadValue<Vector2>().normalized;

        float roll = Look_PitchRoll_Vector.x;
        float pitch = Look_PitchRoll_Vector.y;
        

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch * Speed_Pitch * Time.fixedDeltaTime, roll * Speed_Roll * Time.fixedDeltaTime, 0));

        Quaternion targetRotation = Quaternion.Slerp(rb.rotation, rb.rotation * deltaRotation, Time.fixedDeltaTime * Lerp_Rotate);

        rb.MoveRotation(targetRotation);
    }

    internal void BeginYaw()
    {
        Changing_Yaw = true;
    }

    internal void EndYaw()
    {
        Changing_Yaw = false;
    }

    internal void HandleYaw()
    {
        float Look_Yaw_Axis = actions.Player.LookYaw.ReadValue<float>();

        float yaw = Look_Yaw_Axis;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, -1 * yaw * Speed_Yaw * Time.fixedDeltaTime));

        Quaternion targetRotation = Quaternion.Slerp(rb.rotation, rb.rotation * deltaRotation, Time.fixedDeltaTime * Lerp_Rotate);

        rb.MoveRotation(targetRotation);
    }

    internal void BeginLateral()
    {
        Changing_Lateral = true;
    }

    internal void EndLateral()
    {
        Changing_Lateral = false;
    }

    internal void HandleLateral()
    {
        Vector2 Lateral_Vector = actions.Player.Lateral.ReadValue<Vector2>().normalized;

        Vector3 deltaPosition = new Vector3(Lateral_Vector.x * Speed_Lateral * Time.fixedDeltaTime,0 , Lateral_Vector.y * Speed_Lateral * Time.fixedDeltaTime);

        Vector3 targetPosition = Vector3.Slerp(rb.position, rb.position + cam.transform.TransformDirection(deltaPosition), Time.fixedDeltaTime * Lerp_Move);

        rb.MovePosition(targetPosition);
    }

    internal void BeginVertical()
    {
        Changing_Vertical = true;
    }

    internal void EndVertical()
    {
        Changing_Vertical = false;
    }

    internal void HandleVertical()
    {
        float Vertical_Axis = actions.Player.Vertical.ReadValue<float>();

        Vector3 deltaPosition = new Vector3(0, Vertical_Axis * Speed_Vertical * Time.fixedDeltaTime, 0);

        Vector3 targetPosition = Vector3.Slerp(rb.position, rb.position + cam.transform.TransformDirection(deltaPosition), Time.fixedDeltaTime * Lerp_Move);

        rb.MovePosition(targetPosition);
    }
}
