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
    public GameObject SkillCP;
    public float invokeTime;
    private float holdTime;
    AnimatorStateInfo animatorInfo;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        invokeTime = player.AttackSpeed;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        polygon = GetComponent<PolygonCollider2D>();
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        invokeTime += Time.deltaTime;
        Attack();
    }
    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            float x = player.AttackSpeed * player.PlayerOffset.speedoffset;
            if (invokeTime - x > 0) {
                invokeTime = 0;
                animator.SetTrigger("isAttack");
                StartCoroutine(stratAttack());
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
            collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamge(), 0);
            player.SelfHealth(player.GetDeamge() * player.PlayerOffset.LifeSteal, 0);
        }
    }
    IEnumerator DisAbleAttackBox() { 
        yield return new WaitForSeconds(time);
        polygon.enabled = false;
    }
}
