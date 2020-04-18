using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float Speed_Pitch = 5.0f;
    [SerializeField] private float Speed_Roll = 5.0f;
    [SerializeField] private float Speed_Yaw = 5.0f;
    [SerializeField] private float Speed_Vectical = 100.0f;
    [SerializeField] private float Speed_Horizontal = 20.0f;

    private Rigidbody rb;
    private PlayerInputActions actions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        actions = new PlayerInputActions();
    }

    void FixedUpdate()
    {
        Vector3 lookVector = actions.Player.Look.ReadValue<Vector3>();
        float roll = lookVector.x;
        float pitch = lookVector.y;
        float yaw = lookVector.z;

        Vector2 moveVector = actions.Player.Move.ReadValue<Vector2>();

        Vector3 strafe = new Vector3(moveVector.x * Speed_Horizontal * Time.deltaTime, moveVector.y * Speed_Horizontal * Time.deltaTime, 0);


        rb.AddRelativeTorque(pitch * Speed_Pitch * Time.deltaTime, yaw * Speed_Yaw * Time.deltaTime, roll * Speed_Roll * Time.deltaTime);
       // rb.AddRelativeForce(0, 0, power * Speed_Vectical * Time.deltaTime);
        rb.AddRelativeForce(strafe);
    }
}
