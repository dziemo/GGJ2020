using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public MiniGame miniGameType;

    Outline outline;
    private Color startcolor;
    Renderer rend;
    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }
    void OnMouseEnter()
    {
        startcolor = rend.material.color;
        rend.material.color = Color.yellow;
    }
    void OnMouseExit()
    {
        rend.material.color = startcolor;
    }
}

public enum MiniGame { Weld, Hammer }