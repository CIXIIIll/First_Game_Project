using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthC : MonoBehaviour
{
    public float FlightSpeed;
    public float DestoryDistance;
    private Rigidbody2D rb2d;
    private Vector3 vector;
    private Player player;
    public int currentTimes;
    public int totalTimes;
    public float Extra;
    // Start is called before the first frame update
    void Start()
    {
        
        currentTimes = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        rb2d = GetComponent<Rigidbody2D>();
        if (player.faceright)
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
            StartCoroutine(EndofSkill());
        }
    }
    IEnumerator EndofSkill()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                if (currentTimes <= totalTimes)
                {
                    collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(150) * player.Extra, 0);
                    currentTimes++;
                }
                else
                {
                    collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(150), 0);
                }
            }
        }
    }
}
