using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Sction Asset")]
    [SerializeField] private InputActionAsset playerControls;

    [Header("Action Map Name Refrence")]
    [SerializeField] private string actionMapName = "Player";

    [Header("Action Name Refrences")]
    [SerializeField] private string move = "Move";
    [SerializeField] private string attack = "Attack";
    [SerializeField] private string pickUp = "PickUp";
    [SerializeField] private string hideout = "Hideout";
    [SerializeField] private string scroll = "Scroll";
    [SerializeField] private string look = "Look";
    
    private InputAction moveAction;
    private InputAction attackAction;
    private InputAction pickUpAction;
    private InputAction hideoutAction;
    private InputAction scrollAction;
    private InputAction lookAction;

    public Vector2 MoveInput { get; private set; }
    public bool AttackTriggered { get; private set; }
    public bool PickUpTriggered { get; private set; }
    public bool HideoutTriggered { get; private set; }
    public float ScrollValue { get; private set; }
    public Vector2 LookInput { get; private set; }

    public static PlayerInputHandler Instance { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
        pickUpAction = playerControls.FindActionMap(actionMapName).FindAction(pickUp);
        hideoutAction = playerControls.FindActionMap(actionMapName).FindAction(hideout);
        scrollAction = playerControls.FindActionMap(actionMapName).FindAction(scroll);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        RegisterInputActions();

      
    }

    void RegisterInputActions() {
        moveAction.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        moveAction.canceled += ctx => MoveInput = Vector2.zero;

        attackAction.performed += ctx => AttackTriggered = true;
        attackAction.canceled += ctx => AttackTriggered = false;

        pickUpAction.performed += ctx => PickUpTriggered = true;
        pickUpAction.canceled += ctx => PickUpTriggered = false; 

        hideoutAction.performed += ctx => HideoutTriggered = true;
        hideoutAction.canceled += ctx => HideoutTriggered = false;

        scrollAction.performed += ctx => ScrollValue = ctx.ReadValue<float>();
        scrollAction.canceled += ctx => ScrollValue = 0f;

        lookAction.performed += ctx => LookInput = ctx.ReadValue<Vector2>();
        lookAction.canceled += ctx => LookInput = Vector2.zero;
    }


    private void OnEnable() {
        moveAction.Enable();
        attackAction.Enable();
        pickUpAction.Enable();
        hideoutAction.Enable();
        scrollAction.Enable();
        lookAction.Enable();
    }
    
    private void OnDisable() {
        moveAction.Disable();
        attackAction.Disable();
        pickUpAction.Disable();
        hideoutAction.Disable();
        scrollAction.Disable();
        lookAction.Disable();
    }
}