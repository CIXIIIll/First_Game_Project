using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The Player Object model
/// </summary>
public class Player : Character
{
    /// <summary>
    /// The Attack Type
    /// </summary>
    public bool CloseRange = false;
    /// <summary>
    /// Attack Speed
    /// </summary>
    public float AttackSpeed;
    /// <summary>
    /// The face direction
    /// </summary>
    public bool faceright;
    /// <summary>
    /// Could be able to change face direction?
    /// </summary>
    public bool Frozen;
    /// <summary>
    /// Current Level
    /// </summary>
    public int currentLevel;
    /// <summary>
    /// Exp for Next Level
    /// </summary>
    public int NextExp;
    /// <summary>
    /// Total Distory Enemy Point
    /// </summary>
    public int TotalPoints;
    /// <summary>
    /// Current Distory Enemy Point
    /// </summary>
    public int DestoryPoints;
    /// <summary>
    /// Current Exp
    /// </summary>
    public int CurrEXP;
    /// <summary>
    /// Current Weapon
    /// </summary>
    private Weapon_Data CurrentWeapon;
    /// <summary>
    /// Damage
    /// </summary>
    private float damage = 50;
    /// <summary>
    /// Extra Damage offect
    /// </summary>
    public float Extra;
    /// <summary>
    /// Highest value Elements
    /// </summary>
    public int Elements;
    /// <summary>
    /// How may create poins in total
    /// </summary>
    public int stopPoints;
    /// <summary>
    /// How may create poins currently destory
    /// </summary>
    public int currentPoints;
    /// <summary>
    /// All Skill
    /// </summary>
    private Skill_List skills;
    // Start is called before the first frame update
    void Start()
    {
        skills = Resources.Load<Skill_List>("Weapon/SkillData/Skill_List");
        PlayerOffset = new PlayerOffset();
        base.MaxHP = 100; 
        base.MaxMP = 100;
        base.CannotDamage = false;
        base.MpPs = 1;
        base.HpPs = 1;
        DestoryPoints = 0;
        CloseRange = true;
        Elements = 0;
        Earth = 0;
        Fire = 0;
        Air = 0;
        Water = 0;
        TotalPoints = 0;
        currentLevel = 1;
        CurrEXP = 0;
        currentPoints = 0;
        stopPoints = 50;
        NextExp = GetnextLevel(currentLevel);
        PlayerOffset.speedoffset = 1.0f;
        PlayerOffset.LifeSteal = 0f;
        PlayerOffset.defensiveoffset = 1.0f;
        PlayerOffset.deamgeoffset = 1.0f;
        Frozen = false;
        SetOffset(0,1.2f);
    }
    /// <summary>
    /// Return Current Skill
    /// </summary>
    /// <returns>Current Skill</returns>
    public Skill_Data GetCurrentSkill()
    {
        if (CloseRange)
        {
            switch (Elements)
            {
                //Fire
                case 0:
                    return skills.List[0];
                //Earth
                case 1:
                    return skills.List[2];
                //Air
                case 2:
                    return skills.List[4];
                //Water
                case 3:
                    return skills.List[6];
            }
        }
        else
        {
            switch (Elements)
            {
                //Fire
                case 0:
                    return skills.List[1];
                //Earth
                case 1:
                    return skills.List[3];
                //Air
                case 2:
                    return skills.List[5];
                //Water
                case 3:
                    return skills.List[7];
            }
        }
        return null;
    }
    /// <summary>
    /// Calculate Next level exp
    /// </summary>
    /// <param name="current">current level</param>
    /// <returns>Next level exp</returns>
    private int GetnextLevel(int current) {
        int next = (int)Math.Pow(current - 1, 3);
        next += 5;
        next /= 5;
        next *= 20;
        return next;
    }
    // Update is called once per frame
    new void Update()
    {
        base.Update();
        BossRoom();
    }
    /// <summary>
    /// Enable the Boss fight room
    /// </summary>
    private void BossRoom() {
        if (DestoryPoints == TotalPoints&&(TotalPoints>=1)) {
            GameObject.FindGameObjectWithTag("Tp").GetComponent<Tp>().EnableTp();
        }
    }
    /// <summary>
    /// Set current weapon value
    /// </summary>
    /// <param name="weapon">weapon value</param>
    public void SetWeapon(Weapon_Data weapon) { 
        CurrentWeapon = weapon;
        CloseRange = weapon.CloseWeapon;
        AttackSpeed = weapon.AttackSpeed;
    }
    /// <summary>
    /// Return the deamge for attack
    /// </summary>
    /// <returns>deamge</returns>
    public float GetDeamge() {
        float x = ((damage + Earth) * CurrentWeapon.Damage) * PlayerOffset.deamgeoffset;
        return x;
    }

    /// <summary>
    /// Return the deamge for Skill
    /// </summary>
    /// <param name="Skilldamage">base skill damage</param>
    /// <returns>deamge</returns>
    public float GetDeamgeSkill(float Skilldamage)
    {
        return ((Skilldamage + (Water*0.3f)+(Fire*0.5f)) * CurrentWeapon.Damage) * PlayerOffset.deamgeoffset;
    }
    /// <summary>
    /// Set Extra offset
    /// </summary>
    /// <param name="value">Extra value</param>
    public void SetExtra(float value) {
        Extra += value;
        StartCoroutine(ResetExtra(value));
    }
    /// <summary>
    /// Reset the extra offset
    /// </summary>
    /// <param name="value">Extra value</param>
    /// <returns></returns>
    IEnumerator ResetExtra(float value) {
        yield return new WaitForSeconds(3);
        Extra -= value;
    }
    /// <summary>
    /// Receive Element
    /// </summary>
    /// <param name="type">Element type</param>
    public void ReceiveElement(int type) {
        CurrEXP += 5;
        if (CurrEXP >= NextExp) {
            currentLevel += 1;
            NextExp = GetnextLevel(currentLevel);
            MaxHP += 25;
            CurrEXP = 0;
        }
        switch (type) {
            case 0:
                Fire += 5;
                if (Fire > Earth && Fire > Air && Fire > Water) {
                    Elements = 0;
                    SetOffset(0,1.2f);
                }
                break;

            case 1:
                Earth += 5;
                if (Earth > Fire && Earth > Air && Earth > Water)
                {
                    Elements = 1;
                    SetOffset(1, 1.2f);
                }
                break;
            case 2:
                Air += 5;
                if (Air > Fire && Air > Earth && Air > Water)
                {
                    Elements = 2;
                    SetOffset(2, 1.2f);
                }
                break;
            case 3:
                Water += 5;
                MaxMP += 5;
                if (Water > Fire && Water > Earth && Water > Air)
                {
                    Elements = 3;
                    SetOffset(3, 0.2f);
                }
                break;

            default:
                break;
        }
        this.SelfHealth(5.0f);
    }
}
