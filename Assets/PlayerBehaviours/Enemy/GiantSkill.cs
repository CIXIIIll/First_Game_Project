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
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.GetComponent<Player>() != null) {
                Vector3 dis =  collision.transform.position-transform.position ;
                collision.transform.position = new Vector3(collision.transform.position.x + dis.x,
                                                 collision.transform.position.y + dis.y, -1);
                collision.GetComponent<Player>().CharacterDamage(50, 0);
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
