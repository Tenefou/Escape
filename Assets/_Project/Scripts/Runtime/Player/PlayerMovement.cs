using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Values")]
    Vector3 _playerColliderScale;
    Vector3 _playerMeshPosition;
    Vector3 _playerMeshScale;
    Vector3 _camPosition;
    bool _readyToJump;
    float _moveSpeed;
    float _playerHeight;


    [Header("References")]
    [SerializeField] private CapsuleCollider playerCollider;
    [SerializeField] private SpaceManager spaceManager;
    [SerializeField] private PlayerCamera playerCamera;
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        _playerMeshScale = playerPrefab.transform.localScale;
        _playerMeshPosition = playerPrefab.transform.localPosition;
        _playerColliderScale = playerCollider.transform.localScale;
        _playerHeight = playerPrefab.transform.localScale.y;
        _readyToJump = true;
    }
    private void Update()
    {
        MovePlayer();
        Jump();
        Crouch();
        Sprint();
    }

    void MovePlayer()
    {
        Vector3 desiredMoveDirection = playerCamera.GetDirectionMove(InputManager.Instance.MoveAction);
        Vector3 moveDirection;

        if (spaceManager.isOnSlope && spaceManager.isGrounded)
        {
            // Obtenir la direction de mouvement projetée sur la pente
            moveDirection = GetSlopeMoveDirection(desiredMoveDirection, spaceManager.slopeHit);

            // Appliquer un ajustement à la gravité pour s'assurer que le joueur reste collé à la pente
            // Permet de monter ou descendre la pente au lieu de glisser
            rb.AddForce(-spaceManager.slopeHit.normal * 9.81f, ForceMode.Force);  // Augmente la gravité pour s'assurer qu'il colle à la pente

        }
        else
        {
            // Déplacer le joueur
            moveDirection = desiredMoveDirection;

            Debug.Log("Moving using the ground manager");

        }
        // Déplacer le joueur
        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }

    public void ResetJump()
    {
        _readyToJump = true;    
    }

    public void JumpAction()
    {
        _readyToJump = false;
        if  (spaceManager.isOnSlope)
            rb.AddForce(spaceManager.slopeHit.normal * 9.81f, ForceMode.Force);
        rb.AddForce(Vector3.up * PlayerManager.Instance.JumpHeight, ForceMode.Impulse);
    }

    public void Jump()
    {
        if (InputManager.Instance.JumpAction.triggered && spaceManager.isGrounded && _readyToJump)
            JumpAction();
        else if(spaceManager.isGrounded && !_readyToJump)
            Invoke(nameof(ResetJump), PlayerManager.Instance.JumpCooldown);
    }


    private void Crouch()
    {

        if (InputManager.Instance.CrouchAction.inProgress) 
        
        {
            transform.localScale = new Vector3(transform.localScale.x, PlayerManager.Instance.CrounchScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 3, ForceMode.Impulse);
        } else
        {
            transform.localScale = spaceManager.haveRoof ? 
                new Vector3(transform.localScale.x, PlayerManager.Instance.CrounchScale, transform.localScale.z) : 
                new Vector3(transform.localScale.x, _playerHeight, transform.localScale.z);
        }


    }

    private void Sprint()
    {
            _moveSpeed = InputManager.Instance.SprintAction.inProgress? PlayerManager.Instance.RunSpeed: PlayerManager.Instance.WalkSpeed;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 moveDirection, RaycastHit _slopeHit)
    {
        // Projeter la direction du mouvement sur la pente
        Vector3 slopeDirection = Vector3.ProjectOnPlane(moveDirection, _slopeHit.normal);

        // Retourner la direction corrigée pour le déplacement
        return slopeDirection;
    }
}