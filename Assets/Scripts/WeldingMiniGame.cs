using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingMiniGame : MonoBehaviour
{
    public PathCreator pathPrefab1, pathPrefab2, pathPrefab3;
    public DrawPath drawer;

    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    public void Start()
    {
        SpawnPath();
    }

    public void SpawnPath ()
    {
        PathCreator patCre;
        var i = Random.Range(1, 4);
        if (i == 1)
        {
            patCre = pathPrefab1;
        } else if (i == 2)
        {
            patCre = pathPrefab2;
        } else
        {
            patCre = pathPrefab3;
        }
        var path = Instantiate(patCre, transform.position, transform.rotation);
        path.transform.SetParent(transform);
        drawer.originalPath = path;
        DrawPath(path);
    }

    void DrawPath(PathCreator pathCreator)
    {
        var pointsToDraw = pathCreator.path.localPoints;
        for (int i = 0; i < pointsToDraw.Length; i++)
        {
            pointsToDraw[i] += transform.position;
        }
        lr.positionCount = pointsToDraw.Length;
        lr.SetPositions(pointsToDraw);
    }
}
