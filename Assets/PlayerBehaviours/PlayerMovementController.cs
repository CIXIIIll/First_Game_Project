using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    private Vector2 vector = new Vector2();
    private Animator animator;
    Player player;
    public Rigidbody2D rb2d;
    // Start is cddalled before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetBool("isDie"))
        {
            PlayerAnum();
            MovePlayer();
        }
        else
        {
            animator.Play("Idle");
        }
    }
    private void MovePlayer()
    {
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");

        if (vector.x > 0 && !player.faceright)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            player.faceright = true;
        }
        else if (vector.x < 0 && player.faceright)
        {
            transform.localScale = new Vector3(1, 1, 1);
            player.faceright = false;
        }
        vector.Normalize();//向量的归一化，保证每个方向的移动速度一致。}
        if (!animator.GetBool("isDie")) {
            rb2d.position += vector * MoveSpeed * Time.deltaTime* player.offset.speedoffset;
        }
        //transform.position += new Vector3(vector.x, vector.y, 0.0f) * MoveSpeed * Time.deltaTime;
    }
 
    private void PlayerAnum()
    {
        if (Mathf.Approximately(vector.x, 0.0f) && Mathf.Approximately(vector.y, 0.0f))
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
    }
}
