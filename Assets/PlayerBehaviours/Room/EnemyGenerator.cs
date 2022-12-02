using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void GeneratorPoints(Vector2Int Positions) {
        Vector3 x = new Vector3(Positions.x, Positions.y, -1f);
        GameObject point = Instantiate(Points, x, new Quaternion());
        EnemyGeneratorPoints.Add(point);
    }
    public void Clear() {
        foreach (GameObject ob in EnemyGeneratorPoints) {
            if (ob != null)
            {
                DestroyImmediate(ob);
            }
        }
    }
}
