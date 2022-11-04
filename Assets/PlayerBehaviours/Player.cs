using Assets.PlayerBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    private int Earth = 0;
    private int Fire = 0;
    public  float MaxHP = 100.0f;
    public  float PlayerHP = 100.0f;
    public  float MaxMP = 100.0f;
    public  float PlayerMP = 100.0f;
    public bool CloseRange = false;
    public float AttackSpeed = 1.0f;
    public bool faceright;
    public Weapon_Data CurrentWeapon;
    private float damage = 50.0f;
    private Skill_List skills;
    public Skill_Data CurrentSkill;
    // Start is called before the first frame update
    void Start()
    {
        offset = new PlayerOffset();
        skills = Resources.Load<Skill_List>("Weapon/SkillData/Skill_List");
        CurrentSkill = GetSkill();
        offset.speedoffset = 1.0f;
        offset.LifeSteal = 0f;
        offset.defensiveoffset = 1.0f;
        offset.deamgeoffset = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWeapon(Weapon_Data weapon) { 
        CurrentWeapon = weapon;
        CloseRange = weapon.CloseWeapon;
        AttackSpeed = weapon.AttackSpeed; 
    }
    public Skill_Data GetSkill() {
        if (CloseRange)
        {
            return skills.List[1];
        }
        else {
            return skills.List[1];
        }
    }
    public float GetDeamge() {
        return ((damage+Fire) * CurrentWeapon.Damage)* offset.deamgeoffset;
    }
    public void ReceiveElement(int type) {
        switch (type) {
            case 0:
                Fire += 5;
                break;

            case 1:
                Earth += 5;
                break;

            default:
                break;
        }
        this.SelfHealth(5.0f,0);
    }
}
