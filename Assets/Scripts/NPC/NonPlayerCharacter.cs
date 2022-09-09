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

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            exclamation.enabled = false;
        }
    }
}
