using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthC : MonoBehaviour
{
    public float FlightSpeed;
    public float DestoryDistance;
    private Rigidbody2D rb2d;
    private Vector3 vector;
    public int currentTimes;
    public int totalTimes;
    public float Extra;
    public Skill_Data Skill_Data;
    // Start is called before the first frame update
    void Start()
    {
        
        currentTimes = 0;

        rb2d = GetComponent<Rigidbody2D>();
        if (GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().faceright)
        {
            rb2d.velocity = transform.right * FlightSpeed;
        }
        else
        {
            rb2d.velocity = -(transform.right * FlightSpeed);
            transform.localScale = new Vector3(-1, 1, 1);
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
                if (currentTimes <= totalTimes)
                {
                    collision.GetComponent<Enemy>().CharacterDamage(Skill_Data.Damage, 0);
                    currentTimes++;
                }
                else
                {
                    collision.GetComponent<Enemy>().CharacterDamage(Skill_Data.Damage, 0);
                }
            }
        }
    }
}
