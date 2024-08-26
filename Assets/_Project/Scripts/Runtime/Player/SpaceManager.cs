using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpaceManager : MonoBehaviour
{

    public bool isGrounded { get; private set; }
    public bool haveRoof { get; private set; }
    public bool isOnSlope { get; private set; }
    public RaycastHit slopeHit { get; private set; }

    private RaycastHit _slopeHit;

    [SerializeField] private float maxSlopeAngle = 25; 

    [SerializeField] private Transform bottomCharacter;
    [SerializeField] private Transform topCharacterCrouch;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(bottomCharacter.position, groundCheckRadius, groundLayer);
        haveRoof = Physics.CheckSphere(topCharacterCrouch.position, groundCheckRadius, groundLayer);
        OnSlop();
    }

    private void OnSlop()
    {
        //Le rayon fait le scale du perso divisé par deux et multiplié par la taille du perso avec la marge d'erreur
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, transform.localScale.y * 0.5f * 2f + 0.3f, groundLayer))
        {
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            isOnSlope = angle < maxSlopeAngle && angle != 0;
            Debug.DrawRay(transform.position, Vector3.down * transform.localScale.y, Color.red);
        }
        else
        {
            isOnSlope = false;
            Debug.DrawRay(transform.position, Vector3.down * transform.localScale.y, Color.green);
        }
        slopeHit = _slopeHit;
    }
}
