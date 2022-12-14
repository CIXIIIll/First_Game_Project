using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : Character
{
    [SerializeField]
    private int Earth;
    [SerializeField]
    private int Fire;
    [SerializeField]
    private int Air;
    [SerializeField]
    private int Water;
    public  float MaxHP = 100.0f;
    public  float MaxMP = 100.0f;
    public bool CloseRange = false;
    public float AttackSpeed;
    public bool faceright;
    public bool Frozen;
    public int currentLevel;
    public int NextExp;
    public int TotalPoints;
    public int CurrEXP;
    public bool CannotDamage;
    private Weapon_Data CurrentWeapon;
    private float damage = 50.0f;
    public float Extra;
    public int Elements;
    public int stopPoints;
    public int currentPoints;
    public Skill_Data CurrentSkill;
    private Skill_List skills;
    // Start is called before the first frame update
    void Awake()
    {
        skills = Resources.Load<Skill_List>("Weapon/SkillData/Skill_List");
        PlayerOffset = new PlayerOffset();
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
        CannotDamage = false;
        Frozen = false;
        SetOffset(0,1.2f);
    }
    public Skill_Data GetCurrentSkill()
    {
        if (CloseRange)
        {
            switch (Elements)
            {
                //Fire
                case 0:
                    CurrentSkill = skills.List[0];
                    break;
                //Earth
                case 1:
                    CurrentSkill = skills.List[2];
                    break;
                //Air
                case 2:
                    CurrentSkill = skills.List[4];
                    break;
                //Water
                case 3:
                    CurrentSkill = skills.List[6];
                    break;
            }
        }
        else
        {
            switch (Elements)
            {
                //Fire
                case 0:
                    CurrentSkill = skills.List[1];
                    break;
                //Earth
                case 1:
                    CurrentSkill = skills.List[3];
                    break;
                //Air
                case 2:
                    CurrentSkill = skills.List[5];
                    break;
                //Water
                case 3:
                    CurrentSkill = skills.List[7];
                    break;
            }
        }
        return CurrentSkill;
    }
    private int GetnextLevel(int current) {
        int next = (int)Math.Pow(current - 1, 3);
        next += 5;
        next /= 5;
        next *= 20;
        return next;
    }
    // Update is called once per frame
    void Update()
    {
        BossRoom();
    }
    private void BossRoom() {
        if (TotalPoints == 0) {
            GameObject.FindGameObjectWithTag("Tp").GetComponent<Tp>().EnableTp();
        }
    }
    public void SetWeapon(Weapon_Data weapon) { 
        CurrentWeapon = weapon;
        CloseRange = weapon.CloseWeapon;
        AttackSpeed = weapon.AttackSpeed;
    }
    public float GetDeamge() {
        float x = ((damage + Earth) * CurrentWeapon.Damage) * PlayerOffset.deamgeoffset;
        return x;
    }
    public float GetDeamgeSkill(float Skilldamage)
    {
        return ((Skilldamage + (Water*0.3f)+(Fire*0.5f)) * CurrentWeapon.Damage) * PlayerOffset.deamgeoffset;
    }
    public void SetExtra(float value) {
        Extra += value;
        StartCoroutine(ResetExtra(value));
    }
    IEnumerator ResetExtra(float value) {
        yield return new WaitForSeconds(3);
        Extra -= value;
    }
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
        this.SelfHealth(5.0f,0);
    }
}
