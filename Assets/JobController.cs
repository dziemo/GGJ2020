using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobController : MonoBehaviour
{
    public void TakeJob ()
    {
        if (!JobsManager.instance.currentlyAtJob)
        {
            JobsManager.instance.currentlyAtJob = true;
        }
    }
}
