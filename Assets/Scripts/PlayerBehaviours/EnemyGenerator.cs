using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class will Create The Create Points
/// </summary>
public class EnemyGenerator : MonoBehaviour
{
    private List<GameObject> EnemyGeneratorPoints =new List<GameObject>();
    [SerializeField]
    private GameObject Points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Create Enemy-Create Point 
    /// </summary>
    /// <param name="Positions"></param>
    public void GeneratorPoints(Vector2Int Positions) {
        GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>().TotalPoints += 1;
        Vector3 x = new Vector3(Positions.x, Positions.y, -1f);
        GameObject point = Instantiate(Points, x, new Quaternion());
        EnemyGeneratorPoints.Add(point);
    }
    /// <summary>
    /// Remove the Enemy-Create Points when create new map
    /// </summary>
    public void Clear() {
        foreach (GameObject ob in EnemyGeneratorPoints) {
            if (ob != null)
            {
                DestroyImmediate(ob);
            }
        }
    }
}
