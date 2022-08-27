using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeTransitionScene : MonoBehaviour
{
    public static ChangeTransitionScene Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (GameManager.Instance.currentScene == Scenes.Logo) GameManager.Instance.LogoScene();
    }

    public IEnumerator LoadSceneRutiner(string nameScene)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nameScene);
    }
}
