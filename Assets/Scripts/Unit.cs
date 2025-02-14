using System.Collections;
using TMPro;
using UnityEngine;

//일단 castle 공격에 대한 처리는 하지 않았다.
//castle 스크립트가 없기 때문.
//코드 길이 줄일 수 있으면 줄여도 됨.
public class Unit : MonoBehaviour
{
    public battleData data;
    public bool isEnemy;
    //근접유닛은 0으로 설정.
    public int type; 
    public Animator anim;
    private bool Moving;
    private float deltaAtk;
    private IEnumerator movesCoroutine;
    private Unit target;
    [SerializeField] private Arrow shootArrow;
    //Unit 0에만 현재 hp를 나타내는 Text가 달려 있음. 다른 데도 추가하던가 빼던가 추후 결정.
    //다른 데도 추가할 시 인스펙터에 등록하면 됨.

    //유닛 겹치게 할 건지 안 할 건지 확실히 결정.
    //겹치게 하는 게 편하긴 함.
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
                //원거리 공격 처리.
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
        //근접 공격, 공격 모션 뜨고 0.2초 뒤 피격 모션 뜨는 것으로 일단 설정.
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
