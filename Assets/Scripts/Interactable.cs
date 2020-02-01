using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public MiniGame miniGameType;

    Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;
    }

    public void OnMouseOver()
    {
        outline.OutlineWidth = 10.0f;
    }

    private void OnMouseExit()
    {
        outline.OutlineWidth = 0.0f;
    }
}

public enum MiniGame { Weld, Hammer }