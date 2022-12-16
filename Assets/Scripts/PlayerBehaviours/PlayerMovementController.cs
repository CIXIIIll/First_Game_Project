using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Controller of Movement for Player Object 
/// </summary>
public class PlayerMovementController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    private Vector2 vector = new Vector2();
    private Animator animator;
    Player player;
    public Rigidbody2D rb2d;
    // Start is cddalled before the first frame update
    /// <summary>
    /// Initializaed Data 
    /// </summary>
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerBehavior").GetComponent<Player>();
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
    /// <summary>
    /// Move Player by user input
    /// </summary>
    private void MovePlayer()
    {
        rb2d.velocity = Vector2.zero;
        vector = new Vector2();
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");
        if (!animator.GetBool("isDie"))
        {
            ///Check User have any input
            if ((vector.x != 0 || vector.y != 0))
            {
                /// Check Player could change face direction
                if (player.Frozen)
                {
                    /// set move speed
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
                    ///switch player face direction
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
                float tempSpeed = MoveSpeed * player.PlayerOffset.speedoffset;
                rb2d.position += vector * Time.deltaTime * tempSpeed;
            }
        }
    }
    /// <summary>
    /// Play Animation
    /// </summary>
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
