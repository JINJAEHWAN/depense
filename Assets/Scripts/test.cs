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
        Debug.Log(Input.mousePosition);
        if (Input.mousePosition.x < Screen.width * 0.1f)
        {
            Camera.main.transform.Translate(Vector3.left * Time.deltaTime * 3f);
        }
        if (Input.mousePosition.x > Screen.width * 0.9f)
        {
            Camera.main.transform.Translate(Vector3.right * Time.deltaTime * 3f);
        }
        Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, -7.8f, 7.8f), Camera.main.transform.position.y,
            Camera.main.transform.position.z);
    }
}
