using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed_Pitch = 50;
    [SerializeField] private float Speed_Roll = 75;
    [SerializeField] private float Speed_Yaw = 50;
    [SerializeField] private float Speed_Vertical = 10;
    [SerializeField] private float Speed_Lateral = 10;

    private float Lerp_Rotate = 50;
    private float Lerp_Move = 50;

    private Rigidbody Player_Rigidbody;
    private PlayerInputActions Actions;
    private Camera PlayerCamera;

    private void Awake()
    {
        Player_Rigidbody = GetComponent<Rigidbody>();
        Actions = new PlayerInputActions();
        PlayerCamera = GetComponent<Camera>();

        Actions.Enable();
    }

    private void FixedUpdate()
    {
        HandlePitchRoll();
        HandleYaw();
        HandleLateral();
        HandleVertical();
    }

    internal void HandlePitchRoll()
    {
        Vector2 Look_PitchRoll_Vector = Actions.Player.LookPitchRoll.ReadValue<Vector2>().normalized;

        float roll = Look_PitchRoll_Vector.x;
        float pitch = Look_PitchRoll_Vector.y;
        

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch * Speed_Pitch * Time.fixedDeltaTime, roll * Speed_Roll * Time.fixedDeltaTime, 0));

        Quaternion targetRotation = Quaternion.Slerp(Player_Rigidbody.rotation, Player_Rigidbody.rotation * deltaRotation, Time.fixedDeltaTime * Lerp_Rotate);

        Player_Rigidbody.MoveRotation(targetRotation);
    }

    internal void HandleYaw()
    {
        float Look_Yaw_Axis = Actions.Player.LookYaw.ReadValue<float>();

        float yaw = Look_Yaw_Axis;

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 0, -1 * yaw * Speed_Yaw * Time.fixedDeltaTime));

        Quaternion targetRotation = Quaternion.Slerp(Player_Rigidbody.rotation, Player_Rigidbody.rotation * deltaRotation, Time.fixedDeltaTime * Lerp_Rotate);

        Player_Rigidbody.MoveRotation(targetRotation);
    }

    internal void HandleLateral()
    {
        Vector2 Lateral_Vector = Actions.Player.Lateral.ReadValue<Vector2>();

        Vector3 deltaPosition = new Vector3(Lateral_Vector.x * Speed_Lateral * Time.fixedDeltaTime,0 , Lateral_Vector.y * Speed_Lateral * Time.fixedDeltaTime);

        Vector3 targetPosition = Vector3.Slerp(Player_Rigidbody.position, Player_Rigidbody.position + PlayerCamera.transform.TransformDirection(deltaPosition), Time.fixedDeltaTime * Lerp_Move);

        Player_Rigidbody.MovePosition(targetPosition);
    }

    internal void HandleVertical()
    {
        float Vertical_Axis = Actions.Player.Vertical.ReadValue<float>();

        Vector3 deltaPosition = new Vector3(0, Vertical_Axis * Speed_Vertical * Time.fixedDeltaTime, 0);

        Vector3 targetPosition = Vector3.Slerp(Player_Rigidbody.position, Player_Rigidbody.position + PlayerCamera.transform.TransformDirection(deltaPosition), Time.fixedDeltaTime * Lerp_Move);

        Player_Rigidbody.MovePosition(targetPosition);
    }
}
