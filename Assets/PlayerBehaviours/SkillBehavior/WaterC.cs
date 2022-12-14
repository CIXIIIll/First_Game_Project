using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterC : MonoBehaviour
{
    private Player player;
    private float Angle;
    private float StartTime;
    private float TotalTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        if (Radius > 0.5) {
            Radius -= 0.02f;
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Hitbox(collision, Radius));
    }
    private void SkillEffect(Collider2D collision, float Radius)
    {
        if(collision != null)
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(50), 0);
                if (!collision.gameObject.GetComponent<Enemy>().Boss)
                {
                    collision.gameObject.GetComponent<Enemy>().forzen = true;
                    float RotateSpeed = 15;
                    Vector3 center = transform.position;
                    Angle += RotateSpeed * Time.deltaTime;
                    var offect = new Vector3(Mathf.Sin(Angle), Mathf.Cos(Angle)) * Radius;
                    collision.gameObject.GetComponent<Transform>().position = center + offect;
                }
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(1), 0);
            }
            collision.gameObject.GetComponent<Enemy>().forzen = false;
        }
    }
}