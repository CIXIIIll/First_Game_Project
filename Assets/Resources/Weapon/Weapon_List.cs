using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/List/NewWeaponList", fileName = "NewWeaponList")]
public class Weapon_List : ScriptableObject
{
    public List<Weapon_Data> List = new List<Weapon_Data>();
}
