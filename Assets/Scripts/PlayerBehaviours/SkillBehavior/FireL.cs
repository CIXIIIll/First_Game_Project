using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireL : MonoBehaviour
{
    public float FlightSpeed;
    public float DestoryDistance;
    private Rigidbody2D rb2d;
    private Vector3 vector;
    public Skill_Data Skill_Data;
    [SerializeField]
    private GameObject Next;
    public float next;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().faceright)
        {
            rb2d.velocity = transform.right * FlightSpeed;
        }
        else
        {
            rb2d.velocity = -(transform.right * FlightSpeed);
            transform.localScale = new Vector3(-2.485487f, 1.811549f, 1);
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
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().CharacterDamage(Skill_Data.Damage, 0);
                Instantiate(Next, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

}
