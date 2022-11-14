using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    // Start is called before the first frame update
    private new void Start()
    {
        base.HP = 100.0f;
        base.Deamge = 10.0f;
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
    }
}
