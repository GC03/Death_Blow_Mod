using UnityEngine;
using System.Collections;

public class DaggerController : MonoBehaviour 
{
    [SerializeField] private float damage;

    void OnCollisionEnter(Collision collision)
    {

        // Deal damage to collided object if it is an enemy
        if (collision.gameObject.tag =="Prisoner" || collision.gameObject.tag == "Gladiator")
        {
            // Play impact sound effect
            gameObject.GetComponent<AudioSource>().Play();
            // Debug.Log("got into the attack");
            collision.gameObject.GetComponent<EnemyAi>().EnemyTakeDamage(damage); // probably to high for right now
            // Debug.Log("This is called");
        }
    }
}