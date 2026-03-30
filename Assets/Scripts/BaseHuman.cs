using UnityEngine;

public class BaseHuman : MonoBehaviour
{
    [Header("Runtime")]
    [SerializeField] private HumanData data;

    public HumanData Data => data;

    private int direction;
    private float moveSpeed;
    private float despawnAtTime;

    public void Initialize(HumanData humanData, int moveDirection, float lifetimeSeconds)
    {
        data = humanData;
        direction = moveDirection >= 0 ? 1 : -1;

        moveSpeed = data != null ? data.MoveSpeed : 0f;
        despawnAtTime = Time.time + Mathf.Max(0f, lifetimeSeconds);

        GameObject model = Instantiate(data.ModelPrefab, transform);

        if (data.OverrideMaterial != null)
        {
            Renderer r = model.GetComponentInChildren<Renderer>(includeInactive: true);
            if (r != null)
                r.material = data.OverrideMaterial;
        }
    }

    private void Update()
    {
        transform.position += Vector3.right * (direction * moveSpeed * Time.deltaTime);

        if (Time.time >= despawnAtTime)
            Destroy(gameObject);
    }
}
