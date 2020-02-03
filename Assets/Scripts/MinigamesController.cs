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
    public GameObject startPanel;

    bool gameStarted = false;

    private void Awake()
    {
        startPanel.SetActive(true);
        BlockPlayer();
        instance = this;
        TurnOffMinigames();
    }

    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
                ReleasePlayer();
                startPanel.SetActive(false);
            }
        }
    }

    public void StartMinigame (MiniGame minigame, JobController controller)
    {
        BlockPlayer();
        //minigameTint.SetActive(true);
        switch (minigame)
        {
            case MiniGame.Weld:
                //PUT HELMET ON
                HelmetController.helmetController.HelmetOn(controller);
                //HELMET CONTROLLER
                break;
            case MiniGame.Hammer:
                hammerMinigame.SetActive(true);
                hammerMinigame.GetComponent<HammerMinigame>().StartMinigame(controller);
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

    public void BlockPlayer ()
    {
        playerMov.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        playerMov.gameObject.GetComponent<Rigidbody>().Sleep();
        playerMov.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        playerMov.canMove = false;
        camMov.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReleasePlayer ()
    {
        playerMov.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        playerMov.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        playerMov.canMove = true;
        camMov.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
