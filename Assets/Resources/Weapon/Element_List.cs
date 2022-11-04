using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Weapon/List/NewElenmentList", fileName = "NewElenmentList")]
public class Element_List : ScriptableObject
{
    public List<Elements_Data> List = new List<Elements_Data>();
}
