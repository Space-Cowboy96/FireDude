using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public float invincibilityTime = 2f; // The duration of the invincibility after taking damage
    private bool isInvincible = false; // Whether the player is currently invincible or not
    private bool isAPickup = false;
    public float nextPossibleHit = 0f;
    public float pushBackForce = 200f;
    public float pushbackRadius = 2f;

    public PlayerHealthBar healthBar;
    public GameObject player;
    public GameObject gm;

    public void Start()
    {
        currentHealth = maxHealth;
        gm = GameObject.FindGameObjectWithTag("GameManager");
    }


    public void UpdateHealth(int mod)
    {
        if (!isInvincible || isAPickup) // Only take damage if not currently invincible
        {
            currentHealth += mod;
            healthBar.SetHealth(currentHealth);
            Debug.Log("Player Hit" + currentHealth);

            if (currentHealth > maxHealth){
                currentHealth = maxHealth;
            } else if(currentHealth <= 0){
                currentHealth = 0;
                gm.GetComponent<GameManager>().GameOver();
            }
            if(mod <0){
                StartCoroutine(BecomeInvincible()); // Start the coroutine to make the player invincible
            }
        }
    }

    IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "HealthPickUp"){
            isAPickup = true;
            UpdateHealth(10);
            Destroy(other.gameObject);
            isAPickup = false;
        }
        if (nextPossibleHit > Time.time)
        {
            return; // Not enough time has passed since the last shot
        }
        if (other.gameObject.tag == "Enemy" && !isInvincible){
            
            UpdateHealth(-25); // reduce health by 10 when colliding with an enemy
            CinemachineShake.Instance.ShakeCamera(3f, .1f);
            // Push back enemies in the area
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, pushbackRadius); // define pushbackRadius as 2
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject.tag == "Enemy")
                {
                    Vector2 pushDirection = (collider.transform.position - player.transform.position).normalized;
                    Debug.Log("Push direction: " + pushDirection);
                    collider.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushBackForce); // define pushbackForce as 200
                }
            }
        }

        nextPossibleHit = Time.time + invincibilityTime;
    }

}