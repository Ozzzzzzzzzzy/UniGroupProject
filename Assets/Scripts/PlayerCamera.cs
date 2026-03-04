using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerCamera : MonoBehaviour
{
    [Header("Sensitivity")]
    public float sensX = 0.5f;
    public float sensY = 0.5f;

    [Header("References")]
    public Transform orientation;
    public Transform cameraHolder;

    private InputSystem_Actions inputActions;
    private float xRotation;
    private float yRotation;

    void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        //get mouseinput
        Vector2 lookInput = inputActions.Player.Look.ReadValue<Vector2>();
        float mouseX = lookInput.x * sensX;
        float mouseY = lookInput.y * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        //clamp looking up and down
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate cam and orientation
        cameraHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}