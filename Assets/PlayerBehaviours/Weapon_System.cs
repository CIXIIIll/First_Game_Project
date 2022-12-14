using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_System : MonoBehaviour
{
    public Weapon_Data weapon;
    public float DestoryTime;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
        DestoryTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DestoryTime+=Time.deltaTime;
        if (DestoryTime > 1) {
            box.enabled = true;
        }
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
