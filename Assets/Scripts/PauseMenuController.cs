using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    [SerializeField] Canvas pauseMenu;

    Animator animator;
    Animator transitionScene;

    [SerializeField] AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        transitionScene = GameObject.Find("Transition").GetComponentInChildren<Animator>();
        pauseMenu.enabled = false;
        animator = GetComponent<Animator>();
    }

    public void Return()
    {
        transitionScene.Play("FadeOut");
        Time.timeScale = 1;
        GameManager.Instance.GameScene();
    }

    public void PauseMenu()
    {
        if (!pauseMenu.enabled)
        {
            animator.Play("FadeUp");
            pauseMenu.enabled = true;
            Time.timeScale = 0;
            audioSource.volume = 0.2f;
            animator.enabled = true;
        }
        else
        {
            animator.SetTrigger("Transition");
            Time.timeScale = 1;
            StartCoroutine(DisablePauseMenuRoutiner());
        }
    }

    IEnumerator DisablePauseMenuRoutiner()
    {
        yield return new WaitForSeconds(1);
        pauseMenu.enabled = false;
        audioSource.volume = 0.5f;
        animator.enabled = false;
    }
}
