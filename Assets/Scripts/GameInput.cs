using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public static GameInput Instance { get; private set; }
    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractionAction;
    public event EventHandler OnInteractionAlternativeAction;
    public event EventHandler OnBindingRebind;
    public PlayerInputAction playerInputActions;
    Vector2 inputVector;

    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else{
            Debug.LogWarning("Another instance exsited!");
        }
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_peformed;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
        playerInputActions.Player.Pause.performed += Pause_peformed;
    }

    private void OnDestroy() {
        playerInputActions.Player.Interact.performed -= Interact_peformed;
        playerInputActions.Player.InteractAlternative.performed -= InteractAlternative_performed;
        playerInputActions.Player.Pause.performed -= Pause_peformed;

        playerInputActions.Dispose();
    }
    private void Pause_peformed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {
        if(!GameManager.Instance.IsGamePlaying()) return Vector2.zero;
        inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    void Interact_peformed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        if(!GameManager.Instance.IsGamePlaying()) return;
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternative_performed(InputAction.CallbackContext context)
    {
        if(!GameManager.Instance.IsGamePlaying()) return;
        OnInteractionAlternativeAction?.Invoke(this, EventArgs.Empty);
    }

    public void Rebinding(Binding binding, Action OnActionRebound){
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch(binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Player.Movement;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.AlternativeInteract:
                inputAction = playerInputActions.Player.InteractAlternative;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
        .OnComplete(callback => {
            //Debug.Log(callback.action.bindings[1].path);
            //Debug.Log(callback.action.bindings[1].overridePath);
            callback.Dispose();
            playerInputActions.Player.Enable();
            OnActionRebound();

            //playerInputActions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

            OnBindingRebind? .Invoke(this, EventArgs.Empty);
        })
        .Start();
    }
    
    public string GetBindingText(Binding binding){
        switch(binding){
            default:
            case Binding.Move_Up:
                return playerInputActions.Player.Movement.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Player.Movement.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Player.Movement.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Player.Movement.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Binding.AlternativeInteract:
                return playerInputActions.Player.InteractAlternative.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
        }
    }
     
    
    public enum Binding {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Pause,
        AlternativeInteract
    }
}
