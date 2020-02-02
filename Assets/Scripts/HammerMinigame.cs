using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerMinigame : MonoBehaviour
{
    public GameObject uiParent;
    public RectTransform aimer, scale, target;
    public float pos = 0.0f, speed = 2.0f;
    public int[] targetSizes = new int[5];

    int hitCount = 0, onSpot = 0;
    bool inProgress = true;
    JobController jCtrl;
    public AudioSource aud;
    private void Awake()
    {
        uiParent.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && inProgress)
        {
            Hit();
        }
    }
    
    public void StartMinigame (JobController jc)
    {
        jCtrl = jc;
        uiParent.SetActive(true);
        for (int i = 0; i < targetSizes.Length; i++)
        {
            targetSizes[i] = ((int)scale.sizeDelta.y / 2) / (2 + i);
        }
        inProgress = true;
        hitCount = 0;
        onSpot = 0;
        speed = 1.0f;
        MoveTarget();
    }

    private void FixedUpdate()
    {
        pos += Time.fixedDeltaTime * speed;
        pos = pos % (2*Mathf.PI);
        aimer.transform.localPosition = new Vector3(0, scale.sizeDelta.y / 2 * Mathf.Sin(pos));
    }

    private void Hit()
    {
        aud.Play();
        if (aimer.localPosition.y < target.localPosition.y + target.sizeDelta.y / 2 && aimer.localPosition.y > target.localPosition.y - target.sizeDelta.y / 2)
        {
            onSpot++;
        }
        else
        {
            Debug.Log("NIETRAFIONY");
        }

        if (hitCount < targetSizes.Length)
        {
            MoveTarget();
        }
        else
        {
            jCtrl.RegisterScore((float)onSpot / (float)hitCount);
            inProgress = false;
            uiParent.SetActive(false);
            MinigamesController.instance.TurnOffMinigames();
        }
    }

    private void MoveTarget ()
    {
        speed += 0.5f;
        target.sizeDelta = new Vector2 (target.sizeDelta.x, targetSizes[hitCount]);
        target.localPosition = new Vector3(target.localPosition.x, Random.Range(target.sizeDelta.y / 2 - scale.sizeDelta.y / 2, scale.sizeDelta.y / 2 - target.sizeDelta.y / 2 ));
        hitCount++;
    }
}
