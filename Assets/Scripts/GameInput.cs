using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractionAction;
    public event EventHandler OnInteractionAlternativeAction;
    public PlayerInputAction playerInputActions;
    Vector2 inputVector;

    private void Awake() {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_peformed;
        playerInputActions.Player.InteractAlternative.performed += InteractAlternative_performed;
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
     
}
