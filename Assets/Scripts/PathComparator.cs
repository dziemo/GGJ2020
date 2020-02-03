using PathCreation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathComparator : MonoBehaviour
{
    public static PathComparator instance;
    public TextMeshProUGUI scoreText;
    public PathCreator A, B;
    LineRenderer lr;

    float dist = 0.0f;
    float updateDist = 0.0f;

    int samlpes = 0;
    bool visualize = false;
    bool calculated = false;

    //calc score
    float tolerance = 0.075f;
    int goodNodes = 0;
    public Color goodNode, badNode;

    private void Awake()
    {
        instance = this;
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (visualize && dist * 0.2f <= 1.0f)
        {
            dist += Time.deltaTime;
            var pointA = A.path.GetPointAtTime(Mathf.Lerp(0.0f, 1.0f, dist * 0.2f));
            var pointB = B.path.GetClosestPointOnPath(pointA);
            lr.SetPosition(0, pointA);
            lr.SetPosition(1, pointB);
            var distDiff = Mathf.Abs(Vector2.Distance(pointA, pointB));
            if (distDiff <= tolerance)
            {
                lr.startColor = goodNode;
                lr.endColor = badNode;
                goodNodes++;
            } else
            {
                lr.startColor = badNode;
                lr.endColor = badNode;
            }
            updateDist += distDiff;
            samlpes++;
        }
        else if (dist * 0.2f > 1.0f && !calculated)
        {
            Debug.Log("UPDATE DIST: " + updateDist + " SAMPLES: " + samlpes + " AVG DIST: " + updateDist/samlpes);
            Debug.Log("SCORE: " + (int)((goodNodes / (float)samlpes) * 100.0f));
            scoreText.text = (int)((goodNodes / (float)samlpes) * 100.0f) + "%";
            calculated = true;
            HelmetController.helmetController.ctrl.RegisterScore(goodNodes / (float)samlpes);
        }

        if (calculated && Input.anyKey)
        {
            SceneManager.UnloadSceneAsync(1);
        }
    }

    public void ComparePaths (PathCreator pathA, PathCreator pathB, int stepsNum)
    {
        //float distanceDiff = 0.0f;
        //for (int i = 1; i <= stepsNum; i++)
        //{
        //    var pointA = pathA.path.GetPointAtTime(i / stepsNum);
        //    var pointB = pathB.path.GetClosestPointOnPath(pointA);
        //    distanceDiff += Mathf.Abs(Vector2.Distance(pointA, pointB));
        //}

        //Debug.Log("DISTANCE DIFF: " + distanceDiff + " AVG DIST: " + distanceDiff / stepsNum);
        A = pathA;
        B = pathB;
        lr.positionCount = 2;
        visualize = true;
    }
}
