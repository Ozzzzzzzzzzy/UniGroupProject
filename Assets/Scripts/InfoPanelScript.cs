using UnityEngine;

public class InfoPanelScript : MonoBehaviour
{
    private const string InfoPanelSeenKey = "UI.InfoPanelSeen";

    public bool IsInfoPanelOpen = true;
    public GameObject InfoPanel;

    [SerializeField] private Behaviour cinemachineRotationControl;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody playerRigidbody;

    private void Start()
    {
        bool hasSeen = PlayerPrefs.GetInt(InfoPanelSeenKey, 0) == 1;

        IsInfoPanelOpen = !hasSeen;
        SetInfoPanelOpen(IsInfoPanelOpen);
    }

    private void Update()
    {
        if (IsInfoPanelOpen == true)
            SetInfoPanelOpen(true);
    }

    public void closeinfopanel()
    {
        IsInfoPanelOpen = false;

        PlayerPrefs.SetInt(InfoPanelSeenKey, 1);
        PlayerPrefs.Save();

        SetInfoPanelOpen(false);
    }

    private void SetInfoPanelOpen(bool open)
    {
        if (InfoPanel != null)
            InfoPanel.SetActive(open);

        if (open)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (cinemachineRotationControl != null)
                cinemachineRotationControl.enabled = false;

            if (playerMovement != null)
                playerMovement.enabled = false;

            if (playerRigidbody != null)
            {
                playerRigidbody.linearVelocity = Vector3.zero;
                playerRigidbody.angularVelocity = Vector3.zero;
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (cinemachineRotationControl != null)
                cinemachineRotationControl.enabled = true;

            if (playerMovement != null)
                playerMovement.enabled = true;
        }
    }

    [ContextMenu("Debug/Reset InfoPanel Seen Flag")]
    private void DebugResetSeenFlag()
    {
        PlayerPrefs.DeleteKey(InfoPanelSeenKey);
        PlayerPrefs.Save();
        Debug.Log("[InfoPanelScript] Reset InfoPanelSeen flag.");
    }
}
