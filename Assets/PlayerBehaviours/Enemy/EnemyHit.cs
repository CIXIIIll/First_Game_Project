using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float AttackSpeed;
    public float DestoryDistance;
    private Vector3 vector;
    public float Deamge;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 dis = transform.position - GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().transform.position;
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = -(dis) ;
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
        if (collision != null) {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.GetComponent<Player>() != null) {
                    collision.GetComponent<Player>().CharacterDamage(Deamge, 0);
                    Destroy(gameObject);
                }
            }
        }
    }
}
