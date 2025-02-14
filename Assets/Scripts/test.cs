using System.Diagnostics.Tracing;
using UnityEngine;

public class test : Status
{
    [SerializeField] protected battleData data;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Move", true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * data.moveSpeed * Time.deltaTime);
        
        
    }
}
