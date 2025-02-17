using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [Header("HP�� Start �Լ����� MaxHP�� �ʱ�ȭ��.")]
    public battleData data;
    public bool isEnemy;

    [Header("���� ������ 0, �ü��� 1, ������� 2, ���� 3���� ����")]
    public int type;
    [HideInInspector] public Animator anim;
    private bool Moving;
    private float deltaAtk;
    private List<Unit> targetList;
    private Unit target;
    [Header("�ü� ���ָ� �߻��� ȭ�� �ν����Ϳ� ����")]
    [SerializeField] private Arrow shootArrow;

    private Transform hpSliderPos;
    [Header("�� ü�¹ٸ� Canvas �ȿ� ����� ����.\n�Ϲ� ������ �� �� �뵵 ��")]
    [SerializeField] private Slider hpSlider;
    
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
        if (target != null)
        {
            yield return new WaitForSeconds(0.15f);
            target.DoHit(data.attackPower);
        }
        target = null;
    }
    public void DoHit(int dmg)
    {
        if (anim != null)
        {
            anim.SetTrigger("doHit");
        }
        data.hp -= dmg;
        hpSlider.value = (float)data.hp / data.MaxHp;
        if (data.hp < 1)
        {
            DoDie();
        }

    }
    public void DoDie()
    {
        data.hp = 0;
        if (anim != null)
        {
            anim.SetTrigger("doDie");
        }
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        if (hpSlider != null)
        {
            Destroy(hpSlider.gameObject);
        }
        Destroy(gameObject, 0.5f);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        data.hp = data.MaxHp;
        anim = GetComponentInChildren<Animator>();
        if (anim!=null)
        {
            anim.SetTrigger("doMove");
        }
        if(type != 3) Moving = true;
        target = null;
        targetList = new List<Unit>();
        deltaAtk = 0.01f;
        hpSliderPos = transform.Find("HpSliderPos");
        if(hpSlider == null)
        {
            hpSlider = Instantiate(Resources.Load<Slider>("UnitHPSlider"));
        }
        hpSlider.value = 1f;
        hpSlider.transform.SetParent(FindFirstObjectByType<Canvas>().transform);
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
            //���� ����� ������ ���� ������� ����.
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
                Arrow arrow = Instantiate(shootArrow, transform.position + Vector3.up * 0.2f, Quaternion.identity);
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
        if (hpSlider != null)
        {
            hpSlider.transform.position = Camera.main.WorldToScreenPoint(hpSliderPos.position);
        }
    }
}
