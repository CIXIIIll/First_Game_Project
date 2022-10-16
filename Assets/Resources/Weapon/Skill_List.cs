using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/List/NewSkillList", fileName = "NewSkillList")]
public class Skill_List : ScriptableObject
{
    public List<Skill_Data> List = new List<Skill_Data>();
}
