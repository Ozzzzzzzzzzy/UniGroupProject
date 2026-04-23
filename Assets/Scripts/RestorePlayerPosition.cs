using UnityEngine;

public class RestorePlayerPosition : MonoBehaviour
{
    private const string PlayerPosXKey = "World.Player.PosX";
    private const string PlayerPosYKey = "World.Player.PosY";
    private const string PlayerPosZKey = "World.Player.PosZ";
    private const string PlayerYawKey = "World.Player.Yaw";
    private const string PlayerPosValidKey = "World.Player.PosValid";

    private bool shouldRestore;
    private Vector3 restorePos;
    private float restoreYaw;

    private void Awake()
    {
        shouldRestore = PlayerPrefs.GetInt(PlayerPosValidKey, 0) == 1;
        if (!shouldRestore)
            return;

        restorePos = new Vector3(
            PlayerPrefs.GetFloat(PlayerPosXKey),
            PlayerPrefs.GetFloat(PlayerPosYKey),
            PlayerPrefs.GetFloat(PlayerPosZKey)
        );

        restoreYaw = PlayerPrefs.GetFloat(PlayerYawKey);
    }

    private void LateUpdate()
    {
        if (!shouldRestore)
            return;

        transform.position = restorePos;
        transform.rotation = Quaternion.Euler(0f, restoreYaw, 0f);

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        PlayerPrefs.DeleteKey(PlayerPosValidKey);
        PlayerPrefs.Save();

        shouldRestore = false;
        enabled = false;
    }
}
