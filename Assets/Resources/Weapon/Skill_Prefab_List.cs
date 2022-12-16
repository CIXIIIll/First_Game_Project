using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Skill/List/NewSkillPrefabList", fileName = "SkillPrefabList")]
public class Skill_Prefab_List : ScriptableObject
{
    public List<GameObject> List = new List<GameObject>();
}
