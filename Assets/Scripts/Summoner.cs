using UnityEngine;
using UnityEngine.UI;

public class Summoner : MonoBehaviour
{
    [SerializeField] private Button[] btn = new Button[8];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<btn.Length; i++)
        {
            int index = i;
            btn[index].onClick.AddListener(delegate
            {
                GameObject go = Instantiate(Resources.Load<GameObject>($"Unit {index + 4}"), new Vector2(-10, 0), Quaternion.identity);
                go.layer = 6;
            });
        }
    }  

    // Update is called once per frame
    void Update()
    {
        
    }
}
