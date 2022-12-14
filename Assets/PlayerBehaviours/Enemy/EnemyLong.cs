using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLong : Enemy
{
    public float coldDown;
    public float coldDownTime;
    public GameObject Hit;
    public float attackRaius;
    // Start is called before the first frame update
    new void Start()
    {
        base.HP = 50f;
        base.MAXHP = 50f;
        base.Deamge = 10.0f;
        base.speed = 2;
        base.Rspeed = 2;
        base.radius = 10;
        base.LevelDeamge = 5;
        base.Boss = false;
        base.LevelHp = 5;
        base.Close = false;
        coldDown = 1f;
        base.rb2d = GetComponent<Rigidbody2D>();
        coldDownTime = 1f;
        base.EnemyTransform = GetComponent<Transform>();
        base.value = 1;
        base.Start();
        attackRaius = 30;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        coldDownTime += Time.deltaTime;
        if (coldDownTime >= coldDown)
        {
            enemySkill();
        }
    }
    private void enemySkill() {
        float distance = (transform.position - playerTransform.position).sqrMagnitude;
        if (distance < attackRaius) {
            Hit.GetComponent<EnemyHit>().Deamge = base.Deamge;
            coldDownTime = 0;
            Instantiate(Hit, transform.position, transform.rotation);
        }
    }
}
