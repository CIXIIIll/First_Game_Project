using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float MoveSpeed = 3.0f;
    private Vector2 vector = new Vector2();
    private Animator animator;
    private bool faceright;
    AnimatorStateInfo animatorInfo;

    // Start is called before the first frame update
    void Start()
    {
        animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator = GetComponent<Animator>();
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerAnum();
    }
    private void MovePlayer()
    {
        vector.x = Input.GetAxisRaw("Horizontal");
        vector.y = Input.GetAxisRaw("Vertical");
        if (vector.x > 0 && !faceright)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            faceright = true;
        }
        else if (vector.x < 0 && faceright)
        {
             transform.localScale = new Vector3(1, 1, 1);
             faceright = false;
        }
        vector.Normalize();//向量的归一化，保证每个方向的移动速度一致。}
        transform.position += new Vector3(vector.x, vector.y, 0.0f) * MoveSpeed * Time.deltaTime;
    }

    private void PlayerAnum()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.Play("Attack");
        }
        else if((animatorInfo.normalizedTime > 0.99f) && (animatorInfo.IsName("Attack")))
        {
            animator.Play("Idle");
        }
    }
}
