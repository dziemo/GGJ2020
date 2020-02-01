using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobsManager : MonoBehaviour
{
    public static JobsManager instance;
    public bool currentlyAtJob;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }
}
