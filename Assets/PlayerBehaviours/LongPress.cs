using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPress : MonoBehaviour
{
    Vector3 localScale;
    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateBar(float value)
    {
        localScale.x = value;
        transform.localScale = localScale;
    }
    public void ResetBar()
    {
        localScale.x = 0;
        transform.localScale = localScale;
    }
}
