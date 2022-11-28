using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color original;
    public float HP;
    public float MAXHP;
    public float Deamge;
    public Transform playerTransform;
    public float speed;
    public float radius;
    public float flashTime;
    public Transform EnemyTransform;
    // Start is called before the first frame update
    public void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (HP <= float.Epsilon)
        {
            ElementGenerate();
            Destroy(gameObject);
        }
        enemyMove();
    }
    private void enemyMove() {
        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            float a = (transform.position.x - playerTransform.position.x);
            Vector3 vec = transform.localScale;
            if (a > 0) {
                if (vec.x > 0) {
                    vec.x = vec.x * -1;
                }
                transform.localScale = vec;
            }
            else
            {
                if (vec.x < 0)
                {
                    vec.x = vec.x * -1;
                }
                transform.localScale = vec;
            }
            if (distance < radius)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
            }
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
            if (collision.GetComponent<Player>() != null) {
                collision.GetComponent<Player>().CharacterDamage(Deamge, 0);
                Vector3 dis = EnemyTransform.position - collision.transform.position;
                EnemyTransform.transform.position = new Vector3(EnemyTransform.transform.position.x + dis.x,
                                                 EnemyTransform.transform.position.y + dis.y, -1);
                StartCoroutine(Frozen());
            }
        }
    }
    public void ResetColor()
    {
        sr.color = original;
    }
    private void setHealthBar() {
        float per = HP / MAXHP;
        per = 0.5f * per;
        Transform [] health  = GetComponentsInChildren<Transform>();
        foreach (var child in health) {
            if (child.name == "EnemyHealth") {
                Vector3 x = child.gameObject.transform.localScale;
                x.x = per;
                child.gameObject.transform.localScale = x;
            }
        }
    }

    IEnumerator Frozen()
    {
        float currentSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        speed = currentSpeed;
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
        setHealthBar();
    }
    private IEnumerator DamageOverTime(float damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            FlashColor(flashTime);
            setHealthBar();
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
