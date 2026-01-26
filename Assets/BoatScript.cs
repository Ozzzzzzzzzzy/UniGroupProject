using UnityEngine;
using UnityEngine.InputSystem;

public class BoatScript : MonoBehaviour
{


    public float moveSpeed = 5f;
    private InputSystem_Actions inputActions;
    private Rigidbody rb;
    private float moveInput;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Boat.Enable();
    }

    void OnDisable()
    {
        inputActions.Boat.Disable();
    }


     void Start()
    {
        
    }

    void Update()
    {
        moveInput = inputActions.Boat.Move.ReadValue<float>();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveInput * moveSpeed, 0, 0);

    }
}
