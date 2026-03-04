using UnityEngine;


[CreateAssetMenu(fileName = "HumanData", menuName = "Fishing/Human Data", order = 1)]
public class HumanData : ScriptableObject
{

    public string DisplayName;
    public int Value;
    public int MoveSpeed;

    public GameObject ModelPrefab;


}
