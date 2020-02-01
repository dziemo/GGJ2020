using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public bool canMove = false;
    private float rotY = 0.0f; // rotation around the up/y axis

    CharacterController controller;
    Vector3 dir = new Vector3();
    Camera cam;

    private void Awake()
    {
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
                if (Input.GetMouseButtonDown(0) && go.CompareTag("Interactable"))
                {
                    MinigamesController.instance.StartMinigame(go.GetComponent<Interactable>().miniGameType);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(((controller.transform.forward * dir.z) + (controller.transform.right * dir.x)) * speed * Time.fixedDeltaTime);
    }
}
