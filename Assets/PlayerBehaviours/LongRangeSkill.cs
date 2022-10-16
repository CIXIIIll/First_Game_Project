using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeSkill : MonoBehaviour
{
    public float AttackSpeed;
    public float DestoryDistance;
    Skill_Data Skill_Data;
    private Rigidbody2D rb2d;
    private Vector3 vector;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        if (player.faceright)
        {
            rb2d.velocity = transform.right * AttackSpeed;
        }
        else
        {
            rb2d.velocity = -(transform.right * AttackSpeed);
        }

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
            Monster monster = collision.gameObject.GetComponent<Monster>();
            collision.GetComponent<Monster>().CharacterDamage(player.damage);
            Destroy(gameObject);
        }
    }
}