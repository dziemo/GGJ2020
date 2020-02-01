using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathComparator : MonoBehaviour
{
    public static PathComparator instance;
    public PathCreator A, B;
    LineRenderer lr;

    float dist = 0.0f;
    bool visualize = false;
    private void Awake()
    {
        instance = this;
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (visualize)
        {
            dist += Time.deltaTime;
            var pointA = A.path.GetPointAtTime(Mathf.Lerp(0.0f, 1.0f, dist * 0.1f));
            var pointB = B.path.GetClosestPointOnPath(pointA);
            lr.SetPosition(0, pointA);
            lr.SetPosition(1, pointB);
            //Debug.Log("DIST: " + Vector2.Distance(pointA, pointB));
        }
    }

    public void ComparePaths (PathCreator pathA, PathCreator pathB, int stepsNum)
    {

        float distanceDiff = 0.0f;
        for (int i = 1; i <= stepsNum; i++)
        {
            var pointA = pathA.path.GetPointAtTime(i / stepsNum);
            var pointB = pathB.path.GetClosestPointOnPath(pointA);
            distanceDiff += Mathf.Abs(Vector2.Distance(pointA, pointB));
        }

        Debug.Log("DISTANCE DIFF: " + distanceDiff + " AVG DIST: " + distanceDiff / stepsNum);
        A = pathA;
        B = pathB;
        lr.positionCount = 2;
        visualize = true;
    }
}
