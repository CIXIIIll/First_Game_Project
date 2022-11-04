using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Element/NewElement", fileName = "NewElement")]
public class Elements_Data : ScriptableObject
{
    public int ElementID;
    public string ElementName;
    public GameObject Prefab;
}

