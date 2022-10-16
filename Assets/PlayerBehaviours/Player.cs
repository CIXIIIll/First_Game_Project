using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float MaxHP = 100.0f;
    public float PlayerHP = 100.0f;
    public float MaxMP = 100.0f;
    public float PlayerMP = 100.0f;
    public bool CloseRange = false;
    public float AttackSpeed = 0.3f;
    public bool faceright;
    public Weapon_Data CurrentWeapon;
    public float damage = 50.0f;
    public Skill_List skills;
    // Start is called before the first frame update
    void Start()
    {
         skills = Resources.Load<Skill_List>("Weapon/SkillData/Skill_List");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetWeapon(Weapon_Data weapon) { 
        CurrentWeapon = weapon;
        CloseRange = weapon.CloseWeapon;
        damage = weapon.Damage;
        AttackSpeed = weapon.AttackSpeed; 
    }
    public Skill_Data GetSkill() {
        if (CloseRange)
        {
            return skills.List[0];
        }
        else {
            return skills.List[1];
        }
    }
}
