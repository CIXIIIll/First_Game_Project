using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Item System
/// </summary>
public class ItemController : MonoBehaviour
{
    /// <summary>
    /// Element Data
    /// </summary>
    private Element_List elementList;
    /// <summary>
    /// Weapon Prefab Data
    /// </summary>
    private Weapon_Prefab_List weapon_PrefabList;
    // Start is called before the first frame update
    /// <summary>
    /// Initializaed Data
    /// </summary>
    void Start()
    {
        elementList = Resources.Load<Element_List>("Weapon/ElementData/ElementList");
        weapon_PrefabList = Resources.Load<Weapon_Prefab_List>("Weapon/WeaponData/WeaponPrefabList");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Generate Element
    /// </summary>
    /// <param name="Tragettransform">the position to generate</param>
    /// <param name="ElementID">the element id want to generate</param>
    private void ElementGenerate(Transform Tragettransform,int ElementID) {
        Vector3 x = Tragettransform.transform.position;
        x.z = -1f;
        Tragettransform.transform.position = x;
        if (ElementID < 25)
        {
            Instantiate(elementList.List[0].Prefab, Tragettransform.position, Tragettransform.rotation);
        }
        else if (ElementID < 50)
        {
            Instantiate(elementList.List[1].Prefab, Tragettransform.position, Tragettransform.rotation);
        }
        else if (ElementID < 75)
        {
            Instantiate(elementList.List[2].Prefab, Tragettransform.position, Tragettransform.rotation);
        }
        else {
            Instantiate(elementList.List[3].Prefab, Tragettransform.position, Tragettransform.rotation);
        }      
    }
    /// <summary>
    /// calculate the percent rate and generate item
    /// </summary>
    /// <param name="Tragettransform">the position to generate</param>
    public void GenerateItme(Transform Tragettransform) {
        int perCent = Random.Range(0, 100);

        if (perCent < 70)
        {
            int i = Random.Range(1, 5);
            for (int j = 0; j < i; j++) {
                int x = Random.Range(0, 100);
                ElementGenerate(Tragettransform, x);
            } 
        }
        else if (perCent < 95)
        {
            WeaponGenerate(Tragettransform);
        }
    }
    /// <summary>
    /// Generate Weapon
    /// </summary>
    /// <param name="Tragettransform">the position to generate</param>
    public void WeaponGenerate(Transform Tragettransform) {
        int range = weapon_PrefabList.List.Count;
        int x = Random.Range(0, range);
        Instantiate(weapon_PrefabList.List[x], Tragettransform.position, Tragettransform.rotation);
    }
}
