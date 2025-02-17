using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Arrow : MonoBehaviour
{

    [HideInInspector] public float Direction, Speed;
    [HideInInspector] public int Damage;
    private bool isHit;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isHit) return;
        if ((collision.gameObject.layer == 7 && gameObject.layer == 6) ||
            (collision.gameObject.layer == 6 && gameObject.layer == 7) || collision.gameObject.layer == 8)
        {

            Unit target = collision.GetComponent<Unit>();
            if (target != null)
            {
                isHit = true;
                target.DoHit();
                
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
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed * Direction, Space.World);
    }
}
