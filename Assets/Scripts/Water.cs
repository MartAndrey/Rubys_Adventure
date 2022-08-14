using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float originalSpeed;
    [SerializeField] float speedReductionRatio = 0.5f;

    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        originalSpeed = player.speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (player.CompareTag("Player"))
        {
            player.speed *= speedReductionRatio;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (player.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }
    }
}
