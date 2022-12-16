using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class will control Enement behaviour 
/// </summary>
public class Element : MonoBehaviour
{
    /// <summary>
    /// Element Type 0 for fire, 1 for Earth, 2 for Air, 3 for Water
    /// </summary>
    public int elementType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// The Event Handler when Element get the GameOject in the Hit box
    /// </summary>
    /// <param name="collision">GameOject in the Hit box</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /// if is player
        if (collision.gameObject.CompareTag("PlayerBehavior"))
        {
            if (collision.gameObject.GetComponent<Player>() != null) {
                /// add element for the player
                collision.gameObject.GetComponent<Player>().ReceiveElement(elementType);
                Destroy(gameObject);
            }
        }
    }
}
