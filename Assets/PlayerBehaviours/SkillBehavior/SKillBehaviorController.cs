using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillBehaviorController : MonoBehaviour
{
    private Player player;
    private float holdTime;
    private Animator animator;
    bool isDown;
    Skill_Data CurrentSkill;
    BoxCollider2D AirCbox;
    
    // Start is called before the first frame update
    void Start()
    {
        AirCbox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Skill();
        Timer();
    }
    void Timer()
    {
        if (isDown)
        {
            holdTime += Time.deltaTime;
            float test = holdTime > 3f ? holdTime = 3 : holdTime;
            float hold = test / 3f;
            LongPress x = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<LongPress>();
            x.UpdateBar(hold);

        }
        else if (!isDown && holdTime > 0.5f)
        {
            holdTime = holdTime > 3f ? holdTime = 3 : holdTime;
            float percentum = (holdTime - 0.5f) / (3f - 0.5f) * 1f;
            float damage = (2.5f - 1f) * percentum + 1f;
            player.ReduceMP(CurrentSkill.MPcost);
            player.SetExtra(damage);
            Instantiate(CurrentSkill.Skill_prefab, transform.position, transform.rotation);
            holdTime = 0;
        }
    }
    IEnumerator Invincibilityframe()
    {
        player.CannotDamage = true;
        yield return new WaitForSeconds(0.5f);
        player.CannotDamage = false;
    }
    IEnumerator EnableAirBox()
    {
        AirCbox.enabled = true;
        animator.SetTrigger("isAttack");
        yield return new WaitForSeconds(0.1f);
        Vector3 vec = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;
        if (player.faceright)
        {
            vec.x += 3;
        }
        else
        {
            vec.x -= 3;
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position = vec;
        AirCbox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {  
            collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(150), 0);
        }
    }

    IEnumerator GenAirL(Skill_Data CurrentS, int Repect)
    {
        if (Repect < 5) {
            Repect++;
            Instantiate(CurrentS.Skill_prefab, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(GenAirL(CurrentS, Repect));
        }
    }
    void Skill() {
        CurrentSkill = player.GetCurrentSkill();
        if (Input.GetButtonDown("Skill")) {
            if (player.MP >= CurrentSkill.MPcost) {
                player.ReduceMP(CurrentSkill.MPcost);
                if (player.CloseRange) {
                    switch (CurrentSkill.SkillType) {
                        //Fire
                        case 0:
                            player.Frozen = true;
                            if (player.faceright)
                            {
                                Vector3 vector = player.transform.position;
                                vector.x += 1.5f;
                                Instantiate<GameObject>(CurrentSkill.Skill_prefab, vector, player.transform.rotation, player.transform);
                            }
                            else
                            {
                                Vector3 vector = player.transform.position;
                                vector.x -= 1.5f;
                                Instantiate<GameObject>(CurrentSkill.Skill_prefab, vector, player.transform.rotation, player.transform);
                            }
                            break;
                        //Earth
                        case 1:
                            isDown = true;
                            break;
                        //Air
                        case 2:
                            StartCoroutine(Invincibilityframe());
                            StartCoroutine(EnableAirBox());
                            break;
                        //Water
                        case 3:
                            Instantiate(CurrentSkill.Skill_prefab, transform.position, transform.rotation);
                            break;
                    }       
                }
                else {
                    switch (CurrentSkill.SkillType)
                    {
                        //Fire
                        case 0:
                            Instantiate(CurrentSkill.Skill_prefab, transform.position, transform.rotation);
                            break;
                        //Earth
                        case 1:
                            Instantiate(CurrentSkill.Skill_prefab, transform.position, transform.rotation);
                            break;
                        //Air
                        case 2:
                            StartCoroutine(GenAirL(CurrentSkill, 0));
                            break;
                        //Water
                        case 3:
                            Instantiate(CurrentSkill.Skill_prefab, transform.position, transform.rotation);
                            break;
                    }
                }
            }
        }
        else if (Input.GetButtonUp("Skill"))
        {
            LongPress x = GameObject.FindGameObjectWithTag("EditorOnly").GetComponent<LongPress>();
            x.ResetBar();
            isDown = false;
        }
    }
}
