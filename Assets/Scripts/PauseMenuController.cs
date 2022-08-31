using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] Canvas pauseMenu;

    Animator animator;

    void Start()
    {
        pauseMenu.enabled = false;
        animator = GetComponent<Animator>();
    }

    public void Return()
    {
        StartCoroutine(ChangeTransitionScene.Instance.LoadSceneRutiner("GameScene"));
        PauseMenu();
        //TODO
    }

    public void PauseMenu()
    {
        if (!pauseMenu.enabled)
        {
            animator.Play("FadeUp");
            pauseMenu.enabled = true;
            Time.timeScale = 0;
            AudioListener.volume = 0.5f;
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
        AudioListener.volume = 1f;
        animator.enabled = false;
    }
}
