using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreatePoints : MonoBehaviour
{
    [SerializeField]
    private GameObject Long;
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
