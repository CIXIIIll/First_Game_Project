using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Parameter_",menuName = "PCG/Raandom_Walk_Data")]
public class RandomWalkData : ScriptableObject
{
    public int iterations = 10;
    public int Length = 10;
    public bool startRandomly = true;
}
