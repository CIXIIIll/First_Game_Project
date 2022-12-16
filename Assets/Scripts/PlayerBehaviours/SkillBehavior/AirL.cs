using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirL : MonoBehaviour
{
    /// <summary>
    /// Speed of Skill
    /// </summary>
    private float RotateSpeed = 5;
    /// <summary>
    /// The Radius for moving
    /// </summary>
    private float Radius = 1;
    /// <summary>
    /// Start Time
    /// </summary>
    private float StartTime;
    /// <summary>
    /// Total activate time
    /// </summary>
    private float TotalTime;
    /// <summary>
    /// Where is the player
    /// </summary>
    private Vector2 center;
    /// <summary>
    /// The Angle from current position to next position
    /// </summary>
    private float Angle;
    /// <summary>
    /// Skill info
    /// </summary>
    public Skill_Data Skill_Data;
    void Start()
    {
        Vector3 temp = transform.position;
        temp.z = -1;
        transform.position = temp;
        center = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        StartTime = 0;
        TotalTime = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        StartTime += Time.deltaTime;
        if (StartTime >= TotalTime) {
            Destroy(gameObject);
        }
        ///Let skill move around player
        center = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        Angle +=RotateSpeed *Time.deltaTime;
        var offect = new Vector2(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
        transform.position = center + offect;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().SetExtra(0.5f);
                float Extra = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().Extra;
                collision.GetComponent<Enemy>().CharacterDamage(Skill_Data.Damage * Extra, 0);
                Destroy(gameObject);
            }
        }
    }
}
