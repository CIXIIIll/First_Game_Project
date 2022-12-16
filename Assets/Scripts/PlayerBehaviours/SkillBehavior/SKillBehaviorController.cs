using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillBehaviorController : MonoBehaviour
{
    /// <summary>
    /// Player object
    /// </summary>
    private Player player;
    /// <summary>
    /// Hold time for long press
    /// </summary>
    private float holdTime;
    /// <summary>
    /// Player animator
    /// </summary>
    private Animator animator;
    /// <summary>
    /// Hold or not
    /// </summary>
    bool isDown;
    /// <summary>
    /// Current Skill
    /// </summary>
    Skill_Data CurrentSkill;
    /// <summary>
    /// Air Close hit box
    /// </summary>
    BoxCollider2D AirCbox;
    /// <summary>
    /// Skill Prefab List
    /// </summary>
    private Skill_Prefab_List Skill_Prefab_List;
    
    // Start is called before the first frame update
    void Start()
    {
        AirCbox = GetComponent<BoxCollider2D>();
        Skill_Prefab_List = Resources.Load<Skill_Prefab_List>("Weapon/SkillData/SkillPrefabList");
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
        animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Skill();
        Timer();
    }
    /// <summary>
    /// The timer for long press 
    /// </summary>
    void Timer()
    {
        if (isDown)
        {
            holdTime += Time.deltaTime;
            float test = holdTime > 3f ? holdTime = 3 : holdTime;
            float hold = test / 3f;
            LongPress x = GameObject.FindGameObjectWithTag("GameUI").GetComponent<LongPress>();
            x.UpdateBar(hold);

        }
        else if (!isDown && holdTime > 0.5f)
        {
            /// Set holdtime if greate of 3 seconds
            holdTime = holdTime > 3f ? holdTime = 3 : holdTime;
            float percentum = (holdTime - 0.5f) / (3f - 0.5f) * 1f;
            float damage = (2.5f - 1f) * percentum + 1f;
            player.ReduceMP(CurrentSkill.MPcost);
            player.SetExtra(damage);
            GameObject Skill = Skill_Prefab_List.List[2];
            GetSkillDemage();
            Skill.gameObject.GetComponent<EarthC>().Skill_Data = CurrentSkill;
            Instantiate(Skill, transform.position, transform.rotation);
            holdTime = 0;
        }
    }
    /// <summary>
    /// Update skill Deamage
    /// </summary>
    private void GetSkillDemage() {
        float tempDeamage = CurrentSkill.Damage;
        CurrentSkill.Damage = player.GetDeamgeSkill(tempDeamage);
    }
    /// <summary>
    /// Set Invincibility frame
    /// </summary>
    /// <returns></returns>
    IEnumerator Invincibilityframe()
    {
        player.CannotDamage = true;
        yield return new WaitForSeconds(0.5f);
        player.CannotDamage = false;
    }
    /// <summary>
    /// Set Close Range Air Skill Hit box
    /// </summary>
    /// <returns></returns>
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
    /// <summary>
    /// Find any enemy object in Close Range Air Skill Hit box
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().CharacterDamage(player.GetDeamgeSkill(150), 0);
            }
        }
    }
    /// <summary>
    /// Generate 5 Long Range Air Skill
    /// </summary>
    /// <param name="Air">Prefab of Air Skill</param>
    /// <param name="Repect">Current repect times</param>
    /// <returns></returns>
    IEnumerator GenAirL(GameObject Air, int Repect)
    {
        if (Repect < 5) {
            Repect++;
            Instantiate(Air, transform.position, transform.rotation);
            ///reate for 0.5 seconds to generate next one
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(GenAirL(Air, Repect));
        }
    }
    /// <summary>
    /// Skill System to find skill and use it
    /// </summary>
    void Skill() {
        CurrentSkill = player.GetCurrentSkill();
        GameObject Skill;
        if (Input.GetButtonDown("Skill")) {
            if (player.Frozen) {
                ///Stop the Fire close Skill
                player.transform.GetChild(0).GetComponent<FireC>().StopSkill();
            }
            else if(player.MP >= CurrentSkill.MPcost) {
                player.ReduceMP(CurrentSkill.MPcost);
                if (player.CloseRange) {
                    switch (CurrentSkill.SkillType) {
                        //Fire
                        case 0:
                            player.Frozen = true;
                            Skill = Skill_Prefab_List.List[0];
                            GetSkillDemage();
                            Vector3 vector = player.transform.position;
                            if (player.faceright)
                            {
                                vector.x += 1.5f;
                            }
                            else
                            {
                                vector.x -= 1.5f;
                            }
                            Instantiate<GameObject>(Skill, vector, player.transform.rotation, player.transform);
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
                            Skill = Skill_Prefab_List.List[6];
                            GetSkillDemage();
                            Skill.gameObject.GetComponent<WaterC>().Skill_Data = CurrentSkill;
                            Instantiate(Skill, transform.position, transform.rotation);
                            break;
                    }       
                }
                else {
                    switch (CurrentSkill.SkillType)
                    {
                        //Fire
                        case 0:
                            Skill = Skill_Prefab_List.List[1];
                            GetSkillDemage();
                            Skill.gameObject.GetComponent<FireL>().Skill_Data = CurrentSkill;
                            Skill.gameObject.GetComponent<FireL>().next = player.GetDeamgeSkill(50);
                            Instantiate(Skill, transform.position, transform.rotation);
                            break;
                        //Earth
                        case 1:
                            Skill = Skill_Prefab_List.List[3];
                            GetSkillDemage();
                            Skill.gameObject.GetComponent<EarthL>().Skill_Data = CurrentSkill;
                            Instantiate(Skill, transform.position, transform.rotation);
                            break;
                        //Air
                        case 2:
                            Skill = Skill_Prefab_List.List[5];
                            GetSkillDemage();
                            Skill.gameObject.GetComponent<AirL>().Skill_Data = CurrentSkill;
                            StartCoroutine(GenAirL(Skill, 0));
                            break;
                        //Water
                        case 3:
                            Skill = Skill_Prefab_List.List[7];
                            GetSkillDemage();
                            Skill.gameObject.GetComponent<WaterL>().Skill_Data = CurrentSkill;
                            Instantiate(Skill, transform.position, transform.rotation);
                            break;
                    }
                }
            }
        }
        else if (Input.GetButtonUp("Skill"))
        {
            LongPress x = GameObject.FindGameObjectWithTag("GameUI").GetComponent<LongPress>();
            x.ResetBar();
            isDown = false;
        }
    }
}
