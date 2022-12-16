using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Controller of Attack for Player Object 
/// </summary>
public class PlyaerAttack : MonoBehaviour
{
    /// <summary>
    /// Player animator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// Close range Attack Hit box
    /// </summary>
    private PolygonCollider2D polygon;
    /// <summary>
    ///  Player Object
    /// </summary>
    private Player player;
    /// <summary>
    /// Starting time for Enable Hit box = 0.3 seconds
    /// </summary>
    public float startTime;
    /// <summary>
    /// End of for Disable Hit box = 0.2w seconds
    /// </summary>
    public float endTime;
    /// <summary>
    /// bullet object
    /// </summary>
    public GameObject hit;
    /// <summary>
    /// Attack Speed cool down
    /// </summary>
    public float invokeTime;
    /// <summary>
    /// Initializaed Data 
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
        invokeTime = player.AttackSpeed;
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
        polygon = GetComponent<PolygonCollider2D>();
    }
    /// <summary>
    /// Update the Attack speed cool down
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        invokeTime += Time.deltaTime;
        Attack();
    }
    /// <summary>
    /// Event when input Attack
    /// </summary>
    void Attack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            ///calculate attack speed
            float x = player.AttackSpeed * player.PlayerOffset.speedoffset;
            if (invokeTime - x > 0) {
                invokeTime = 0;
                animator.SetTrigger("isAttack");
                StartCoroutine(stratAttack());
            }     
        }
    }
    /// <summary>
    /// Start Attack
    /// </summary>
    /// <returns></returns>
    IEnumerator stratAttack() { 
        if (player.CloseRange) {
            yield return new WaitForSeconds(startTime);
            /// Eable the hit box
            polygon.enabled = true;
            StartCoroutine(DisAbleAttackBox());
        }
        else { 
            Instantiate(hit, transform.position, transform.rotation); 
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) {
            collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamge(), 0);
            player.SelfHealth(player.GetDeamge() * player.PlayerOffset.LifeSteal);
        }
    }
    IEnumerator DisAbleAttackBox() { 
        yield return new WaitForSeconds(endTime);
        polygon.enabled = false;
    }
}
