using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthL : MonoBehaviour
{
    public float StartTime;
    public float EndTime;
    private PolygonCollider2D box;
    private Player player;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<PolygonCollider2D>(); 
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!player.faceright)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        StartCoroutine(stratSkill());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator stratSkill()
    {
        yield return new WaitForSeconds(StartTime);
        box.enabled = true;
        StartCoroutine(DisAbleAttackBox());
    }
    IEnumerator DisAbleAttackBox()
    {
        yield return new WaitForSeconds(EndTime);
        box.enabled = false;
        animator.Play("EarthLE");
        StartCoroutine(EndofSkill());
    }
    IEnumerator EndofSkill()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamge(), 0);
            //»÷ÍË
            Vector3 dis = collision.transform.position - transform.position;
            if (dis.x > 0)
            {
                dis.x = (dis.x * 2);
            }
            else {
                dis.x = (dis.x * 2);
            }
            if (dis.y > 0)
            {
                dis.y = (dis.y * 2);
            }
            else { 
                dis.y = (dis.y * 2);
            }
            collision.transform.position = new Vector3(collision.transform.position.x + dis.x,
                                                        collision.transform.position.y + dis.y, -1);               

        }
    }
}
