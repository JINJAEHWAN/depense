using System.Diagnostics.Tracing;
using UnityEngine;

public class test : Status
{
    [SerializeField] protected battleData data;
    private Animator anim;
    private bool Moving;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("Move", true);
        Moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
            transform.Translate(Vector3.right * data.moveSpeed * Time.deltaTime);
        
        
    }
}
