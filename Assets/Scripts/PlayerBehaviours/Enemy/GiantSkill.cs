using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantSkill : MonoBehaviour
{
    private BoxCollider2D box;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        StartCoroutine(startofSkill());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBehavior"))
        {
            if (collision.GetComponent<Player>() != null) {
                collision.GetComponent<Player>().CharacterDamage(80, 0);
                box.enabled = false;
            }
        }
    }
    IEnumerator startofSkill() {
        yield return new WaitForSeconds(1f);
        animator.enabled = true;
        yield return new WaitForSeconds(0.5f);
        box.enabled = true;
        StartCoroutine(EndofSkill());
    }
    IEnumerator EndofSkill()
    {
        yield return new WaitForSeconds(1.8f);
        Destroy(gameObject);
    }
}
