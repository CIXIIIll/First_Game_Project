using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLII : MonoBehaviour
{
    public float StartTime;
    public float EndTime;
    private BoxCollider2D box;
    private Player player;
    public int repectTimes;
    public int currentTimes;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        currentTimes = 0;
        StartCoroutine(stratSkill());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator stratSkill()
    {
        yield return new WaitForSeconds(StartTime);
        box.enabled = true;
        StartCoroutine(DisAbleAttackBox());
        currentTimes++;
    }
    IEnumerator DisAbleAttackBox()
    {
        yield return new WaitForSeconds(EndTime);
        box.enabled = false;
        if (currentTimes < repectTimes)
        {
            StartCoroutine(stratSkill());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(50), 0);
            }
        }
    }
}
