using System.Collections;
using TMPro;
using UnityEngine;


public class Unit : MonoBehaviour
{
    public battleData data;
    public bool isEnemy;
    public int type; //일단 근접은 0, 원거리는 1임.
    private Animator anim;
    private bool Moving;
    private float deltaAtk;
    private IEnumerator movesCoroutine;
    private Unit target;
    [SerializeField] private TextMeshPro hptext;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            if (movesCoroutine != null)
            {
                StopCoroutine(movesCoroutine);
                movesCoroutine = null;
            }
            anim.SetTrigger("doStop");
            Moving = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            if (data.hp >0)
            {
                movesCoroutine = MoveCo();
                StartCoroutine(movesCoroutine);
                
            }
        }
    }
    
    IEnumerator MoveCo()
    {
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("doMove");
        Moving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            target = collision.GetComponent<Unit>();
            if (movesCoroutine != null)
            {
                StopCoroutine(movesCoroutine);
                movesCoroutine = null;
            }
            anim.SetTrigger("doStop");
            Moving = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            if(deltaAtk < 0)
            {
                anim.SetTrigger("doAttack");
                deltaAtk = 1f;
                if (type == 0)
                {
                    StartCoroutine(HitByMeleeAttack());
                }
            }
        }
    }

    IEnumerator HitByMeleeAttack()
    {
        if (target != null)
        {
            yield return new WaitForSeconds(0.2f);
            target.anim.SetTrigger("doHit");
            target.data.hp -= data.attackPower;
            target.hptext.text = target.data.hp.ToString();
            if (target.data.hp < 1)
            {
                yield return new WaitForSeconds(0.2f);
                target.anim.SetTrigger("doDie");
                
                Destroy(target.gameObject, 0.4f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            target = null;
            if (data.hp > 0)
            {
                movesCoroutine = MoveCo();
                StartCoroutine(movesCoroutine);

            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        movesCoroutine = null;
        anim.SetTrigger("doMove");
        Moving = true;
        hptext.text = data.hp.ToString();
        if (isEnemy)
        {
            hptext.rectTransform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
            transform.Translate(Vector3.right * data.moveSpeed * Time.deltaTime);
        deltaAtk -= Time.deltaTime * data.attackSpeed;
    }
}
