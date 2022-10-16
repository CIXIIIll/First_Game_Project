using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    public float HP;
    public float MP;
    virtual public void CharacterDie() {
        if (gameObject.gameObject.CompareTag("Player"))
        {
            animator = GameObject.FindWithTag("Player").GetComponent<Animator>();
            animator.applyRootMotion = false;
            animator.SetBool("isDie", true);
        }
        else {
            Destroy(gameObject);
        }
    }
    public void CharacterDamage(float damage) 
    {
        HP -= damage;
        if (HP <= float.Epsilon)
        {
            CharacterDie();
        }         
    }
    public IEnumerator DamageOverTime(float damage, float duration) {
        float timer = 0;
        while(HP>=0 && timer <= duration){ 
            HP-=damage*Time.deltaTime;
            timer+=timer*Time.deltaTime;
            yield return null;
        }
    }

}
