using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/NewSkill", fileName = "NewSkill")]
public class Skill_Data : ScriptableObject
{
    public int SkillType;
    public bool CloseWeapon;
    public float Damage;
    public float MPcost;
    public float DamageTime;
}
