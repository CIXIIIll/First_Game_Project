using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Weapon Behaviour
/// </summary>
public class Weapon_System : MonoBehaviour
{
    /// <summary>
    /// weapon info
    /// </summary>
    public Weapon_Data weapon;
    /// <summary>
    /// Destory Time
    /// </summary>
    public float DestoryTime;
    /// <summary>
    /// Hit box
    /// </summary>
    private BoxCollider2D box;
    /// <summary>
    /// Timer 
    /// </summary>
    public bool hand;
    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data 
    /// </summary>
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        DestoryTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hand) {
            DestoryTime += Time.deltaTime;
        }
        /// avoid Infinite swtich
        if (DestoryTime > 1) {
            box.enabled = true;
        }
        /// destory weapon
        if (DestoryTime >= 20) { 
            Destroy(gameObject);
        }
    }
    public void DesotryWeapon() { 
        Destroy(gameObject);
    }
    public Weapon_Data WeaponInfo() {
        return weapon;
    }
}
