using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private Element_List elementList;
    private Weapon_List weapon_List;
    // Start is called before the first frame update
    void Start()
    {
        elementList = Resources.Load<Element_List>("Weapon/ElementData/ElementList");
        weapon_List = Resources.Load<Weapon_List>("Weapon/WeaponData/Weapon_List");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
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

    public void WeaponGenerate(Transform Tragettransform) {
        int range = weapon_List.List.Count;
        int x = Random.Range(0, range);
        Instantiate(weapon_List.List[x].Weapon_prefab, Tragettransform.position, Tragettransform.rotation);
    }
}
