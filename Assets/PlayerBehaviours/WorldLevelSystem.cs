using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLevelSystem : MonoBehaviour
{
    private float CurrentTime;
    private float UpdateTime;
    public int World_Level;
    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = 0;
        UpdateTime = 0;
        World_Level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentTime += Time.deltaTime;
        UpdateLevel();
    }
    private void UpdateLevel() {
        if (CurrentTime - UpdateTime > 30f) {
            World_Level++;
            UpdateTime = CurrentTime;
        }
    }
    
}
