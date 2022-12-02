using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float attackDistance = 5f;
    private Animator animator;
    public float coldDown;
    public float coldDownTime;
    // Start is called before the first frame update
    private new void Start()
    {
        animator = GetComponent<Animator>();
        base.HP = 50f;
        base.MAXHP = 50f;
        base.Deamge = 10.0f;
        base.speed = 5;
        base.radius = 30;
        coldDown = 10f;
        base.rb2d = GetComponent<Rigidbody2D>();
        coldDownTime = 10f;
        base.EnemyTransform = GetComponent<Transform>();
        base.value = 1;
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        coldDownTime += Time.deltaTime;
        if (coldDownTime >= coldDown) {
            enemySkill();
        }
    }
    private void enemySkill() {
        Transform playerTransform = playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance <= attackDistance)
            {
                playAttackAnim();
                StartCoroutine(batSkill());
                coldDownTime = 0;
            }
        }
    }

    void playAttackAnim()
    {
        if (animator != null) {
            animator.Play("Bat_Attack");
        }
        StartCoroutine(playIdle());
    }
    IEnumerator batSkill()
    {
        base.speed = base.speed+ (base.speed*0.3f);
        yield return new WaitForSeconds(3f);
        base.speed = 5f;
    }
    IEnumerator playIdle() {
        yield return new WaitForSeconds(0.5f);
        if (animator != null)
        {
            animator.Play("BatAnim");
        }
    }
}
