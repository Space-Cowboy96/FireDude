using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BulletController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float destroyDelay = 0.5f;
    private GameObject bullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bullet = this.gameObject;
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Walls"))
            {
                animator.SetBool("Hit", true);
                StartCoroutine(DestroyAfterDelay(destroyDelay));
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        if (other.gameObject.tag.Equals("Enemy"))
        {
                other.gameObject.GetComponent<EnemyHealth>().TakeDamage(-10);
                animator.SetBool("Hit", true);
                StartCoroutine(DestroyAfterDelay(destroyDelay));
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
