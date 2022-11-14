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
        Vector3 x = Tragettransform.transform.position;
        x.z = -0.5f;
        Tragettransform.transform.position = x;
        switch (ElementID) { 
            case 0:
                GameObject temp = elementList.List[0].Prefab;
                Instantiate(temp, Tragettransform.position, Tragettransform.rotation);
                break;
            case 1:
                Instantiate(elementList.List[1].Prefab, Tragettransform.position, Tragettransform.rotation);
                break ;
            case 2:
                Instantiate(elementList.List[2].Prefab, Tragettransform.position, Tragettransform.rotation);
                break;
            case 3:
                Instantiate(elementList.List[3].Prefab, Tragettransform.position, Tragettransform.rotation);
                break;
            default:
                break;
        }
    }
}
