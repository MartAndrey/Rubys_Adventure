using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTransitionScene : MonoBehaviour
{
    public static ChangeTransitionScene Instance;

    Animator animator;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        if (GameManager.Instance.currentScene == Scenes.Logo)
        {
            GameManager.Instance.LogoScene();
        }
    }

    public IEnumerator LoadSceneRutiner(string nameScene, float time = 1)
    {
        yield return new WaitForSeconds(time);
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(nameScene);
    }
}
