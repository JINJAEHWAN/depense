using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class TestButton : MonoBehaviour, IPointerClickHandler
{
    public UnityAction clickAct;
   
    public int num;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(num);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
