using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NonPlayerCharacter : MonoBehaviour
{
    [SerializeField] Image exclamation;
    [SerializeField] GameObject dialogue;

    public void DisplayDialog()
    {
        exclamation.enabled = false;
        dialogue.SetActive(true);
    }

    public void HideDialog()
    {
        exclamation.enabled = true;
        dialogue.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exclamation.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null)
            {
                player.GunLoaded = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exclamation.enabled = false;

            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();

                if (player != null)
                {
                    player.GunLoaded = true;
                }
            }
        }
    }
}
