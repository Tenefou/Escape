using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{

    public bool isGrounded { get; private set; }
    public bool haveRoof { get; private set; }

    [SerializeField] private Transform bottomCharacter;
    [SerializeField] private Transform topCharacterCrouch;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(bottomCharacter.position, groundCheckRadius, groundLayer);
        haveRoof = Physics.CheckSphere(topCharacterCrouch.position, groundCheckRadius, groundLayer);
    }
}
