using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class StartFishingScript : MonoBehaviour
{
    private const string PlayerPosXKey = "World.Player.PosX";
    private const string PlayerPosYKey = "World.Player.PosY";
    private const string PlayerPosZKey = "World.Player.PosZ";
    private const string PlayerYawKey = "World.Player.Yaw";
    private const string PlayerPosValidKey = "World.Player.PosValid";

    private InputSystem_Actions input;
    private bool playerInside;
    private Transform playerTransform;

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
        if (!playerInside || playerTransform == null)
            return;

        SavePlayerWorldTransform(playerTransform);
        SceneManager.LoadScene("FishingScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        playerTransform = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        playerTransform = null;
    }

    private static void SavePlayerWorldTransform(Transform player)
    {
        Vector3 p = player.position;

        PlayerPrefs.SetFloat(PlayerPosXKey, p.x);
        PlayerPrefs.SetFloat(PlayerPosYKey, p.y);
        PlayerPrefs.SetFloat(PlayerPosZKey, p.z);
        PlayerPrefs.SetFloat(PlayerYawKey, player.eulerAngles.y);
        PlayerPrefs.SetInt(PlayerPosValidKey, 1);
        PlayerPrefs.Save();
    }
}
