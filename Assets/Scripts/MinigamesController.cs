using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesController : MonoBehaviour
{
    public static MinigamesController instance;
    public PlayerController playerMov;
    public CameraController camMov;
    public GameObject weldMinigame, hammerMinigame;
    public GameObject minigameTint;

    private void Awake()
    {
        instance = this;
        TurnOffMinigames();
    }

    public void StartMinigame (MiniGame minigame)
    {
        BlockPlayer();
        minigameTint.SetActive(true);
        switch (minigame)
        {
            case MiniGame.Weld:
                weldMinigame.SetActive(true);
                break;
            case MiniGame.Hammer:
                hammerMinigame.SetActive(true);
                hammerMinigame.GetComponent<HammerMinigame>().StartMinigame();
                break;
            default:
                break;
        }
    }

    public void TurnOffMinigames()
    {
        minigameTint.SetActive(false);
        weldMinigame.SetActive(false);
        hammerMinigame.SetActive(false);
        ReleasePlayer();
    }

    private void BlockPlayer ()
    {
        playerMov.canMove = false;
        camMov.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ReleasePlayer ()
    {
        playerMov.canMove = true;
        camMov.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
