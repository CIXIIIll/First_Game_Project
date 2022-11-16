using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Player : Character
{
    private int Earth;
    private int Fire;
    private int Air;
    private int Water;
    public  float MaxHP = 100.0f;
    public  float MaxMP = 100.0f;
    public bool CloseRange = false;
    public float AttackSpeed = 1.0f;
    public bool faceright;
    public bool Frozen;
    public int currentLevel;
    public int NextExp;
    public int CurrEXP;
    public bool CannotDamage;
    private Weapon_Data CurrentWeapon;
    private float damage = 50.0f;
    private Skill_List skills;
    public Skill_Data CurrentSkill;
    public float Extra;
    // Start is called before the first frame update
    void Awake()
    {
        offset = new PlayerOffset();
        skills = Resources.Load<Skill_List>("Weapon/SkillData/Skill_List");
        CloseRange = true;
        UpdateSkill();
        Earth = 0;
        Fire = 0;
        Air = 0;
        Water = 0;
        currentLevel = 1;
        CurrEXP = 0;
        NextExp = GetnextLevel(currentLevel);
        offset.speedoffset = 1.0f;
        offset.LifeSteal = 0f;
        offset.defensiveoffset = 1.0f;
        offset.deamgeoffset = 1.0f;
        CannotDamage = false;
        Frozen = false;
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

    }
    public void SetWeapon(Weapon_Data weapon) { 
        CurrentWeapon = weapon;
        CloseRange = weapon.CloseWeapon;
        AttackSpeed = weapon.AttackSpeed;
        UpdateSkill();
    }
    public void UpdateSkill() {
        if (CloseRange)
        {
            CurrentSkill = skills.List[4];
        }
        else {
            CurrentSkill = skills.List[5];
        }
    }
    public float GetDeamge() {
        return ((damage+Earth) * CurrentWeapon.Damage)* offset.deamgeoffset;
    }
    public float GetDeamgeSkill(float Skilldamage)
    {
        return ((Skilldamage + (Water*0.3f)+(Fire*0.5f)) * CurrentWeapon.Damage) * offset.deamgeoffset;
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
        UpdateSkill();
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
                break;

            case 1:
                Earth += 5;
                break;
            case 2:
                Air += 5;
                break;
            case 3:
                Water += 5;
                MaxMP += 5;
                break;

            default:
                break;
        }
        this.SelfHealth(5.0f,0);
    }
}
