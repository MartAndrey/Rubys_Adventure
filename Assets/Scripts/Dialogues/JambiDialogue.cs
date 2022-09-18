using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JambiDialogue : MonoBehaviour
{
    int indexLine;
    int delayRead = 1;
    float timeLine = 0.1f;

    Animator animator;
    PlayerController player;

    [SerializeField] int jambiNumber;
    [SerializeField] NonPlayerCharacter jambi;
    [SerializeField] TMP_Text textDialogue;
    [SerializeField] GameObject imageInfiniteBullet;
    [SerializeField] GameObject textBulletAmmo;
    [SerializeField] GameObject imageYes;
    [SerializeField] GameObject imageNot;
    [SerializeField] GameObject collectableAmmoUI;
    [SerializeField, TextArea(2, 6)] string[] linesDialogue;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        animator = GetComponent<Animator>();
    }

    void StartDialogue()
    {
        indexLine = 0;

        if (jambiNumber == 2)
        {
            if (Robot.EnemiesDefeated >= 13)
            {
                StartCoroutine(StartDialogueRutiner(indexLine));
            }
            else
            {
                textDialogue.text = "First you have to defeat all the enemies.";
                StartCoroutine(EndDialogueRutiner());
            }
        }
        else
        {
            StartCoroutine(StartDialogueRutiner(indexLine));
        }
    }

    public void NextDialog()
    {
        indexLine++;

        if (indexLine < linesDialogue.Length)
        {
            StartCoroutine(NextDialogueRutiner(indexLine));
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        StartCoroutine(EndDialogueRutiner());
    }

    IEnumerator StartDialogueRutiner(int indexLine)
    {
        textDialogue.text = string.Empty;

        foreach (char l in linesDialogue[indexLine].ToCharArray())
        {
            textDialogue.text += l;
            yield return new WaitForSeconds(timeLine);

            if (textDialogue.text == linesDialogue[indexLine])
            {
                if (textDialogue.text == linesDialogue[1] && jambiNumber == 0)
                {
                    imageYes.SetActive(true);
                    imageNot.SetActive(true);
                }
                else if (textDialogue.text == linesDialogue[2] && jambiNumber == 1)
                {
                    StartCoroutine(CollectableAmmoUIRutiner());
                }
                else
                {
                    imageYes.SetActive(false);
                    imageNot.SetActive(false);

                    NextDialog();
                }
            }
        }
    }

    IEnumerator NextDialogueRutiner(int indexLine)
    {
        yield return new WaitForSeconds(delayRead);

        StartCoroutine(StartDialogueRutiner(indexLine));
    }

    IEnumerator EndDialogueRutiner()
    {
        yield return new WaitForSeconds(delayRead);

        textDialogue.text = string.Empty;

        imageYes.SetActive(false);
        imageNot.SetActive(false);

        animator.SetTrigger("Transition");

        yield return new WaitForSeconds(delayRead);

        jambi.HideDialog();

        if (jambiNumber == 2 && Robot.EnemiesDefeated >= 13)
        {
            InfiniteBullet();
        }
    }

    IEnumerator CollectableAmmoUIRutiner()
    {
        collectableAmmoUI.SetActive(true);

        yield return new WaitForSeconds(1);

        player.AmmoBullet += 5;
        player.HasAmmo = true;
        player.UpdateUIAmmoBullet();
        EndDialogue();
    }

    void InfiniteBullet()
    {
        textBulletAmmo.SetActive(false);
        imageInfiniteBullet.SetActive(true);

        player.AmmoBullet = 999999999;
        player.HasAmmo = true;

        GameObject[] robots = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var i in robots)
        {
            i.GetComponent<Robot>().Unfixed();
        }
    }
}
