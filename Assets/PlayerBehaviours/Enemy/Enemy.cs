using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color original;
    public float HP;
    public float MAXHP;
    public float Deamge;
    private float BaseDeamge;
    public bool Close;
    public bool Boss;
    private float BaseHp;
    public Transform playerTransform;
    public float speed;
    public float Rspeed;
    public float radius;
    public float flashTime;
    public float LevelHp;
    public float LevelDeamge;
    public Transform EnemyTransform;
    public int value;
    public Rigidbody2D rb2d;
    public bool forzen;
    private WorldLevelSystem WLevel;
    // Start is called before the first frame update
    public void Start()
    {
        BaseHp = HP;
        BaseDeamge = Deamge;
        forzen = false;
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
        WLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<WorldLevelSystem>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void End() {
        SceneManager.LoadScene("GameOver");
    }
    // Update is called once per frame
    public void Update()
    {
        MAXHP = BaseHp + (LevelHp*WLevel.World_Level);
        Deamge = BaseDeamge + (LevelDeamge * WLevel.World_Level);
        if (HP <= 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().currentPoints += value;
            Destroy(gameObject);
            if (Boss)
            {
                End();
            }
            else {
                GameObject.FindGameObjectWithTag("Element").GetComponent<ItemController>().GenerateItme(transform);
            }
        }
        if (!forzen) {
            enemyMove();
        }
    }
    public void ResetSpeed()
    {
        speed = Rspeed;
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
                if (Close)
                {
                    Vector2 temp = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                    rb2d.MovePosition(temp);
                }
                else {
                    if (a > 0)
                    {
                        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                    }
                    else {
                        transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                    }
                }

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
                if (!Boss) {
                    Vector3 dis = EnemyTransform.position - collision.transform.position;
                    EnemyTransform.transform.position = new Vector3(EnemyTransform.transform.position.x + dis.x,
                                                     EnemyTransform.transform.position.y + dis.y, -1);
                    StartCoroutine(Frozen());
                }
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
}
