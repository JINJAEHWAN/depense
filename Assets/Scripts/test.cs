using UnityEngine;

public class test : Status
{
    [SerializeField] protected battleData data;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * data.moveSpeed * Time.deltaTime);
    }
}
