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
        base.HP = 500.0f;
        base.MAXHP = 500f;
        base.Deamge = 30.0f;
        base.speed = 0.5f;
        base.radius = 50;
        base.Start();
        attackDistance = 50;
        coldDown = 15;
        coldDownTime = 15;
        animator = GetComponent<Animator>();
        base.EnemyTransform = GetComponent<Transform>();
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
    void enemySkill() {
        Transform playerTransform = playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance <= attackDistance)
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
    IEnumerator playIdle()
    {
        yield return new WaitForSeconds(1f);
        if (animator != null)
        {
            animator.Play("Gient_Idle");
        }
    }
}
