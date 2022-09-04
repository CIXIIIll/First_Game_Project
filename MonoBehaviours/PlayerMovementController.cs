using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float JumpSpeed = 1.0f;
    private Vector2 Direction = new Vector2();
    // Start is called before the first frame update
    void Start()
    {
        MoveMentcontrol();
    }

    // Update is called once per frame
    void Update()
    {
        MoveMentcontrol();
    }
    private void MoveMentcontrol()
    {
        Direction.x = Input.GetAxisRaw("Horizontal");
        Direction.y = Input.GetAxisRaw("Vertical");
        Direction.Normalize();
        transform.position += new Vector3(Direction.x, Direction.y, 0.0f) * MoveSpeed * Time.deltaTime;
        //Debug.Log("Movement Error" + Direction.ToString());
    }

}
