using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color original;
    public float HP;
    public float Deamge;
    public float flashTime;

    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (HP <= float.Epsilon)
        {
            ElementGenerate();
            Destroy(gameObject);
        }
    }
    private void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Player>().CharacterDamage(Deamge, 0);
        }
    }
    public void ResetColor()
    {
        sr.color = original;
    }
    public void CharacterDamage(float damage, float duration)
    {
        if (duration != 0)
        {
            StartCoroutine(DamageOverTime(damage, duration));
        }
        else
        {
            HP -= damage;
        }
        FlashColor(flashTime);
    }
    private IEnumerator DamageOverTime(float damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            FlashColor(flashTime);
            HP -= damage;
        }
    }
    public void ElementGenerate()
    {
        int i = Random.Range(1,8);
        for (int j = 0; j < i; j++)
        {
            int x = Random.Range(0, 4);
            if (x == 4) {
                x = 3;
            }
            GameObject.FindGameObjectWithTag("Element").GetComponent<ElementController>().ElementGenerate(transform, x);
        }
    }
}
