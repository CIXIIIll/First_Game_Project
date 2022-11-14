using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterL : MonoBehaviour
{
    public float FlightSpeed;
    public float DestoryDistance;
    Skill_Data Skill_Data;
    private Rigidbody2D rb2d;
    private Vector3 vector;
    private Player player;
    private Skill_Data skill;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        if (player.faceright)
        {
            rb2d.velocity = transform.right * FlightSpeed;
        }
        else
        {
            rb2d.velocity = -(transform.right * FlightSpeed);
            transform.localScale = new Vector3(-2.485487f, 1.811549f, 1);
        }
        skill = player.CurrentSkill;
        vector = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = (transform.position - vector).sqrMagnitude;
        if (Distance > DestoryDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (skill.DamageTime != 0)
            {
                float skillDamage = player.GetDeamgeSkill(skill.Damage);
                collision.GetComponent<Enemy>().CharacterDamage(skillDamage, skill.DamageTime);
            }
            else {
                float skillDamage = player.GetDeamgeSkill(skill.Damage);
                collision.GetComponent<Enemy>().CharacterDamage(skillDamage, 0);
            }

        }
    }
}
