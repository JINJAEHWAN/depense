using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public battleData data;
    public bool isEnemy;
    public int type;
    private Animator anim;
    private bool Moving;
    private float deltaAtk;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7)
            )
        {
            anim.SetBool("Move", false);
            Moving = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7))
        {
            deltaAtk -= Time.deltaTime;
            if(deltaAtk < 0)
            {
                anim.SetTrigger("Attack");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7))
        {
            anim.SetBool("Move", true);
            Moving = true;
        }
    }
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
