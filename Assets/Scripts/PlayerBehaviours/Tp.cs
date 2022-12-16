using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The transport Behaviour
/// </summary>
public class Tp : MonoBehaviour
{
    /// <summary>
    /// hit box
    /// </summary>
    BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Enable the Transport points Hit box
    /// </summary>
    public void EnableTp() {
        box.enabled = true;
    }
    /// <summary>
    /// Transport player to boss room
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(100, 100, -1);
        }
    }
}
