using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public MiniGame miniGameType;
    private List<Color> startColors = new List<Color>();
    Renderer rend;
    JobController jc;

    private void Awake()
    {
        jc = GetComponent<JobController>();
        rend = GetComponent<Renderer>();
        foreach(var m in rend.materials)
        {
            startColors.Add(m.color);
        }
    }

    void OnMouseEnter()
    {
        if (jc.isFullfiled)
        {
        }
        else
        {
            foreach (var m in rend.materials)
            {
                m.color = Color.yellow;
            }
        }
    }
    void OnMouseExit()
    {
        for (int i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].color = startColors[i];
        }
    }
}

public enum MiniGame { Weld, Hammer }