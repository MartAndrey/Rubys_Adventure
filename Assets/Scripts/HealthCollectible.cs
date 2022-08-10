using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int amount = 1;
     
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if(controller != null)
        {
            controller.ChangeHealth(amount);
            Destroy(gameObject, 0.1f);
        }
    }
}
