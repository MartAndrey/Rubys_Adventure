using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] int amount = 1;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null && controller.Health < 5)
        {
            audioSource.Play();
            controller.ChangeHealth(amount);
            Destroy(gameObject, 0.3f);
        }
    }
}
