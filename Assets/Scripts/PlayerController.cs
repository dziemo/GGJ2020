using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject repairIcon;
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public bool canMove = false;
    private float rotY = 0.0f; // rotation around the up/y axis

    CharacterController controller;
    Vector3 dir = new Vector3();
    Camera cam;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
    }

    void Update()
    {
        if (canMove)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            dir.z = z;
            dir.x = x;

            float mouseX = Input.GetAxis("Mouse X");
            rotY += mouseX * mouseSensitivity * Time.deltaTime;
            Quaternion localRotation = Quaternion.Euler(0, rotY, 0.0f);
            transform.rotation = localRotation;

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                var go = hit.collider.gameObject;
                if (go.CompareTag("Interactable") && !go.GetComponent<JobController>().isFullfiled)
                    repairIcon.SetActive(true);
                else if (repairIcon.activeSelf)
                    repairIcon.SetActive(false);
                if (Input.GetMouseButtonDown(0) && go.CompareTag("Interactable"))
                {
                    MinigamesController.instance.StartMinigame(go.GetComponent<Interactable>().miniGameType, go.GetComponent<JobController>());
                }
            }
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + (((rb.transform.forward * dir.z) + (rb.transform.right * dir.x)) * speed * Time.fixedDeltaTime));
       //controller.Move(((controller.transform.forward * dir.z) + (controller.transform.right * dir.x)) * speed * Time.fixedDeltaTime);
    }
}
