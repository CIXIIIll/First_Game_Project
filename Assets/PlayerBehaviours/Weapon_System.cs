using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_System : MonoBehaviour
{
    public Weapon_Data weapon;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DesotryWeapon() { 
        Destroy(gameObject);
    }
    public Weapon_Data WeaponInfo() {
        return weapon;
    }
}
