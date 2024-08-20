using KinematicCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public struct CharacterInput
{
    public Quaternion Rotation;
    public Vector2 Move;
}


public class PlayerCharacter : MonoBehaviour, ICharacterController
{
    [SerializeField] private KinematicCharacterMotor motor;
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float walkSpeed = 20f;

    private Quaternion _requestedRotation;
    private Vector3 _requestedMovement;

    public void Initialize()
    {
        motor.CharacterController = this;
    }

    public void UpdateInput(CharacterInput input)
    {
        _requestedRotation = input.Rotation;
        //Take 2D movements into 3D movement in a XZ plane
        _requestedMovement = new Vector3(input.Move.x, 0f, input.Move.y);

        //Debug.Log(input.Move.x + "y = " + input.Move.y);

        //Make sur that we always go the same speed diagonally and straigth
        _requestedMovement = Vector3.ClampMagnitude(_requestedMovement, 1f);

        //Orient the  input in the direction the player is looking 
        _requestedMovement = input.Rotation * _requestedMovement;
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        var groundedMovement = motor.GetDirectionTangentToSurface
            (
                direction: _requestedMovement,
                surfaceNormal: motor.GroundingStatus.GroundNormal
            ) * _requestedMovement.magnitude;
       
        currentVelocity = groundedMovement * walkSpeed;
    }

    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime)
    {
        //Update chara location to look at the same direction as the camera rotation

        var forward = Vector3.ProjectOnPlane
            (
                _requestedRotation * Vector3.forward,
                motor.CharacterUp
            );
        currentRotation = Quaternion.LookRotation(forward, motor.CharacterUp);
    }

    public void AfterCharacterUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void BeforeCharacterUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public bool IsColliderValidForCollisions(Collider coll)
    {
        throw new System.NotImplementedException();
    }

    public void OnDiscreteCollisionDetected(Collider hitCollider)
    {
        throw new System.NotImplementedException();
    }

    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    public void PostGroundingUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport)
    {
        throw new System.NotImplementedException();
    }

    public Transform GetCameraTarget() => cameraTarget;
}
