using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public void Play()
    {
        GameManager.Instance.GameScene();
    }

    public void MainMenu()
    {
        GameManager.Instance.MenuScene();
    }
}
