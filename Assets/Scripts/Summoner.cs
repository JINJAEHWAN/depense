

using UnityEngine;
using UnityEngine.UI;

public class Summoner : MonoBehaviour
{
    [SerializeField] private Button[] btn = new Button[8];
    private float delta_cpu, delta_player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for(int i=0; i<btn.Length; i++)
        {
            int index = i;
            btn[index].onClick.AddListener(delegate
            {
                if (delta_player < 0)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>($"Unit {index + 4}"), new Vector2(-10, 0), Quaternion.identity);
                    go.layer = 6;
                    delta_player = 2f;
                }
            });
        }
        delta_cpu = 5f;
        delta_player = 0;
    }  

    // Update is called once per frame
    void Update()
    {
        delta_player -= Time.deltaTime;
        delta_cpu -= Time.deltaTime;
        if(delta_cpu < 0)
        {
            Unit un = Instantiate(Resources.Load<Unit>($"Unit {Random.Range(4,12)}"), new Vector2(10, 0), Quaternion.identity);
            un.data.moveSpeed *= -1;
            un.transform.localScale = new Vector3(-un.transform.localScale.x, un.transform.localScale.y, 1);
            un.gameObject.layer = 7;
            delta_cpu = 5;
        }
        
    }
}
