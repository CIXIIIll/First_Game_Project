using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiant : Enemy
{
    private Animator animator;
    public float coldDown;
    public float coldDownTime;
    public float attackDistance;
    public GameObject skill;
    // Start is called before the first frame update
    private new void Start()
    {
        base.HP = 1500.0f;
        base.MAXHP = 1500f;
        base.Deamge = 30.0f;
        base.LevelDeamge = 10;
        base.LevelHp = 100;
        base.Close = true;
        base.Boss = true;
        base.speed = 0.5f;
        base.Rspeed = 0.5f;
        base.radius = 50;
        base.value = 5;
        attackDistance = 50;
        base.rb2d = GetComponent<Rigidbody2D>();
        coldDown = 15;
        coldDownTime = 15;
        animator = GetComponent<Animator>();
        base.EnemyTransform = GetComponent<Transform>();
        base.Start();
        StartCoroutine(enemyAttack());
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        coldDownTime += Time.deltaTime;
        if (coldDownTime >= coldDown)
        {
            enemySkill();
        }
    }
    IEnumerator enemyAttack() {
        playAttackAAnim();
        Transform attack = transform.GetChild(1);
        attack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        attack.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        StartCoroutine(enemyAttack());
    }
    void enemySkill() {
        Transform playerTransform = playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance <= attackDistance&& !forzen)
            {
                playAttackAnim();
                Instantiate(skill, playerTransform.position, playerTransform.rotation);
                coldDownTime = 0;
            }
        }
    }
    void playAttackAnim()
    {
        if (animator != null)
        {
            animator.Play("Earth_Golem_AttackB");
        }
        StartCoroutine(playIdle());
    }
    void playAttackAAnim()
    {
        if (animator != null)
        {
            animator.Play("Earth_Golem_AttackA");
        }
        StartCoroutine(playIdle());
    }
    IEnumerator playIdle()
    {
        yield return new WaitForSeconds(1f);
        if (animator != null)
        {
            animator.Play("Gient_Idle");
        }
    }
}
