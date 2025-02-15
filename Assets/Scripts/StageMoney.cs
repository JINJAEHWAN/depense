using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
[System.Serializable]
public struct stageNeed
{
    public float moneySpeed;
    public float moneyAmount;
    public float moneyNow;
}


public class StageMoney : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    bool isPlayerLive = true;
    public stageNeed stageneed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageneed.moneyNow = 100.0f;
        stageneed.moneySpeed = 10.0f;
        stageneed.moneyAmount = 5000.0f;

        StartCoroutine(moneyCount());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator moneyCount()
    {
        while(stageneed.moneyNow <= stageneed.moneyAmount && isPlayerLive)
        {
            stageneed.moneyNow += Time.deltaTime * stageneed.moneySpeed;

            if(stageneed.moneyNow >= stageneed.moneyAmount)
            {
                stageneed.moneyNow = stageneed.moneyAmount;
            }
            float moneya = Mathf.Floor(stageneed.moneyNow * 10f) / 10f;
            tmp.text = moneya.ToString();

            yield return null;
        }
    }


}
