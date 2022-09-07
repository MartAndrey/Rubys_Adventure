using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrainingDialog : MonoBehaviour
{
    int indexLine;
    private float timeLine = 0.1f;

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

    void Update()
    {
        if (textDialog.text == linesDialog[indexLine])
        {
            NextDialogLines();
        }
    }

    void StartDialog()
    {
        panelDialog.SetActive(true);
        animator.enabled = true;
        Invoke("StartDialogLines", 1);
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
            imageDialog[indexLine - 1].SetActive(false);
            StartCoroutine(StartDialogLinesRutiner(indexLine));
        }
        else
        {
            
        }
    }

    IEnumerator StartDialogLinesRutiner(int indexLine)
    {
        textDialog.text = string.Empty;

        foreach (char l in linesDialog[indexLine].ToCharArray())
        {
            textDialog.text += l;
            imageDialog[indexLine].SetActive(true);

            yield return new WaitForSeconds(timeLine);
        }
    }
}
