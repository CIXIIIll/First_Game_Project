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
    public GameObject FireCP;
    private float invokeTime;
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
            Skill_Data CurrentSkill = player.CurrentSkill;
            if (player.MP >= player.CurrentSkill.MPcost)
            {
                if (player.CurrentSkill.CloseWeapon == true)
                {
                    if (true)
                    {
                        // Fire C
                        player.Frozen = true;
                        if (player.faceright)
                        {
                            Vector3 vector = player.transform.position;
                            vector.x += 1.5f;
                            FireCP = Instantiate<GameObject>(CurrentSkill.Skill_prefab, vector, player.transform.rotation, player.transform);
                        }
                        else
                        {
                            Vector3 vector = player.transform.position;
                            vector.x -= 1.5f;
                            FireCP = Instantiate<GameObject>(CurrentSkill.Skill_prefab, vector, player.transform.rotation, player.transform);
                        }

                    }
                }
                // Long range
                else {
                    if (true)
                    {
                        //animator.Play("CloseEarth");
                        player.ReduceMP(player.CurrentSkill.MPcost);
                        Vector3 vec = transform.position;
                        if (player.faceright)
                        {
                            vec.x += 3;
                        }
                        else
                        {
                            vec.x -= 3;
                        }
                        Instantiate(CurrentSkill.Skill_prefab, vec, transform.rotation);
                        StartCoroutine(Invincibilityframe());
                    }
                    else {
                        animator.SetTrigger("isAttack");
                        player.ReduceMP(player.CurrentSkill.MPcost);
                        Instantiate(CurrentSkill.Skill_prefab, transform.position, transform.rotation);
                    }
                }
            }
            else {
                return;
            }
        }
    }

    IEnumerator Invincibilityframe()
    {
        player.CannotDamage = true;
        yield return new WaitForSeconds(0.5f);
        player.CannotDamage = false;
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
