using UnityEngine;

public class InfoPanelScript : MonoBehaviour
{
    public bool IsInfoPanelOpen = true;
    public GameObject InfoPanel;

    [SerializeField] private Behaviour cinemachineRotationControl;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Rigidbody playerRigidbody;

    private void Start()
    {
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
}
