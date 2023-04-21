using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUpScript : MonoBehaviour
{
    public int healthAmount = 25;
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-healthAmount);
            Destroy(gameObject);
        }
    }
}
