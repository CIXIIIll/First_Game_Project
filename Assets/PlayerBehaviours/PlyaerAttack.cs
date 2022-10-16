using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerAttack : MonoBehaviour
{
    private Animator animator;
    private PolygonCollider2D polygon;
    private Player player;
    public float Starttime = 0.2f;
    public float time = 0.6f;
    public GameObject hit;
    private float invokeTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        invokeTime = player.AttackSpeed;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    void Attack()
    {
        invokeTime += Time.deltaTime;
        if (Input.GetButtonDown("Attack"))
        { 
            if (invokeTime - player.AttackSpeed > float.Epsilon) {
                animator.SetTrigger("isAttack");
                StartCoroutine(stratAttack());
                invokeTime = 0;
            }     
        }
    }
    IEnumerator stratAttack() { 
        if (player.CloseRange) {
            polygon.enabled = true;
            StartCoroutine(DisAbleAttackBox());
        }
        else { 
            Instantiate(hit, transform.position, transform.rotation); 
        }
        yield return new WaitForSeconds(Starttime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            collision.GetComponent<Monster>().CharacterDamage(player.damage);
        }
    }
    IEnumerator DisAbleAttackBox() { 
        yield return new WaitForSeconds(time);
        polygon.enabled = false;
    }
}
