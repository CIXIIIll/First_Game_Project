using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireC : MonoBehaviour
{
    public float StartTime;
    public float EndTime;
    private PolygonCollider2D box;
    private Player player;
    public float MPCost;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<PolygonCollider2D>();
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
        StartCoroutine(stratSkill());
        StartCoroutine(reduceMpOverTime());
        Skill_Data temp = player.GetCurrentSkill();
        MPCost = temp.MPcost;
    }

    // Update is called once per frame
    void Update()
    {
  
    }
    /// <summary>
    /// Reduce the MP by the time
    /// </summary>
    /// <returns></returns>
    IEnumerator reduceMpOverTime() {
        if (MPCost > player.MP)
        {
            StopSkill();
        }
        else
        {
            player.ReduceMP(MPCost);
            yield return new WaitForSeconds(1);
            endReduceMPOverTime();
        }
    }
    void endReduceMPOverTime()
    {
        StartCoroutine(reduceMpOverTime());
    }
    /// <summary>
    /// Enable the hit box as loop
    /// </summary>
    /// <returns></returns>
    IEnumerator stratSkill()
    {
        yield return new WaitForSeconds(StartTime);
        box.enabled = true;
        StartCoroutine(DisAbleAttackBox());
    }
    /// <summary>
    /// reset the hit box as loop
    /// </summary>
    /// <returns></returns>
    IEnumerator DisAbleAttackBox()
    {
        yield return new WaitForSeconds(EndTime);
        box.enabled = false;
        StartCoroutine(stratSkill());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null) {
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(100), 0);
            }
        }
    }
    /// <summary>
    /// Stop the skill either with out enough Mp or input skill key again
    /// </summary>
    public void StopSkill() {
        player.Frozen = false;
        Destroy(gameObject);
    }
}
