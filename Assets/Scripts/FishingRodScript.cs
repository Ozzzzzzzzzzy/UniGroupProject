using UnityEngine;
using UnityEngine.InputSystem;

public class FishingRod : MonoBehaviour
{
    [SerializeField] private float TopY = 4f;
    [SerializeField] private float BottomY = -4f;
    [SerializeField] private float MoveSpeed = 6f;
    [SerializeField] private Transform Hook;

    [SerializeField] private ScoreManager ScoreManager;

    private enum State
    {
        IdleAtTop,
        Dropping,
        Raising
    }

    public bool CanCatch => state == State.Dropping;

    private State state = State.IdleAtTop;
    private BaseHuman caughtHuman;

    private InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Fishing.DropRod.performed += OnDropPerformed;
    }

    private void OnDisable()
    {
        input.Fishing.DropRod.performed -= OnDropPerformed;
        input.Disable();
    }

    private void Start()
    {
        Vector3 p = Hook.position;
        p.y = TopY;
        Hook.position = p;
    }

    private void OnDropPerformed(InputAction.CallbackContext _)
    {
        if (state == State.IdleAtTop)
            state = State.Dropping;
    }

    private void Update()
    {
        if (state == State.Dropping)
        {
            MoveHookTowardsY(BottomY);

            if (Mathf.Abs(Hook.position.y - BottomY) < 0.01f)
                state = State.Raising;
        }
        else if (state == State.Raising)
        {
            MoveHookTowardsY(TopY);

            if (Mathf.Abs(Hook.position.y - TopY) < 0.01f)
            {
                state = State.IdleAtTop;

                if (caughtHuman != null)
                {
                    int baitLevel = PlayerPrefs.GetInt(UpgradeManager.UpgradeData, 1);

                    float multiplier = baitLevel switch
                    {
                        1 => 1f,
                        2 => 1.5f,
                        3 => 2f,
                        4 => 3f,
                        _ => 1f
                    };

                    int addScore = Mathf.RoundToInt(caughtHuman.Data.Value * multiplier);
                    ScoreManager.CurrentScore += addScore;

                    Destroy(caughtHuman.gameObject);
                    caughtHuman = null;
                }
            }
        }
    }

    private void MoveHookTowardsY(float targetY)
    {
        Vector3 p = Hook.position;
        p.y = Mathf.MoveTowards(p.y, targetY, MoveSpeed * Time.deltaTime);
        Hook.position = p;
    }

    public void TryCatch(BaseHuman human)
    {
        if (!CanCatch)
            return;

        if (caughtHuman != null)
            return;

        caughtHuman = human;
        caughtHuman.transform.SetParent(Hook, worldPositionStays: true);
        caughtHuman.enabled = false;

        state = State.Raising;
    }
}