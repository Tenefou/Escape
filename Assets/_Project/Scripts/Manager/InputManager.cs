using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    // Instance unique (singleton) de l'InputManager
    private PlayerInput _input;
    public static InputManager Instance { get; private set; }

    // Actions de joueur
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _crouchAction;
    private InputAction _sprintAction;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _moveAction = _input.actions.FindAction("Move");
        _jumpAction = _input.actions.FindAction("Jump");
        _crouchAction = _input.actions.FindAction("Crouch");
        _sprintAction = _input.actions.FindAction("Sprint");
        // Singleton pattern : s'assure qu'il n'y ait qu'une instance d'InputManager dans la scène
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Garde l'instance entre les scènes
        }
        else
        {
            Destroy(gameObject); // Détruit les doublons
        }
    }

    private void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
        _crouchAction.Enable();
        _sprintAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
        _crouchAction.Disable();
        _sprintAction.Disable();
    }

    // Expose les actions publiques via des getters
    public InputAction MoveAction => _moveAction;
    public InputAction JumpAction => _jumpAction;
    public InputAction CrouchAction => _crouchAction;
    public InputAction SprintAction => _sprintAction;
}
