using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/List/NewWeaponPrefabList", fileName = "WeaponPrefabList")]
public class Weapon_Prefab_List : ScriptableObject
{
    public List<GameObject> List = new List<GameObject>();
}
