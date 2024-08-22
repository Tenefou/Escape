using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput input;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction crouchAction;
    Rigidbody rb;

    [SerializeField] private Transform cameraTransform; // Référence à la caméra
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpHeigh = 1.0f;
    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform bottomCharacter;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private CapsuleCollider collider;


    private void Start()
    {
        input = GetComponent<PlayerInput>();
        moveAction = input.actions.FindAction("Move");
        jumpAction = input.actions.FindAction("Jump");
        crouchAction = input.actions.FindAction("Crouch");
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        rb = GetComponent<Rigidbody>();
    }

    void MovePlayer()
    {
        Vector3 desiredMoveDirection = GetDirection();

        // Déplacer le joueur
        transform.position += desiredMoveDirection * speed * Time.deltaTime;
    }

    private Vector3 GetDirection()
    {
        Vector2 inputDirection = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputDirection.x, 0, inputDirection.y);

        // Récupérer les axes relatifs à la caméra
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

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

    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeigh, ForceMode.Impulse);
    }

    private void Update()
    {
        MovePlayer();

        isGrounded = Physics.CheckSphere(bottomCharacter.position, groundCheckRadius, groundLayer);
        if (jumpAction.triggered && isGrounded)
        {
            Jump();
        }

        Crouch();
    }

    private void Crouch()
    {
        if (crouchAction.inProgress)
        {
            collider.height = 1.75f;
            collider.center = new Vector3(collider.center.x, -0.125f);
        }
        else
        {
            collider.height = 2f;
            collider.center = new Vector3(collider.center.x, 0);
        }
    }
}
