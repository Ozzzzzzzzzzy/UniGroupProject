using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartFishingScript : MonoBehaviour
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

        Debug.Log("Player interacted with fishing area, start fishing");
        StartFishing();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = true;
        Debug.Log("Player entered fishing area.");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInside = false;
        Debug.Log("Player exited fishing area.");
    }

    private void StartFishing()
    {
        SceneManager.LoadScene("FishingScene");
    }
}
