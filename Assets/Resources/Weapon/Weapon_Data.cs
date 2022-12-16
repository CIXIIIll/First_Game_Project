using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/NewWeapon", fileName ="NewWeapon")]
//All Weapons Data Template
public class Weapon_Data : ScriptableObject
{
    public int index;
    public Sprite weapon_sprite;
    public string WeaponName;
    public bool CloseWeapon;
    public string WeaponInfo;
    public float Damage;
    public float AttackSpeed;
}
