using UnityEngine;

public class HookScript : MonoBehaviour
{

    [SerializeField] private FishingRod Rod;
        


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        BaseHuman human = other.GetComponentInParent<BaseHuman>();
        if (human != null)
            Rod.TryCatch(human);
    }

}

