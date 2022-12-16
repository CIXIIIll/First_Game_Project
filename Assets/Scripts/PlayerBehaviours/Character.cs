using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is the battle value change behaivor for the player object
/// </summary>
public class Character : MonoBehaviour
{
    /// <summary>
    /// Amount of Earth Element
    /// </summary>
    [SerializeField]
    protected int Earth;
    /// <summary>
    /// Amount of Fire Element
    /// </summary>
    [SerializeField]
    protected int Fire;
    /// <summary>
    /// Amount of Air Element
    /// </summary>
    [SerializeField]
    protected int Air;
    /// <summary>
    /// Amount of Water Element
    /// </summary>
    [SerializeField]
    protected int Water;
    /// <summary>
    /// Player Current HP
    /// </summary>
    public float HP;
    /// <summary>
    /// Player Current MP
    /// </summary>
    public float MP;
    /// <summary>
    /// Player Max HP
    /// </summary>
    public float MaxHP;
    /// <summary>
    /// Player Max MP
    /// </summary>
    public float MaxMP;
    /// <summary>
    /// Could Player could be able to damage by enemy?
    /// </summary>
    public bool CannotDamage;
    /// <summary>
    /// 4 different offect effect by highest elements
    /// </summary>
    public PlayerOffset PlayerOffset;
    /// <summary>
    /// Base Add Mp per seconds
    /// </summary>
    protected float MpPs;
    /// <summary>
    /// Base Add Hp per seconds
    /// </summary>
    protected float HpPs;
    /// <summary>
    /// Timer for adding Mp and HP 
    /// </summary>
    private float Timer;
    /// <summary>
    /// Evenet for Player Die
    /// Change The Scene to GameOver
    /// </summary>
    protected void Update()
    {
        Timer+=Time.deltaTime;
        if (Timer > 1) {
            Timer = 0;
            SelfHealthOverTime();
        }
    }
    virtual public void CharacterDie() {

        SceneManager.LoadScene("GameOver");

    }
    /// <summary>
    /// Set the Offect
    /// </summary>
    /// <param name="type">The Element Tyep</param>
    /// <param name="value">The value want to set</param>
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
    /// <summary>
    /// Self Health MP and HP Over the time
    /// </summary>
    void SelfHealthOverTime()
    {
        HP += HpPs + (Earth*0.1f + Water *0.1f);
        MP += MpPs+ (Water * 0.1f);
        if (MP >= MaxMP)
        {
            MP = MaxMP;
        }
        if (HP >= MaxHP)
        {
            HP = MaxHP;
        }
    }
    /// <summary>
    /// Reduce Player MP
    /// </summary>
    /// <param name="value">The amount of Mp reduce</param>
    public void ReduceMP(float value) { 
        MP -= value;
    }
    /// <summary>
    /// Health Player HP
    /// </summary>
    /// <param name="value">the amount to Health</param>
    public void SelfHealth(float value) {
        HP += value;
        if (HP >= MaxHP) { 
            HP = MaxHP;
        }
    }
    /// <summary>
    /// Make the damage to the player and reduce the Hp
    /// </summary>
    /// <param name="damage">the value of the damage</param>
    /// <param name="duration">to take damage over the time, if only once set to 0</param>
    public void CharacterDamage(float damage, float duration) 
    {
        if (gameObject != null)
        {
            if (!CannotDamage)
            {
                if (duration != 0)
                {
                    //if is take damage over the time call other function
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
    /// <summary>
    /// to take damage over the time
    /// </summary>
    /// <param name="damage">the value of the damage</param>
    /// <param name="duration">the total time damaged</param>
    /// <returns></returns>
    private IEnumerator DamageOverTime(float damage, float duration) {
        if (!CannotDamage)
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
