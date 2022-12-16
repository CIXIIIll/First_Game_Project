using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterC : MonoBehaviour
{
    private float Angle;
    public Skill_Data Skill_Data;
    private float StartTime;
    private float TotalTime;
    // Start is called before the first frame update
    void Start()
    {
        Angle = 0;
        StartTime = 0;
        TotalTime = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        StartTime += Time.deltaTime;
        if (StartTime >= TotalTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Hitbox(collision,3));
        }
    }
    IEnumerator Hitbox(Collider2D collision, float Radius)
    {
        SkillEffect(collision, Radius);
        ///Check if is too close to center
        if (Radius > 0.5) {
            Radius -= 0.02f;
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(Hitbox(collision, Radius));
    }
    private void SkillEffect(Collider2D collision, float Radius)
    {
        if(collision != null)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                if (!collision.gameObject.GetComponent<Enemy>().Boss)
                {
                    // forzen the enemy movement and let enemy move to the center of skill
                    collision.gameObject.GetComponent<Enemy>().forzen = true;
                    float RotateSpeed = 15;
                    Vector3 center = transform.position;
                    Angle += RotateSpeed * Time.deltaTime;
                    var offect = new Vector3(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
                    collision.gameObject.GetComponent<Transform>().position = center + offect;
                }
                collision.GetComponent<Enemy>().CharacterDamage(Skill_Data.Damage, 0);
                collision.gameObject.GetComponent<Enemy>().forzen = false;
            }
        }
    }
}