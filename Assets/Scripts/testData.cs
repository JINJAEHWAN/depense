using System.Collections;
using UnityEngine;

public class testData : MonoBehaviour
{
    [Header("소환할 위치 인스펙터에 입력")]
    [SerializeField] private Vector2 CpuSummon;

    IEnumerator firstWave()
    {
        Unit[] un = new Unit[3];

        for (int i = 0; i < 3; i++)
        {
            un[i] = Instantiate(Resources.Load<Unit>($"Unit 0"), CpuSummon, Quaternion.identity);
            un[i].data.moveSpeed *= -1;
            un[i].transform.localScale = new Vector3(-un[i].transform.localScale.x, un[i].transform.localScale.y, 1);
            un[i].gameObject.layer = 7;
            un[i].isEnemy = true;
        }
        yield return null;
    }
    private void Start()
    {
        StartCoroutine(firstWave());
    }


}
