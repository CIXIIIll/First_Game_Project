using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    private Animator animator;
    public float HP;
    public float MP;
    public PlayerOffset PlayerOffset;
    private Player player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    virtual public void CharacterDie() {

        SceneManager.LoadScene("GameOver");

    }
    protected void SetOffset(int type, float value)
    {
        PlayerOffset = new PlayerOffset();
        PlayerOffset.defensiveoffset = 1;
        PlayerOffset.deamgeoffset = 1;
        PlayerOffset.speedoffset = 1;
        PlayerOffset.LifeSteal = 0;
        switch (type) { 
            case 0:
                PlayerOffset.defensiveoffset = value;
                break;
            case 1:
                PlayerOffset.defensiveoffset = value;
                break;
            case 2:
                PlayerOffset.speedoffset = value;
                break;
            case 3:
                PlayerOffset.LifeSteal = value;
                break;
        }
    }
    public void ReduceMP(float value) { 
        MP -= value;
    }
    public void SelfHealth(float value, float duration) {
        HP += value;
        if (HP >= player.MaxHP) { 
            HP = player.MaxHP;
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
                    HP -= damage / PlayerOffset.defensiveoffset;
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
