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
            player.SetExtra(1);
            collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamge() * player.Extra, 0);
            Destroy(gameObject);
        }
    }
}
