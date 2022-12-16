using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will control Enemy_Create Points behaviour 
/// </summary>
public class EnemyCreatePoints : MonoBehaviour
{
    /// <summary>
    /// Long Range Attack Enemy Prefab
    /// </summary>
    [SerializeField]
    private GameObject Long;
    /// <summary>
    /// Close Range Attack (Bat) Enemy Prefab
    /// </summary>
    [SerializeField]
    private GameObject Bat;
    /// <summary>
    /// Create time between two enemy Default 10 sec 
    /// </summary>
    [SerializeField]
    private float CreateTime;
    /// <summary>
    /// Total Value of enmey to create Default 50
    /// </summary>
    [SerializeField]
    private int totalValue;
    /// <summary>
    /// Curretn time for create enemy
    /// </summary>
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime+= Time.deltaTime;
        CreateEnemy();
    }
    /// <summary>
    /// Create enemy
    /// </summary>
    private void CreateEnemy() {
        GameObject enemy;
        if (this.transform.position != null) {
            Vector3 pos = this.transform.position;
            pos.z = -1f;
            if (currentTime >= CreateTime)
            {
                if (totalValue > 0)
                {
                    int x = Random.Range(0, 4);
                    switch (x)
                    {
                        case 0:
                            enemy = Instantiate(Long, pos, new Quaternion());
                            totalValue -= 2;
                            break;
                        default:
                            enemy = Instantiate(Bat, pos, new Quaternion());
                            totalValue -= 1;
                            break;
                    }
                    currentTime = 0;
                }
            }
        }
    }
    /// <summary>
    /// Destory the point
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           if (collision.GetComponent<Player>() != null)
           {
                if (collision.GetComponent<Player>().currentPoints >= 30) {
                    collision.GetComponent<Player>().currentPoints = 0;
                    collision.GetComponent<Player>().DestoryPoints += 1;
                    Destroy(gameObject);
                }
           }
        }
    }
}
