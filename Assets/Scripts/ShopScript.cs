using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ShopScript : MonoBehaviour
{
    private InputSystem_Actions input;
    private bool playerInside;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player.Enable();
        input.Player.Interact.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        input.Player.Interact.performed -= OnInteractPerformed;
        input.Player.Disable();
    }

    private void OnInteractPerformed(InputAction.CallbackContext _)
    {
        if (!playerInside)
            return;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;

    }

}
