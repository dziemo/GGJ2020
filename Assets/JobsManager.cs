using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobsManager : MonoBehaviour
{
    public static JobsManager instance;

    public List<JobController> tasks = new List<JobController>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        
    }

    public void TaskEnded ()
    {
        foreach(var task in tasks)
        {
            if (!task.isFullfiled)
                return;
        }

        Debug.Log("END GAME!!!!!!!!!!!!!!!!!");
    }
}
