using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    public float HP;
    public float MP;
    public PlayerOffset offset;
    private Player player;
    virtual public void CharacterDie() {
       animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
       animator.applyRootMotion = false;
       animator.SetBool("isDie", true);

    }
    public void ReduceMP(float value) { 
        MP -= value;
    }
    public void SelfHealth(float value, float duration) {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        HP += value;
        if (HP >= player.HP) { 
            HP = player.HP;
        }
    }
    private IEnumerator HealthOverTime(float damage, float duration)
    {
        for (int i = 0; i < duration; i++)
        {
            yield return new WaitForSeconds(1);
            HP += damage;
        }
    }
    public void CharacterDamage(float damage, float duration) 
    {
        if (gameObject != null && player != null)
        {
            if (!player.CannotDamage)
            {
                if (duration != 0)
                {
                    StartCoroutine(DamageOverTime(damage, duration));
                }
                else
                {
                    HP -= damage / offset.defensiveoffset;
                    if (HP <= float.Epsilon)
                    {
                        CharacterDie();
                    }
                }
            }
        }
    }
    private IEnumerator DamageOverTime(float damage, float duration) {
        if (!player.CannotDamage)
        {
            for (int i = 0; i < duration; i++)
            {
                yield return new WaitForSeconds(1);
                HP -= damage;
                if (HP <= float.Epsilon)
                {
                    CharacterDie();
                }
            }
        }
    }

}
