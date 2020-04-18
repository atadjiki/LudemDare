using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed_Pitch = 5.0f;
    [SerializeField] private float Speed_Roll = 5.0f;
    [SerializeField] private float Speed_Yaw = 5.0f;
    [SerializeField] private float Speed_Vectical = 100.0f;
    [SerializeField] private float Speed_Strafe = 20.0f;

    [SerializeField] private float Rotation_Lerp_Speed = 50.0f;

    private Rigidbody rb;
    private PlayerInputActions actions;

    private Vector2 lookVector;
    private Vector2 moveVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        actions = new PlayerInputActions();

        lookVector = Vector2.zero;
        moveVector = Vector2.zero;

        actions.Enable();
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        lookVector = actions.Player.Look.ReadValue<Vector2>();
        moveVector = actions.Player.Move.ReadValue<Vector2>();

        float roll = lookVector.x;
        float pitch = lookVector.y;
        // float yaw = lookVector.y;
        // rb.AddRelativeForce(0, 0, power * Speed_Vectical * Time.deltaTime);

        Vector3 strafe = new Vector3(moveVector.x * Speed_Strafe * Time.fixedDeltaTime, 0, moveVector.y * Speed_Strafe * Time.fixedDeltaTime);

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch * Speed_Pitch * Time.fixedDeltaTime, roll * Speed_Roll * Time.fixedDeltaTime, 0));

        Quaternion targetRotation = Quaternion.Lerp(rb.rotation, rb.rotation * deltaRotation, Time.fixedDeltaTime * Rotation_Lerp_Speed);

        rb.MoveRotation(targetRotation);
        rb.AddRelativeForce(strafe);
    }
}
