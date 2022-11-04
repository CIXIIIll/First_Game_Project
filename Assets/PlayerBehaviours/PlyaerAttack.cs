using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerAttack : MonoBehaviour
{
    private Animator animator;
    private PolygonCollider2D polygon;
    private Player player;
    public float Starttime;
    public float time;
    public GameObject hit;
    public GameObject charged1;
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
        Skill();
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
    void Skill() {
        invokeTime += Time.deltaTime;
        if (Input.GetButtonDown("Skill"))
        {
            if (player.MP >= player.CurrentSkill.MPcost)
            {
                animator.SetTrigger("isAttack");
                player.ReduceMP(player.CurrentSkill.MPcost);
                Instantiate(charged1, transform.position, transform.rotation);
            }
            else {
                return;
            }
        }
    }

    IEnumerator stratAttack() { 
        if (player.CloseRange) {
            yield return new WaitForSeconds(Starttime);
            polygon.enabled = true;
            StartCoroutine(DisAbleAttackBox());
        }
        else { 
            Instantiate(hit, transform.position, transform.rotation); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamge(),0);
        }
    }
    IEnumerator DisAbleAttackBox() { 
        yield return new WaitForSeconds(time);
        polygon.enabled = false;
    }
}
