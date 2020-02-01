using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public PathCreator originalPath;
    Camera cam;
    List<Vector2> vertexesList = new List<Vector2>();
    LineRenderer lr;

    bool drawing = false;

    private void Awake()
    {
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {

        if (drawing)
        {
            Draw(cam.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log("Drawing!");
        }

        if (Input.GetMouseButtonDown(1))
        {
            drawing = false;
            RenderPath();
        }

        if (!drawing && Input.GetMouseButtonDown(0))
        {
            vertexesList.Add(cam.ScreenToWorldPoint(Input.mousePosition));
            drawing = true;
        }
    }

    void Draw(Vector3 mousePos)
    {
        if (Vector2.Distance(mousePos, vertexesList[vertexesList.Count - 1]) > 0.2f)
        {
            vertexesList.Add(mousePos);
            Debug.Log("Added!");
        }
        else
        {
            Debug.Log("Not added!");
        }
    }

    void RenderPath ()
    {
        if (vertexesList.Count > 1)
        {
            var pc = gameObject.AddComponent<PathCreator>();
            var bp = new BezierPath(vertexesList, true, PathSpace.xy);
            pc.bezierPath = bp;
            PathComparator.instance.ComparePaths(originalPath, pc, 100);
            var pointsToDraw = pc.path.localPoints;
            lr.positionCount = pointsToDraw.Length;
            lr.SetPositions(pointsToDraw);
        }
    }
}
