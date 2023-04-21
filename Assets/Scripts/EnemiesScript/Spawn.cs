using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnCollisonEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            anim.SetBool("Hit", true);
        }
    }
}
