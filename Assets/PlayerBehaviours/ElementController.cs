using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementController : MonoBehaviour
{
    private Element_List elementList;
    // Start is called before the first frame update
    void Start()
    {
        elementList = Resources.Load<Element_List>("Weapon/ElementData/ElementList");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ElementGenerate(Transform Tragettransform,int ElementID) {
        switch (ElementID) {
            case 1:
                GameObject temp = elementList.List[0].Prefab;
                Instantiate(temp, Tragettransform.position, Tragettransform.rotation);
                break;
            case 2:
                Instantiate(elementList.List[1].Prefab, Tragettransform.position, Tragettransform.rotation);

                break ;
            default:
                break;
        }
    }
}
