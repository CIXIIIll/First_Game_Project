using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : AbstractDungeonGenerator
{
    [SerializeField]
    protected RandomWalkData randomParameter, rommGenerationParameters;

    protected override void RunPorceduralGeneration() {
        HashSet<Vector2Int> floor = RunRandomWalk(randomParameter, startPosition);
        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }
    public HashSet<Vector2Int> RunRandomWalk(RandomWalkData parameters,Vector2Int position)
    {
        var current = position;
        HashSet<Vector2Int> floorPostions = new HashSet<Vector2Int>();
        for (int i = 0; i < randomParameter.iterations; i++) {
            var path = ProceduralGenerationAlgorithms.SimpleRandomWalk(current, randomParameter.Length);
            floorPostions.UnionWith(path);
            if (randomParameter.startRandomly)
            {
                current = floorPostions.ElementAt(Random.Range(0, floorPostions.Count));
            }
        }
        return floorPostions;
    }
    // Start is called before the first frame update
}
