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
        vector.Normalize();//�����Ĺ�һ������֤ÿ��������ƶ��ٶ�һ�¡�}
        transform.position += new Vector3(vector.x, vector.y, 0.0f) * MoveSpeed * Time.deltaTime;
    }

}
