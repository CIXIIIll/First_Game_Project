using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Current weapon hold by player
/// </summary>
public class CurrentWeapon : MonoBehaviour
{
    /// <summary>
    /// The Current weapon hold by player
    /// </summary>
    GameObject weaponTrans;
    /// <summary>
    /// The player information
    /// </summary>
    Player player;
    /// <summary>
    /// The temp Weapon info
    /// </summary>
    Weapon_Data weaponData;
    /// <summary>
    /// The list store all Weapon Prefab
    /// </summary>
    Weapon_Prefab_List weaponPrefabList;

    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data and a basic weapon
    /// </summary>
    void Start()
    {
        //Initializaed Data
        weaponData = Resources.Load<Weapon_Data>("Weapon/WeaponData/Sword");
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
        weaponPrefabList = Resources.Load<Weapon_Prefab_List>("Weapon/WeaponData/WeaponPrefabList");
        weaponPrefabList.List[1].GetComponent<Weapon_System>().hand = true;
        //Initializaed basic weapon
        weaponTrans = Instantiate(weaponPrefabList.List[1]);
        player.SetWeapon(weaponData);
        weaponTrans.transform.parent = transform;
        weaponTrans.transform.position = transform.position;
        weaponTrans.transform.rotation = transform.rotation;
    }
    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// Update the weapon to player hand
    /// </summary>
    /// <param name="weapon">The weapon info</param>
    private void UpdateWeapon(Weapon_Data weapon) {
        if (weaponTrans != null) {
            /// Destory current weapon
            Destroy(weaponTrans);
        }
        ///generate the weapon on on the head
        weaponTrans = Instantiate(weaponPrefabList.List[weapon.index]);
        /// stop the timer to Destory
        weaponTrans.GetComponent<Weapon_System>().hand = true;
        /// set the postions
        weaponTrans.transform.parent = transform;
        weaponTrans.transform.position = transform.position;
        weaponTrans.transform.rotation = transform.rotation;
    }
    /// <summary>
    /// The Event Handler when Player get the GameOject in the Hit box
    /// </summary>
    /// <param name="collision"> GameOject in the Hit box</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /// if is item
        if (collision.gameObject.CompareTag("Item"))
        {
            ///if is weapon
            Weapon_Data weapon = collision.GetComponent<Weapon_System>().WeaponInfo();
            if (weapon != null) {
                if (weaponTrans != null)
                {
                    /// create current weapon on the ground
                    weaponTrans.GetComponent<Weapon_System>().hand = false;
                    Instantiate(weaponTrans, transform.position, new Quaternion());
                }
                /// update weapon
                UpdateWeapon(weapon);
                player.SetWeapon(weapon);
                /// Destroy weapon
                collision.GetComponent<Weapon_System>().DesotryWeapon();
            }
        }
    }
}
