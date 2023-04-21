using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public float speed = 3f;
    public int collisionDamage = 10;
    private Transform target;
    private Animator animator;
    public float pushbackDistance = 5f;

    private bool isPushingBack = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (!isPushingBack && target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            animator.SetBool("isMoving", true);
            animator.SetFloat("moveX", target.position.x - transform.position.x);
            animator.SetFloat("moveY", target.position.y - transform.position.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPushingBack)
        {
            isPushingBack = true;
            StartCoroutine(StopPushback());
        }
    }

    private IEnumerator StopPushback()
    {
        yield return new WaitForSeconds(pushbackDistance / speed);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        isPushingBack = false;
    }
}

