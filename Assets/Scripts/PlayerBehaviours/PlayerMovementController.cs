using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    public float JumpSpeed = 1.0f;

    private Vector2 vector = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        MovePlayer();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }
    private void MovePlayer()
    {
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");
        vector.Normalize();//向量的归一化，保证每个方向的移动速度一致。}
        transform.position += new Vector3(vector.x, vector.y, 0.0f) * MoveSpeed * Time.deltaTime;
    }

}
