using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentWeapon : MonoBehaviour
{
    GameObject weaponTrans;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Weapon_Data weaponData = Resources.Load<Weapon_Data>("Weapon/WeaponData/Sword");
        weaponTrans = Instantiate(weaponData.Weapon_prefab);
        player.SetWeapon(weaponData);
        weaponTrans.transform.parent = transform;
        weaponTrans.transform.position = transform.position;
        weaponTrans.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void UpdateWeapon(Weapon_Data weapon) {
        if (weaponTrans != null) {
            Destroy(weaponTrans);
        }
        weaponTrans = Instantiate(weapon.Weapon_prefab);
        weaponTrans.transform.parent = transform;
        weaponTrans.transform.position = transform.position;
        weaponTrans.transform.rotation = transform.rotation;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Weapon_Data weapon = collision.GetComponent<Weapon_System>().WeaponInfo();
            UpdateWeapon(weapon);
            player.SetWeapon(weapon);
            collision.GetComponent<Weapon_System>().DesotryWeapon();
        }
    }
}
