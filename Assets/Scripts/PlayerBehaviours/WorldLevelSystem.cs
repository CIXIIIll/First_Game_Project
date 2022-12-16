using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Level Change system for enemy
/// </summary>
public class WorldLevelSystem : MonoBehaviour
{
    /// <summary>
    /// overall gaming time
    /// </summary>
    private float CurrentTime;
    /// <summary>
    /// Time to level up
    /// </summary>
    private float UpdateTime;
    /// <summary>
    /// World Level
    /// </summary>
    public int World_Level;
    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data 
    /// </summary>
    void Start()
    {
        CurrentTime = 0;
        UpdateTime = 0;
        World_Level = 0;
    }
    /// <summary>
    /// Update timer
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        UpdateLevel();
    }
    /// <summary>
    /// Update Level
    /// </summary>
    private void UpdateLevel() {
        if (CurrentTime - UpdateTime > 30f) {
            World_Level++;
            UpdateTime = CurrentTime;
        }
    }
    
}
