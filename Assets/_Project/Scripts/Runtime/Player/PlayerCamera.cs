using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;


public struct CameraInput
{
    public Vector2 Look;
}

public class PlayerCamera : MonoBehaviour
{

    private Vector3 _eulerAngles;
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private Transform pointCamera;

    private PlayerInputActions _inputActions;


    private void Update()
    {
        UpdatePosition();
        UpdateRotation();
    }

    private void Start()
    {
        Initialize(pointCamera);
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
    }

    public void Initialize(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;

        transform.eulerAngles = _eulerAngles = target.eulerAngles;
    }

    public void UpdateRotation()
    {
        var input = _inputActions.Gameplay;
        var cameraInput = new CameraInput { Look = input.Look.ReadValue<Vector2>() };
        _eulerAngles += new Vector3(-cameraInput.Look.y, cameraInput.Look.x) * sensitivity;
        _eulerAngles.x = Mathf.Clamp(_eulerAngles.x, -80f, 80f);
        transform.eulerAngles = _eulerAngles;
    }

    public Vector3 GetDirectionMove(InputAction moveAction)
    {
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

        // Récupérer les axes relatifs à la caméra
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        // Ignorer la composante verticale
        forward.y = 0;
        right.y = 0;

        // Normaliser pour s'assurer que les directions sont unitaires
        forward.Normalize();
        right.Normalize();

        // Calculer la direction finale
        Vector3 desiredMoveDirection = (forward * moveDirection.z + right * moveDirection.x).normalized;
        return desiredMoveDirection;
    }

    private void UpdatePosition()
    {
        Vector3 camPosition = pointCamera.position;
        camPosition.y = pointCamera.position.y;
        transform.position = camPosition;
    }
}