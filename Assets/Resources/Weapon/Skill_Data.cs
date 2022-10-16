using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/NewSkill", fileName = "NewSkill")]
public class Skill_Data : ScriptableObject
{
    public string SkillType;
    public bool CloseWeapon;
    public float Damage;
    public float CD;
    public GameObject Weapon_prefab;
    public float DamageTime;
}
