using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{
    //Direction은 -1 아니면 1로 설정.
    public float Direction, Speed;
    public int Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {
            Unit target = collision.GetComponent<Unit>();
            if (target != null)
            {
                target.anim.SetTrigger("doHit");
                target.data.hp -= Damage;
                if (target.data.hp < 1)
                {
                    target.data.hp = 0;
                    target.anim.SetTrigger("doDie");
                    Destroy(target.gameObject, 0.4f);
                }
                target.hptext.text = target.data.hp.ToString();
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
