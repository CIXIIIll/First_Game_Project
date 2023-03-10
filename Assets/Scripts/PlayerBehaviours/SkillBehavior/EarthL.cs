using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthL : MonoBehaviour
{
    public float StartTime;
    public float EndTime;
    private PolygonCollider2D box;
    private Animator animator;
    public Skill_Data Skill_Data;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<PolygonCollider2D>(); 
        if (!GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().faceright)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        StartCoroutine(stratSkill());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// enable the hit box to fit with Animation
    /// </summary>
    /// <returns></returns>
    IEnumerator stratSkill()
    {
        yield return new WaitForSeconds(StartTime);
        box.enabled = true;
        StartCoroutine(DisAbleAttackBox());
    }
    /// <summary>
    /// disable the hit box to fit with Animation
    /// </summary>
    /// <returns></returns>
    IEnumerator DisAbleAttackBox()
    {
        yield return new WaitForSeconds(EndTime);
        box.enabled = false;
        animator.Play("EarthLE");
        StartCoroutine(EndofSkill());
    }
    /// <summary>
    /// End the Skill to fit with Animation
    /// </summary>
    /// <returns></returns>
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
                collision.GetComponent<Enemy>().CharacterDamage(Skill_Data.Damage, 0);
            }
        }
    }
}
