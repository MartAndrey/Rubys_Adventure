using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Canvas gameMenu, gameOverMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
