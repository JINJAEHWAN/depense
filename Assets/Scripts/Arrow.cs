using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{

    [HideInInspector] public float Direction, Speed;
    [HideInInspector] public int Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            Unit target = collision.GetComponent<Unit>();
            if (target != null)
            {
                if (target.anim != null)
                {
                    target.anim.SetTrigger("doHit");
                }
                target.data.hp -= Damage;
                if (target.data.hp < 1)
                {
                    target.data.hp = 0;
                    if (target.anim != null)
                    {
                        target.anim.SetTrigger("doDie");
                    }
                    if (target.GetComponent<Collider2D>() != null)
                        target.GetComponent<Collider2D>().enabled = false;
                    if (target.GetComponent<Rigidbody2D>() != null) target.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                    Destroy(target.gameObject, 0.5f);
                }
                if(target.hptext != null)
                {
                    target.hptext.text = target.data.hp.ToString();
                }
                Destroy(gameObject);
            }
        }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //발사된 지 2초 지나면 사라지게 설정.
        transform.localScale = new Vector3(Direction, 1, 1);
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed * Direction, Space.World);
    }
}
