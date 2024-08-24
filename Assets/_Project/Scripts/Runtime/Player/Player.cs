using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private PlayerInputActions _inputActions;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _inputActions = new PlayerInputActions();
        _inputActions.Enable();
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }
}
