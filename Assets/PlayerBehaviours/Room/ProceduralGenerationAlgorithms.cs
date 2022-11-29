using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int start, int Length) {
        HashSet<Vector2Int> path = new HashSet<Vector2Int> ();
        path.Add (start);
        var previousposition = start;
        for (int i = 0; i < Length; i++) {
            var newPosition = previousposition + Direction2D.GetRandom();
            path.Add (newPosition);
            previousposition = newPosition;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandom();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }
        return corridor;
    }

}
public static class Direction2D
{
    public static List<Vector2Int> Directions = new List<Vector2Int> {
        new Vector2Int (0, 1),
        new Vector2Int (1, 0),
        new Vector2Int (0, -1),
        new Vector2Int (-1, 0),
    };
    public static List<Vector2Int> cardinalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1),
        new Vector2Int(1,0), 
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0) 
    };
    public static Vector2Int GetRandom() { 
        return Directions[Random.Range (0, Directions.Count)];
    }

}


