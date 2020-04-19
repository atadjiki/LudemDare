using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Camera))]
public class PlayerController : MonoBehaviour
{
    private static PlayerController _instance;

    [SerializeField] private float Speed_Pitch = 50;
    [SerializeField] private float Speed_Roll = 75;
    [SerializeField] private float Speed_Yaw = 50;
    [SerializeField] private float Speed_Vertical = 10;
    [SerializeField] private float Speed_Lateral = 10;

    [SerializeField] private float Lerp_Rotate = 15;
    [SerializeField] private float Lerp_Move = 15;

    public GameObject grabbableOffset;
    [SerializeField] private float interactDistance = 1.0f;
    [SerializeField] private float centerLerp = 0.01f;

    private bool isHolding = false;
    private Grabbable currentlyGrabbed = null;

    private Rigidbody Player_Rigidbody;
    private PlayerInputActions Actions;
    private Camera PlayerCamera;

    private Vector3 PreviousPosition;

    public static PlayerController Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        Build();
    }

    internal void Build()
    {
        Player_Rigidbody = GetComponent<Rigidbody>();
        Actions = new PlayerInputActions();
        PlayerCamera = GetComponent<Camera>();

        Cursor.visible = false;

        Actions.Player.Interact.performed += ctx => Interact();

        Actions.Enable();
    }

    private void FixedUpdate()
    {
        PreviousPosition = Player_Rigidbody.position;

        HandlePitchRoll();
        HandleYaw();
        HandleLateral();
        HandleVertical();

        if (isHolding)
        {
            if (currentlyGrabbed.gameObject.GetComponent<Rigidbody>() != null)
            {
                currentlyGrabbed.transform.localPosition = Vector3.Lerp(currentlyGrabbed.transform.localPosition, Vector3.zero, centerLerp);
              //  currentlyGrabbed.transform.localRotation = Quaternion.Lerp(currentlyGrabbed.transform.localRotation, Quaternion.identity, centerLerp);
              //  currentlyGrabbed.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                currentlyGrabbed.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        else if (UIManager.Instance.SubtitlesShowing())
        {
            UIManager.Instance.SetDefault();
        }
        else
        {
            Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.GetComponent<Interactable>() != null && Vector3.Distance(this.transform.position, hit.transform.position) <= interactDistance)
                {
                    UIManager.Instance.SetObjectInRange();
                }
                else
                {
                    UIManager.Instance.SetDefault();
                }
            }
            else
            {
                UIManager.Instance.SetDefault();
            }
        }

        Player_Rigidbody.velocity = Player_Rigidbody.velocity * 0.75f;
    }

    public bool IsHoldingObject(GameObject obj)
    {
        if(isHolding && ReferenceEquals(obj, currentlyGrabbed.gameObject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal void Interact()
    {

        // Shoot raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 50) && isHolding == false)
        {

            if (hit.transform.gameObject.GetComponent<Interactable>() != null && Vector3.Distance(this.transform.position, hit.transform.position) <= interactDistance)
            {
                if (hit.transform.gameObject.GetComponent<Grabbable>() != null)
                {
                    if (isHolding == false)
                    {
                        Grab(hit.transform.gameObject.GetComponent<Grabbable>());
                    }

                }
                else if (hit.transform.gameObject.GetComponent<CharacterInteraction>())
                {
                    hit.transform.gameObject.GetComponent<CharacterInteraction>().Interact();
                }
                else if(hit.transform.gameObject.GetComponent<Boombox>())
                {
                    hit.transform.gameObject.GetComponent<Boombox>().Interact();
                }
                else if(hit.transform.gameObject.GetComponent<Door>())
                {
                    hit.transform.gameObject.GetComponent<Door>().Interact();
                }


            }
        }
        else if (isHolding)
        {
            Release();
        }
    }

    internal void Grab(Grabbable target)
    {
        isHolding = true;
        currentlyGrabbed = target;

        if (currentlyGrabbed.gameObject.GetComponent<Rigidbody>() != null)
        {
         //   currentlyGrabbed.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        currentlyGrabbed.transform.SetParent(grabbableOffset.transform);
        UIManager.Instance.SetObjectGrabbed();
    }

    internal void Release()
    {
        if(currentlyGrabbed != null)
        {
            currentlyGrabbed.transform.SetParent(null);

            Vector3 childVelocity = (Player_Rigidbody.position - PreviousPosition) / Time.fixedDeltaTime;  // velocity = dist/time

            if (currentlyGrabbed.gameObject.GetComponent<Rigidbody>() != null)
            {
                currentlyGrabbed.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                currentlyGrabbed.gameObject.GetComponent<Rigidbody>().velocity = childVelocity;
            }

            currentlyGrabbed = null;
        }
        
        isHolding = false;
        UIManager.Instance.SetObjectReleased();
    }

    internal void HandlePitchRoll()
    {
        Vector2 Look_PitchRoll_Vector = Actions.Player.LookPitchRoll.ReadValue<Vector2>();

        float roll = Look_PitchRoll_Vector.x;
        float pitch = Look_PitchRoll_Vector.y;
        

        Quaternion deltaRotation = Quaternion.Euler(new Vector3(pitch * Speed_Pitch * Time.fixedDeltaTime, roll * Speed_Roll * Time.fixedDeltaTime, 0));

        Quaternion targetRotation = Quaternion.Slerp(Player_Rigidbody.rotation, Player_Rigidbody.rotation * deltaRotation, Time.fixedDeltaTime * Lerp_Rotate * Look_PitchRoll_Vector.magnitude);

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

        Vector3 targetPosition = Vector3.Slerp(Player_Rigidbody.position, Player_Rigidbody.position + PlayerCamera.transform.TransformDirection(deltaPosition), Time.fixedDeltaTime * Lateral_Vector.magnitude * Lerp_Move);

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
