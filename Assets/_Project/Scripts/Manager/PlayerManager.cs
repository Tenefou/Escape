using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   private Player _player;

    public static PlayerManager Instance { get; private set; }

    [SerializeField] private float _playerWalkSpeed;
    [SerializeField] private float _playerRunSpeed;
    [SerializeField] private float _playerJumpHeigh;
    [SerializeField] private float _playerJumpCooldown;
    [SerializeField] private float _playerCrounchScale;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float WalkSpeed => _playerWalkSpeed;
    public float RunSpeed => _playerRunSpeed;
    public float JumpHeight => _playerJumpHeigh;
    public float JumpCooldown => _playerJumpCooldown;
    public float CrounchScale => _playerCrounchScale;

}
