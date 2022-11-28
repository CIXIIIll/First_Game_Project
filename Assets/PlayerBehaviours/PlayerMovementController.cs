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
    public void movePos(float x, float y) { 
        vector.x = x;
        vector.y = y;
        rb2d.position += vector;
    }
    private void MovePlayer()
    {
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");
        if (!animator.GetBool("isDie"))
        {
            if ((vector.x != 0 || vector.y != 0))
            {
                if (player.Frozen)
                {
                    if (vector.x < 0 && player.faceright)
                    {
                        vector.x *= 0.5f;
                    }
                    else if (vector.x > 0 && !player.faceright)
                    {
                        vector.x *= 0.5f;
                    }
                }
                else
                {
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
                    vector.Normalize();
                }

                rb2d.position += vector * MoveSpeed * Time.deltaTime;
            }
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
