using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ShopScript : MonoBehaviour
{
    private InputSystem_Actions input;
    private bool playerInside;
    [SerializeField] private GameObject shopPanel;

    private void Awake()
    {
        input = new InputSystem_Actions();
        shopPanel.SetActive(false);
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
