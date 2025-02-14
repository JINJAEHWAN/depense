using UnityEngine;

public struct stageNeed
{
    public float moneySpeed;
    public float moneyAmount;
    public float moneyNow;
}


public class Stage : MonoBehaviour
{
    [SerializeField]protected stageNeed stageneed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageneed.moneyNow = 100.0f;
        stageneed.moneySpeed = 10.0f;
        stageneed.moneyAmount = 5000.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
