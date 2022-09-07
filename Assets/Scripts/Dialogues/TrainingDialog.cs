using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingDialog : MonoBehaviour
{
    int indexLine;
    float delayRead = 3;
    float timeLine = 0.1f;

    Animator animator;

    [SerializeField] TMP_Text textDialog;
    [SerializeField] GameObject panelDialog;
    [SerializeField] GameObject[] imageDialog;
    [SerializeField, TextArea(2, 6)] string[] linesDialog;

    void Start()
    {
        animator = GetComponent<Animator>();
        Invoke("StartDialog", 5);
    }

    void StartDialog()
    {
        panelDialog.SetActive(true);
        animator.enabled = true;
    }

    void StartDialogLines()
    {
        indexLine = 0;
        StartCoroutine(StartDialogLinesRutiner(indexLine));
    }

    void NextDialogLines()
    {
        indexLine++;

        if (indexLine < linesDialog.Length)
        {
            StartCoroutine(DelayToReadRutiner(indexLine));
        }
        else
        {
            EndDialogLines();
        }
    }

    void EndDialogLines()
    {
        StartCoroutine(EndDialogLinesRutiner());
    }

    IEnumerator StartDialogLinesRutiner(int indexLine)
    {
        textDialog.text = string.Empty;

        foreach (char l in linesDialog[indexLine].ToCharArray())
        {
            textDialog.text += l;
            imageDialog[indexLine].SetActive(true);

            yield return new WaitForSeconds(timeLine);

            if (textDialog.text == linesDialog[indexLine])
            {
                NextDialogLines();
            }
        }
    }

    IEnumerator EndDialogLinesRutiner()
    {
        yield return new WaitForSeconds(delayRead);

        animator.SetTrigger("Transition");
    }

    IEnumerator DelayToReadRutiner(int indexLine)
    {
        yield return new WaitForSeconds(delayRead);

        imageDialog[indexLine - 1].SetActive(false);
        StartCoroutine(StartDialogLinesRutiner(indexLine));
    }
}
