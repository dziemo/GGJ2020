using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawPath : MonoBehaviour
{
    public Image energyImage;
    public PathCreator originalPath;
    public GameObject particlesParent;
    public Camera cam;
    List<Vector2> vertexesList = new List<Vector2>();
    LineRenderer lr;
    AudioSource aud;
    bool drawing = false;
    bool canDraw = true;

    float maxEnergy = 0.0f;
    float energyLeft = 0.0f;
    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        lr = GetComponent<LineRenderer>();
        particlesParent.SetActive(false);
    }

    void Update()
    {

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (drawing && energyLeft > 0.0f)
        {
            Draw(mousePos);
            particlesParent.transform.position = mousePos;
            Debug.Log("Mouse pos: " + mousePos.ToString());
        } else if (drawing && energyLeft <= 0.0f)
        {
            aud.Stop();
            canDraw = false;
            particlesParent.SetActive(false);
            drawing = false;
            RenderPath();
        }

        if (Input.GetMouseButtonUp(0))
        {
            aud.Stop();
            canDraw = false;
            particlesParent.SetActive(false);
            drawing = false;
            RenderPath();
        }

        if (!drawing && Input.GetMouseButtonDown(0) && canDraw)
        {
            aud.Play();
            Draw(mousePos);
            particlesParent.transform.position = mousePos;
            particlesParent.SetActive(true);
            drawing = true;
            maxEnergy = originalPath.path.length;
            energyLeft = maxEnergy;
            Debug.Log("MAX ENERGY: " + maxEnergy + " ENERGY LEFT: " + energyLeft);
        }
    }

    void Draw(Vector2 mousePos)
    {
        if (vertexesList.Count == 0)
        {
            vertexesList.Add(mousePos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, mousePos);
            Debug.Log("Added!");
        }
        else if (Vector2.Distance(mousePos, vertexesList[vertexesList.Count - 1]) > 0.2f)
        {
            energyLeft -= Vector2.Distance(mousePos, vertexesList[vertexesList.Count - 1]);
            vertexesList.Add(mousePos);
            lr.positionCount++;
            lr.SetPosition(lr.positionCount - 1, mousePos);
            Debug.Log("Added!");
            energyImage.fillAmount = energyLeft / maxEnergy;
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
