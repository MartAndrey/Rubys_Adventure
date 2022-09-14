using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum TypeCollectible { HealthCollectible, AmmoCollectible }
    public TypeCollectible typeCollectible;

    PlayerController player;

    [SerializeField] int amountHealth = 1;
    [SerializeField] int amountBullet = 5;

    AudioSource audioSource;

    [SerializeField] ParticleSystem pickUp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
    }

    public void HealthCollectible()
    {
        Instantiate(pickUp, transform.position, Quaternion.identity);
        audioSource.Play();
        player.ChangeHealth(amountHealth);
    }

    public void AmmoCollectible()
    {
        Instantiate(pickUp, transform.position, Quaternion.identity);
        audioSource.Play();
        player.HasAmmo = true;
        player.AmmoBullet += amountBullet;
        player.UpdateUIAmmoBullet();
    }
}
