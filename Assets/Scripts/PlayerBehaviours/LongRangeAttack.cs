using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The bullet object behaviour
/// </summary>
public class LongRangeAttack : MonoBehaviour
{
    /// <summary>
    /// Flight Speed default 10
    /// </summary>
    [SerializeField]
    private float flightSpeed;
    /// <summary>
    /// Flight Distance
    /// </summary>
    [SerializeField]
    public float DestoryDistance;
    /// <summary>
    /// Rigidbody2D to move
    /// </summary>
    private Rigidbody2D rb2d;
    /// <summary>
    /// Strat position
    /// </summary>
    private Vector3 vector;
    /// <summary>
    /// Player object
    /// </summary>
    private Player player;
    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data and start to flight
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        if (player.faceright)
        {
            rb2d.velocity = transform.right * flightSpeed;
        }
        else
        {
            rb2d.velocity = -(transform.right * flightSpeed);
        }

        vector = transform.position;
    }
    /// <summary>
    /// Update the Distance to Destory
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        ///Calculate the Disance 
        float Distance = (transform.position - vector).sqrMagnitude;
        if (Distance > DestoryDistance) {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// The Event Handler when Bullet hit box find any enemy gameoject
    /// </summary>
    /// <param name="collision">The gameoject in hit box</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamge(), 0);
                player.SelfHealth(player.GetDeamge() * player.PlayerOffset.LifeSteal);
                Destroy(gameObject);
            }
        }
    }
}
