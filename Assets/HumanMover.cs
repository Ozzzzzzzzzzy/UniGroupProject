using UnityEngine;

public class HumanMover : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float Direction;
    public float Speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void MoveLeft()
    {
        transform.Translate(Vector3.left * Speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);
    }
}
