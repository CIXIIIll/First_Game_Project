using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Long Press System
/// </summary>
public class LongPress : MonoBehaviour
{
    /// <summary>
    /// the size of Long Press bar
    /// </summary>
    Vector3 localScale;
    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data
    /// </summary>
    void Start()
    {
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// change the size of Long Press bar
    /// </summary>
    /// <param name="value"></param>
    public void UpdateBar(float value)
    {
        localScale.x = value;
        transform.localScale = localScale;
    }

    /// <summary>
    /// reset the size of Long Press bar
    /// </summary>
    public void ResetBar()
    {
        localScale.x = 0;
        transform.localScale = localScale;
    }
}
