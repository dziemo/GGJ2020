using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesController : MonoBehaviour
{
    public GameObject weldMinigame, hammerMinigame;

    private void Awake()
    {
        TurnOffMinigames();
    }

    public void StartMinigame (MiniGame minigame)
    {
        switch (minigame)
        {
            case MiniGame.Weld:
                break;
            case MiniGame.Hammer:
                break;
            default:
                break;
        }
    }

    public void TurnOffMinigames()
    {
        weldMinigame.SetActive(false);
        hammerMinigame.SetActive(false);
    }
}
