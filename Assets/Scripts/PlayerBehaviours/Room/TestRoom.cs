using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoom : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    CorridorDungeonGeneration generator;
    void Start()
    {
        tilemapVisualizer.Clear();
        generator = GameObject.Find("CoridoerDungeonGenerator").GetComponent<CorridorDungeonGeneration>();
        generator.GenerateDungeon();
    }
}
