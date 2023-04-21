using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float dropChance = 0.3f;
    private GameObject enemy;
    public GameObject HealthPickUp;
    private GameObject GameManager;

    void Start()
    {
        currentHealth = maxHealth;
        enemy = this.gameObject;
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        
    }

    public void TakeDamage(int mod)
    {
        Debug.Log("Enemy Hit" + currentHealth);
        currentHealth += mod;
        CinemachineShake.Instance.ShakeCamera(3f, .1f);

        if (currentHealth > maxHealth){

            currentHealth = maxHealth;

        } else if(currentHealth <= 0){

            currentHealth = 0;
            Die();
        }
    }

    private void Die()
    {
        GameManager.GetComponent<GameManager>().AddScore(1);
        if(Random.value < dropChance){
            Instantiate(HealthPickUp, transform.position, Quaternion.identity);
        }
        Destroy(enemy);
    }
}
