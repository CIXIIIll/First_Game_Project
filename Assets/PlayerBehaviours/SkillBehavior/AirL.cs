using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirL : MonoBehaviour
{

    private float RotateSpeed = 5;
    private float Radius = 1;
    private float StartTime;
    private float TotalTime;
    private Vector2 center;
    private float Angle;
    private Player player;
    void Start()
    {
        Vector3 temp = transform.position;
        temp.z = -1;
        transform.position = temp;
        center = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
                player.SetExtra(1);
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(50) * player.Extra, 0);
                Destroy(gameObject);
            }
        }
    }
}
