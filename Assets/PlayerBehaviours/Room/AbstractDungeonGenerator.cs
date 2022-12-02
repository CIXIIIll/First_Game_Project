using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    [SerializeField]
    protected TilemapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    private void Awake()
    {
        GenerateDungeon();
    }
    public void GenerateDungeon() {
        tilemapVisualizer.Clear();
        RunPorceduralGeneration();
    }

    protected abstract void RunPorceduralGeneration();
}
