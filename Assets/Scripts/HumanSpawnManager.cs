using UnityEngine;

public class HumanSpawnManager : MonoBehaviour
{
    public GameObject BaseHumanPrefab;
    public HumanData[] HumanTypes;

    public float LeftX = -10.42f;
    public float RightX = 10.42f;
    public float MinY;
    public float MaxY;
    public float StaticZ = 15f;

    public float SpawnIntervalSeconds = 3f;
    public float LifetimeSeconds = 10f;

    public Vector3 SpawnRotationEulerLeft = Vector3.zero;
    public Vector3 SpawnRotationEulerRight = new Vector3(0f, 180f, 0f);

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + SpawnIntervalSeconds;
    }

    private void Update()
    {
        if (Time.time < nextSpawnTime)
            return;

        SpawnOne();
        nextSpawnTime = Time.time + SpawnIntervalSeconds;
    }

    private void SpawnOne()
    {
        HumanData chosenType = ChooseRandomHumanType();

        bool spawnLeft = Random.value < 0.5f;

        float x = spawnLeft ? LeftX : RightX;
        float y = Random.Range(MinY, MaxY);
        float z = StaticZ;

        int direction = spawnLeft ? 1 : -1;

        Quaternion rotation = Quaternion.Euler(spawnLeft ? SpawnRotationEulerLeft : SpawnRotationEulerRight);
        GameObject go = Instantiate(BaseHumanPrefab, new Vector3(x, y, z), rotation);

        BaseHuman human = go.GetComponent<BaseHuman>();
        human.Initialize(chosenType, direction, LifetimeSeconds);
    }

    private HumanData ChooseRandomHumanType()
    {
        int index = Random.Range(0, HumanTypes.Length);
        return HumanTypes[index];
    }
}
