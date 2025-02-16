using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public battleData data;
    public bool isEnemy;

    [Header("���� ������ 0, �ü��� 1, ������� 2, ���� 3���� ����")]
    public int type;
    [HideInInspector] public Animator anim;
    private bool Moving;
    private float deltaAtk;
    public List<Unit> targetList;
    private Unit target;
    [Header("�߻��� ȭ�� Resources���� ã�Ƽ� �ν����Ϳ� ����")]
    [SerializeField] private Arrow shootArrow;
    //Unit 0���� ���� hp�� ��Ÿ���� Text�� �޷� ����. �ٸ� ���� �߰��ϴ��� ������ ���� ����.
    //�ٸ� ���� �߰��� �� �ν����Ϳ� ����ϸ� ��.

    //���� ��ġ�� �� ���� �� �� ���� Ȯ���� ����.
    //��ġ�� �ϴ� �� ���ϱ� ��.
    [Header("Object �ȿ��� HP �ؽ�Ʈ ã�Ƽ� �ν����Ϳ� ����")]
    public TextMeshPro hptext;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            
            anim.SetTrigger("doStop");
            Moving = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            anim.SetTrigger("doMove");
            Moving = true;
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
            Unit u = collision.GetComponent<Unit>();
            if (u != null)
            {
                targetList.Add(u);
                if (anim != null)
                    anim.SetTrigger("doStop");
                Moving = false;
            }
        }
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            Unit u = collision.GetComponent<Unit>();
            if (u != null)
            {
                targetList.Remove(u);
            }
            if (data.hp > 0 && targetList.Count < 1)
            {
                if (anim != null)
                    anim.SetTrigger("doMove");
                Moving = true;
            }
        }
    }

    IEnumerator HitByMeleeAttack()
    {
        //���� ����, ���� ��� �߰� 0.25�� �� �ǰ� ��� �ߴ� ������ �ϴ� ����.
        if (target != null)
        {
            yield return new WaitForSeconds(0.25f);
            if (target.anim != null)
            {
                target.anim.SetTrigger("doHit");
            }
            target.data.hp -= data.attackPower;
            if (target.data.hp < 1)
            {
                target.data.hp = 0;
                if (target.anim != null)
                {
                    target.anim.SetTrigger("doDie");
                }
                target.GetComponent<Collider2D>().enabled = false;
                if (target.GetComponent<Rigidbody2D>() != null) target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                targetList.Remove(target);
                Destroy(target.gameObject, 0.5f);
            }
            target.hptext.text = target.data.hp.ToString();
        }
        target = null;
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        if (anim!=null)
        {
            anim.SetTrigger("doMove");
        }
        if(type != 3) Moving = true;
        hptext.text = data.hp.ToString();
        if (isEnemy)
        {
            hptext.rectTransform.localScale = new Vector3(-1, 1, 1);
        }
        target = null;
        targetList = new List<Unit>();
        deltaAtk = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Moving)
        {
            transform.Translate(Vector3.right * data.moveSpeed * Time.deltaTime);
        }
        
        deltaAtk -= Time.deltaTime * data.attackSpeed;
        if (deltaAtk < 0 && targetList.Count > 0)
        {
            float nearestDistance = Mathf.Infinity;
            
            for (int i = 0; i < targetList.Count; i++)
            {
                float dist = Vector2.Distance(transform.position, targetList[i].transform.position);
                if (dist < nearestDistance)
                {
                    target = targetList[i].GetComponent<Unit>();
                    nearestDistance = dist;
                }
            }
            anim.SetTrigger("doAttack");
            deltaAtk = 1f;
            //���� ���� ó��.
            if (type == 0)
            {
                StartCoroutine(HitByMeleeAttack());
            }
            //���Ÿ� ���� ó��. ȭ���� �����Բ� ó��.
            else if (type == 1)
            {
                Arrow arrow = Instantiate(shootArrow, transform.position, Quaternion.identity);
                arrow.Speed = 3f;
                arrow.Direction = isEnemy ? -1 : 1;
                arrow.gameObject.layer = isEnemy ? 7 : 6;
                arrow.Damage = data.attackPower;
                target = null;
            }
            //������ ���� ó�� ��� ���� �𸣰���.
            //�ϴ� �̷��� ó���� ��� ������ ����ü�� ������ �ʰ� ��Ÿ� �ȿ� �ִ� ���� ����� ���� �׳� ������.
            //�����ص� ��.
            if (type == 2)
            {
                StartCoroutine(HitByMeleeAttack());
            }
        }
    }
}
