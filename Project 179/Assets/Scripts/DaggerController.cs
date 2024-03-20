using UnityEngine;
using System.Collections;

public class DaggerController : MonoBehaviour 
{
    [SerializeField] private float damage;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag =="Prisoner" || collision.gameObject.tag == "Gladiator")
        {
            // Debug.Log("got into the attack");
            collision.gameObject.GetComponent<EnemyAi>().EnemyTakeDamage(damage); // probably to high for right now
            // Debug.Log("This is called");
            Destroy(gameObject);
        }
    }
}