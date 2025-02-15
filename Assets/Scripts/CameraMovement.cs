using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //카메라 움직이는 범위 인스펙터에 입력.
    [SerializeField] private float Min_X, Max_X;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.x < Screen.width * 0.125f)
        {
            Camera.main.transform.Translate(Vector3.left * Time.deltaTime * 4f);
        }
        if (Input.mousePosition.x > Screen.width * 0.875f)
        {
            Camera.main.transform.Translate(Vector3.right * Time.deltaTime * 4f);
        }
        Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, Min_X , Max_X), Camera.main.transform.position.y,
            Camera.main.transform.position.z);
    }
}
