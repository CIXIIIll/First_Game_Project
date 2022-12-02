using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatePoints : MonoBehaviour
{
    [SerializeField]
    private GameObject Giant;
    [SerializeField]
    private GameObject Bat;
    [SerializeField]
    private float CreateTime;
    [SerializeField]
    private int totalValue;
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
    private void CreateEnemy() {
        GameObject enemy;
        Vector3 pos = this.transform.position;
        pos.z = -1f;
        if (currentTime >= CreateTime) {
            if (totalValue > 0) { 
                int x = Random.Range(0, 2);
                switch (x) { 
                    case 0:
                        enemy = Instantiate(Giant, pos, new Quaternion());
                        totalValue -= 5;
                        break;
                    case 1: 
                        enemy = Instantiate(Bat, pos, new Quaternion());
                        totalValue -= 1;
                         break;
                    case 2:
                        enemy = Instantiate(Bat, pos, new Quaternion());
                        totalValue -= 1;
                        break;
                    default:
                        break;
                }
                currentTime = 0;
            }
        }
    }
}
