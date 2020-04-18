using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnSpeed = 1f;
    [SerializeField] private float rollSpeed = 0.1f;
    [SerializeField] private bool invertY = false;

    private Rigidbody rb;

    private PlayerActions playerInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = new PlayerActions();

        playerInput.Player.Move.performed += OnMove;
    }

    private void OnMove()
    {
        
    }

    void OnEnable()
    {
        // Lock the cursor and hide it.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        rb.AddRelativeTorque(GetRotation(), ForceMode.VelocityChange);
        rb.AddRelativeForce(GetDirection() * moveSpeed, ForceMode.VelocityChange);
    }

    Vector3 GetDirection()
    {
        // Create a movement direction vector based on keyboard input.
        var dir = new Vector3();
        if (Input.GetKey(KeyCode.W)) dir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) dir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) dir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir += Vector3.right;
        if (Input.GetKey(KeyCode.LeftControl)) dir += Vector3.down;
        if (Input.GetKey(KeyCode.Space)) dir += Vector3.up;
        return dir;
    }

    Vector3 GetRotation()
    {
        float yaw = Input.GetAxis("Mouse X");
        float pitch = Input.GetAxis("Mouse Y") * (invertY ? 1 : -1);
        float roll = 0;
        if (Input.GetKey(KeyCode.Q)) roll += 1;
        if (Input.GetKey(KeyCode.E)) roll -= 1;
        return new Vector3(pitch * turnSpeed, yaw * turnSpeed, roll * rollSpeed);
    }
}
