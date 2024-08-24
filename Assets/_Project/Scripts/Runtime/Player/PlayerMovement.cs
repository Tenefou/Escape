using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 playerMesh;
    Vector3 camPosition;

    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpHeigh = 1.0f;
    [SerializeField] private float characterHeight = 2f;
    [SerializeField] private CapsuleCollider characterCollider;
    [SerializeField] private Transform cameraPoint;
    [SerializeField] private SpaceManager spaceManager;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void MovePlayer()
    {
        Vector3 desiredMoveDirection = playerCamera.GetDirectionMove(InputManager.Instance.MoveAction);

        // Déplacer le joueur
        transform.position += desiredMoveDirection * speed * Time.deltaTime;
    }

    public void Jump()
    {
        if (InputManager.Instance.JumpAction.triggered && spaceManager.isGrounded)
            rb.AddForce(Vector3.up * jumpHeigh, ForceMode.Impulse);
    }

    private void Update()
    {
        MovePlayer();
        Jump();
        Crouch();
        Sprint();
    }

    private void Crouch()
    {
        playerMesh = playerPrefab.transform.localScale;
        camPosition = cameraPoint.position;

        if (InputManager.Instance.CrouchAction.inProgress) 
        
        {
            characterCollider.height = 1f;
            characterCollider.center = new Vector3(characterCollider.center.x, -0.5f);

            playerMesh.y = 0.5f;

            camPosition.y = 1f;
            cameraPoint.position = camPosition;


        } else
        {
            characterCollider.height = spaceManager.haveRoof ? 1f : characterHeight;
            characterCollider.center = spaceManager.haveRoof ?
                new Vector3(characterCollider.center.x, -0.5f) :
                new Vector3(characterCollider.center.x, 0f);
            

            camPosition.y = spaceManager.haveRoof? 1f : characterHeight;
            cameraPoint.position = camPosition;
        }
       

    }

    private void Sprint()
    {
            speed = InputManager.Instance.SprintAction.inProgress? 8f: 5f;
    }
}