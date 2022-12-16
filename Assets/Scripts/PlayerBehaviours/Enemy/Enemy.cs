using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
/// <summary>
/// The Enemy Behaviour controller
/// </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>
    /// Hit box Object
    /// </summary>
    BoxCollider2D boxCollider;
    /// <summary>
    /// SpriteRenderer Object to change color
    /// </summary>
    private SpriteRenderer sr;
    /// <summary>
    /// The original color
    /// </summary>
    private Color original;
    /// <summary>
    /// Enemy Current Hp
    /// </summary>
    public float HP;
    /// <summary>
    /// Enemy Max HP
    /// </summary>
    public float MAXHP;
    /// <summary>
    /// Enenmy Current Damage
    /// </summary>
    public float Damage;
    /// <summary>
    /// Enemy Base Damage
    /// </summary>
    private float BaseDamage;
    /// <summary>
    /// Close range attack?
    /// </summary>
    public bool Close;
    /// <summary>
    /// is Boss?
    /// </summary>
    public bool Boss;
    /// <summary>
    /// Enemy Base Hp
    /// </summary>
    private float BaseHp;
    /// <summary>
    /// Player Position
    /// </summary>
    public Transform playerTransform;
    /// <summary>
    /// Enemy Move Speed
    /// </summary>
    public float speed;
    /// <summary>
    /// Enemy original Move Speed
    /// </summary>
    public float originalSpeed;
    /// <summary>
    /// Enemy Chasing radius Default bat = 15, Giant = 50, long = 10
    /// </summary>
    public float radius;
    /// <summary>
    /// Flash Time
    /// </summary>
    public float flashTime = 0.2f;
    /// <summary>
    /// Enemy increase Hp for Level
    /// </summary>
    public float LevelHp;
    /// <summary>
    /// Enemy increase Deamge for Level
    /// </summary>
    public float LevelDeamge;
    /// <summary>
    /// Enemy Position
    /// </summary>
    public Transform EnemyTransform;
    /// <summary>
    /// Enemy value Points
    /// </summary>
    public int value;
    /// <summary>
    /// Rigidbody2D Object
    /// </summary>
    public Rigidbody2D rb2d;
    /// <summary>
    /// Could Enemy be able to move?
    /// </summary>
    public bool forzen;
    /// <summary>
    /// Current World Level
    /// </summary>
    private WorldLevelSystem WLevel;
    /// <summary>
    /// Randmo Move timer
    /// </summary>
    [SerializeField]
    private float moveTimer;
    /// <summary>
    /// Randmo Move Time
    /// </summary>
    [SerializeField]
    private float moveTime =3;
    private Vector3 nextPosition = new Vector3(0, 0, -1);
    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data 
    /// </summary>

    protected void Start()
    {
        moveAuto();
        moveTimer = 0;
        BaseHp = HP;
        BaseDamage = Damage;
        forzen = false;
        boxCollider = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        original = sr.color;
        WLevel = GameObject.FindGameObjectWithTag("Level").GetComponent<WorldLevelSystem>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // Update is called once per frame
    protected void Update()
    {
        moveTimer += Time.deltaTime;
        MAXHP = BaseHp + (LevelHp*WLevel.World_Level);
        Damage = BaseDamage + (LevelDeamge * WLevel.World_Level);
        if (HP <= 0)
        {
            GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().currentPoints += value;
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
            float distance = (transform.position - playerTransform.position).sqrMagnitude;
            if (distance <= radius) {
                enemyMove();
            }
            else
            {
                if (moveTime - moveTimer>= 0)
                {
                    Vector3 currentPosition = rb2d.position;
                    currentPosition = rb2d.position;
                    currentPosition += nextPosition * speed * Time.deltaTime;
                    Vector2 temp = Vector2.MoveTowards(transform.position, currentPosition, speed * Time.deltaTime);
                    rb2d.MovePosition(temp);
                }
                else {
                    nextPosition = moveAuto();
                    moveTimer = 0;
                }
            }
        }
    }
    /// <summary>
    /// End of game
    /// </summary>
    private void End()
    {
        SceneManager.LoadScene("GameOver");
    }
    // Update is called once per frame
    /// <summary>
    /// Reset the Speed
    /// </summary>
    public void ResetSpeed()
    {
        speed = originalSpeed;
    }
    /// <summary>
    /// Enemy Movement controller
    /// </summary>
    private void enemyMove() {
        if (playerTransform != null)
        {
            float Face = transform.position.x - playerTransform.position.x; 
            Vector3 vec = transform.localScale;
            if (Face > 0) {
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
            if (Close)
            {
                Vector2 temp = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
                rb2d.MovePosition(temp);
            }
            else
            {
                if (Face > 0)
                {
                    transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                }
                else
                {
                    transform.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                }
            }
        }

    }
    /// <summary>
    /// Get the position for next random move
    /// </summary>
    Vector3 moveAuto() { 
        System.Random rand = new System.Random();
        float x = rand.Next(-1, 2);
        float y = rand.Next(-1, 2);
        Vector3 movePlay = new Vector3(x, y,-1);
        if (x != 0) { 
            Vector3 TempScale = this.transform.localScale;
            TempScale.x = Math.Abs(TempScale.x) * x;
        }
        return movePlay;
    }
    /// <summary>
    /// Flash Color to red when enemy being attack
    /// </summary>
    /// <param name="time"></param>
    private void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBehavior"))
        {
            if (collision.GetComponent<Player>() != null) {
                collision.GetComponent<Player>().CharacterDamage(Damage, 0);
                if (!Boss) {
                    Vector3 dis = EnemyTransform.position - collision.transform.position;
                    EnemyTransform.transform.position = new Vector3(EnemyTransform.transform.position.x + dis.x,
                                                     EnemyTransform.transform.position.y + dis.y, -1);
                    StartCoroutine(Frozen());
                    StartCoroutine(CloseHitBox());
                }
            }
        }
        if (collision.gameObject.CompareTag("Wall")) {
            moveAuto();
        }
    }
    /// <summary>
    /// reset the color
    /// </summary>
    public void ResetColor()
    {
        sr.color = original;
    }

    /// <summary>
    /// forzen the enemy to not move
    /// </summary>
    /// <returns></returns>
    IEnumerator Frozen()
    {
        float currentSpeed = speed;
        speed = 0;
        yield return new WaitForSeconds(0.5f);
        speed = currentSpeed;
    }
    /// <summary>
    /// Get damage from player
    /// </summary>
    /// <param name="damage">damage</param>
    /// <param name="duration">damage time in second</param>
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
    /// <summary>
    /// get damage over time by player
    /// </summary>
    /// <param name="damage">damage</param>
    /// <param name="duration">damage time in second</param>
    /// <returns></returns>
    private IEnumerator DamageOverTime(float damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            FlashColor(flashTime);
            HP -= damage;
        }
    }
    /// <summary>
    /// Reset the Hit box when Enemy hit player
    /// </summary>
    /// <returns></returns>
    private IEnumerator CloseHitBox()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
    }
}
