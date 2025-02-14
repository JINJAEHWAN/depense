using System.Collections;
using TMPro;
using UnityEngine;

//�ϴ� castle ���ݿ� ���� ó���� ���� �ʾҴ�.
//castle ��ũ��Ʈ�� ���� ����.
//�ڵ� ���� ���� �� ������ �ٿ��� ��.
public class Unit : MonoBehaviour
{
    public battleData data;
    public bool isEnemy;
    //���������� 0���� ����.
    public int type; 
    public Animator anim;
    private bool Moving;
    private float deltaAtk;
    private IEnumerator movesCoroutine;
    private Unit target;
    [SerializeField] private Arrow shootArrow;
    //Unit 0���� ���� hp�� ��Ÿ���� Text�� �޷� ����. �ٸ� ���� �߰��ϴ��� ������ ���� ����.
    //�ٸ� ���� �߰��� �� �ν����Ϳ� ����ϸ� ��.

    //���� ��ġ�� �� ���� �� �� ���� Ȯ���� ����.
    //��ġ�� �ϴ� �� ���ϱ� ��.
    public TextMeshPro hptext;
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
        yield return new WaitForSeconds(0.25f);
        anim.SetTrigger("doMove");
        Moving = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            if (target == null)
            {
                Debug.Log("in");
                target = collision.GetComponent<Unit>();
            }
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
            if(deltaAtk < 0 && target == collision.GetComponent<Unit>())
            {
                anim.SetTrigger("doAttack");
                deltaAtk = 1f;
                if (type == 0)
                {
                    StartCoroutine(HitByMeleeAttack());
                }
                //���Ÿ� ���� ó��.
                else if (type == 1)
                {
                    Arrow arrow = Instantiate(shootArrow, transform.position, Quaternion.identity);
                    arrow.Speed = 3f;
                    arrow.Direction = isEnemy ? -1 : 1;
                    arrow.gameObject.layer = isEnemy ? 7 : 6;
                    arrow.Damage = data.attackPower;
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            if(target == collision.GetComponent<Unit>())
            {
                Debug.LogWarning(1);
                target = null;
            }
            if (data.hp > 0)
            {
                movesCoroutine = MoveCo();
                StartCoroutine(movesCoroutine);

            }
        }
    }

    IEnumerator HitByMeleeAttack()
    {
        //���� ����, ���� ��� �߰� 0.2�� �� �ǰ� ��� �ߴ� ������ �ϴ� ����.
        if (target != null)
        {
            yield return new WaitForSeconds(0.2f);
            target.anim.SetTrigger("doHit");
            target.data.hp -= data.attackPower;
            if (target.data.hp < 1)
            {
                target.data.hp = 0;
                target.anim.SetTrigger("doDie");
                Destroy(target.gameObject, 0.4f);
            }
            target.hptext.text = target.data.hp.ToString();
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
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
            transform.Translate(Vector3.right * data.moveSpeed * Time.deltaTime);
        deltaAtk -= Time.deltaTime * data.attackSpeed;
    }
}
