using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeldingMiniGame : MonoBehaviour
{
    public PathCreator pathPrefab;
    public DrawPath drawer;

    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    [ContextMenu("Make path")]
    public void SpawnPath ()
    {
        var path = Instantiate(pathPrefab, transform.position, transform.rotation);
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
