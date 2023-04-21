using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimNShoot : MonoBehaviour
{
    //Bullet attributes;
    public float regular_fireForce = 20f;
    private float nextFireTime = 0f;
    public float fireRate = 1f; // The higher this number is, the more time ther is in between shots 

    //Bullet spawn point attributes
    private float radius = 1f;
    private float rotationSpeed = 1f;

    //Camera/Mouse attributes
    public Camera sceneCamera;
    private Vector2 mousePosition;
    

    public Transform bulletSpawnPoint;
    private SpriteRenderer spriteRenderer;

    private GameObject firePoint;
    public GameObject regularBulletPrefab;


    void Start()
    {
        firePoint = GameObject.Find("FirePoint");
    }

    void Update()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the angle between the player and the mouse
        Vector2 direction = mousePosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calculate the offset of the bullet spawn point
        float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        // Update the bullet spawn point position
        bulletSpawnPoint.position = transform.position + new Vector3(x, y, 0f);

        // Rotate the bullet spawn point around the player
        bulletSpawnPoint.RotateAround(transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        // Regular Bullet
        if(!PauseScreen.GameIsPaused){
           if (Input.GetMouseButton(0))
            {
                Shoot_Regular();
            }
        }

    }

    
    public void Shoot_Regular()
    {
        if (Time.time < nextFireTime)
        {
            return; // Not enough time has passed since the last shot
        }

        // Create a new Regular bullet
        GameObject regularBullet = Instantiate(regularBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = regularBullet.GetComponent<Rigidbody2D>();

        // Calculate the angle between the player and the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, mousePosition);
        float force = Mathf.Max(distance, 1f) * regular_fireForce;
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        // Set the next fire time
        nextFireTime = Time.time + fireRate;
    }
    
}
