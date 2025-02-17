using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Summoner : MonoBehaviour
{
    [Header("유닛 소환 버튼 인스펙터에 연결")]
    [SerializeField] private Button[] btn;

    [Header("소환할 유닛 Resources에서 찾아서 인스펙터에 연결")]
    [SerializeField] private Unit[] UnitData;
    
    public float delta_cpu;

    [Header("적 유닛 나올 수 있는 범위")]
    [SerializeField] private int cpu_min;
    [SerializeField] private int cpu_max;


    [Header("돈 정보 들어있는 곳 연결")]
    [SerializeField] private StageMoney moneyData;

    [Header("소환할 위치 인스펙터에 입력")]
    [SerializeField] private Vector2 PlayerSummon;
    [SerializeField] private Vector2 CpuSummon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyData = FindFirstObjectByType<StageMoney>();
        for(int i=0; i<btn.Length; i++)
        {
            int index = i;

            btn[index].onClick.AddListener(delegate
            {
                if (UnitData[index].data.cost < moneyData.stageneed.moneyNow)
                {
                    Unit u = Instantiate(UnitData[index], PlayerSummon, Quaternion.identity);
                    u.gameObject.layer = 6;
                    moneyData.stageneed.moneyNow -= u.data.cost;
                }
            });
            btn[i].gameObject.GetComponentInChildren<TextMeshProUGUI>().text = UnitData[index].data.cost.ToString();
        }
        delta_cpu = 5f;
    }  

    // Update is called once per frame
    void Update()
    {
        delta_cpu -= Time.deltaTime;
        if(delta_cpu < 0)
        {
            Unit un = Instantiate(Resources.Load<Unit>($"Unit {Random.Range(cpu_min,cpu_max+1)}"), CpuSummon, Quaternion.identity);
            un.data.moveSpeed *= -1;
            un.transform.localScale = new Vector3(-un.transform.localScale.x, un.transform.localScale.y, 1);
            un.gameObject.layer = 7;
            un.isEnemy = true;
            delta_cpu = 4f;
        }
        
    }
}
