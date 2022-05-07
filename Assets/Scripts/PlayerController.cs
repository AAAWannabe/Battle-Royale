using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveDirection, lookDirection;
    private float moveSpeed = 10;
    private CharacterController characterController;
    private Transform cameraTransform;

    private float lookDir;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = GetComponentInChildren<Camera>().transform;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        characterController.SimpleMove(moveSpeed * transform.TransformDirection(Vector3.ClampMagnitude(new Vector3(moveDirection.x, 0f, moveDirection.y), 1f)));
        //transform.Translate(moveDirection.x * moveSpeed * Time.deltaTime, 0, moveDirection.y * moveSpeed * Time.deltaTime);
    }


    public void OnLook(InputAction.CallbackContext context)
    {
        lookDirection = context.ReadValue<Vector2>();
    }

    private void Look()
    {
        
        transform.Rotate(0, lookDirection.x, 0);
        lookDir -= lookDirection.y;
        //downwards plus, upwards minus
        cameraTransform.localRotation = Quaternion.Euler(Mathf.Clamp(lookDir, -30, 30), 0, 0);
        //transform.Rotate(Mathf.Clamp(lookDirection.y, -30, 60), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
    }
}
