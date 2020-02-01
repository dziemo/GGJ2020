using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPath : MonoBehaviour
{
    public PathCreator originalPath;
    public GameObject particlesParent;
    Camera cam;
    List<Vector2> vertexesList = new List<Vector2>();
    LineRenderer lr;

    bool drawing = false;

    private void Awake()
    {
        cam = Camera.main;
        lr = GetComponent<LineRenderer>();
        particlesParent.SetActive(false);
    }

    void Update()
    {

        if (drawing)
        {
            Draw(cam.ScreenToWorldPoint(Input.mousePosition));
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            particlesParent.transform.position = mousePos;
            Debug.Log("Drawing!");
        }

        if (Input.GetMouseButtonUp(0))
        {
            particlesParent.SetActive(false);
            drawing = false;
            RenderPath();
        }

        if (!drawing && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            particlesParent.transform.position = mousePos;
            particlesParent.SetActive(true);
            vertexesList.Add(mousePos);
            drawing = true;
        }
    }

    void Draw(Vector2 mousePos)
    {
        if (Vector2.Distance(mousePos, vertexesList[vertexesList.Count - 1]) > 0.2f)
        {
            vertexesList.Add(mousePos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, mousePos);
            Debug.Log("Added!");
        }
        else
        {
            Debug.Log("Not added!");
        }
    }

    private void LateUpdate()
    {
        if (drawing)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            particlesParent.transform.position = mousePos;
        }
    }

    void RenderPath ()
    {
        if (vertexesList.Count > 1)
        {
            var pc = gameObject.AddComponent<PathCreator>();
            var bp = new BezierPath(vertexesList, false, PathSpace.xy);
            pc.bezierPath = bp;
            PathComparator.instance.ComparePaths(originalPath, pc, 500);
        }
    }
}
