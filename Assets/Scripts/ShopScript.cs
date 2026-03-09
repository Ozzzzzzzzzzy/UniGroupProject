using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;

public class ShopScript : MonoBehaviour
{
    private InputSystem_Actions input;
    private bool playerInside;
    private bool shopOpen = false;

    [SerializeField] private GameObject shopPanel;

    [SerializeField] private CurrencyManager currencyManager;
    [SerializeField] private UpgradeManager upgradeManager;

    [SerializeField] private TextMeshProUGUI baitLevelText;
    [SerializeField] private TextMeshProUGUI baitCostText;

    [SerializeField] private Behaviour cinemachineRotationControl;

    [Header("Player Control")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody playerRigidbody;

    private void Awake()
    {
        input = new InputSystem_Actions();
        shopPanel.SetActive(false);

        UpdateBaitUpgradeUI();
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

        if (shopOpen == true)
        {
            shopPanel.SetActive(false);
            shopOpen = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

                cinemachineRotationControl.enabled = true;
                playerMovement.enabled = true;
        }
        else
        {
            shopPanel.SetActive(true);
            shopOpen = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

                cinemachineRotationControl.enabled = false;
                playerMovement.enabled = false;
                playerRigidbody.linearVelocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            

            UpdateBaitUpgradeUI();
        }
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

        if (shopOpen == true)
        {
            shopPanel.SetActive(false);
            shopOpen = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (cinemachineRotationControl != null)
                cinemachineRotationControl.enabled = true;

            if (playerMovement != null)
                playerMovement.enabled = true;
        }
    }

    public void BaitUpgrade()
    {
        int level = UpgradeManager.LoadBaitUpgradeLevel();
        int cost = UpgradeManager.GetBaitUpgradeCost(level);

        if (currencyManager.TrySpend(cost))
            upgradeManager.UpgradeBait();

        UpdateBaitUpgradeUI();
    }

    private void UpdateBaitUpgradeUI()
    {
        int level = UpgradeManager.LoadBaitUpgradeLevel();
        int cost = UpgradeManager.GetBaitUpgradeCost(level);

        baitLevelText.text = "Bait Level: " + level;
        baitCostText.text = "Cost: " + cost;
    }
}
