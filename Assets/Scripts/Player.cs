using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set;}
  
    public event EventHandler OnStep;
    public event EventHandler OnItemPickup;
    public event EventHandler OnItemDrop;
    public event EventHandler<OnSelectCounterChangedEventArgs> OnSelectCounterChanged;
    public class OnSelectCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }
    
    [SerializeField] private GameInput _input;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform objectHoldpoint;
    [SerializeField] private  LayerMask counterLayermask;
    bool isWalking = false; 

    float rotateSpeed = 10f;
    Vector2 inputVector;
    Vector3 lastInteractDir;
    BaseCounter selectedCounter;
    KitchenObject kitchenObject;
    float stepSoundTimer = 0.1f;
    private void Awake() {
        if(Instance != null){
            Debug.LogError("More than 1 instance exits");
        }
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        _input.OnInteractionAction += GameInput_OnInteractionAction;
        _input.OnInteractionAlternativeAction += GameInput_OnAlternativeInteractionAction;
    }

    private void GameInput_OnAlternativeInteractionAction(object sender, EventArgs e)
    {
        if(selectedCounter != null){
            selectedCounter.InteractAlternative(this);
        }
    }

    private void GameInput_OnInteractionAction(object sender, EventArgs e)
    {
       if(selectedCounter != null){
            selectedCounter.Interact(this);
       }
    }

    // Update is called once per frame
    void Update()
    {
       MovementHandle();
       InteractionHandle();
    }
    
    public bool IsWalking(){
        return isWalking;
    }

    private void InteractionHandle(){
        inputVector = _input.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        if(moveDir != Vector3.zero) {
            lastInteractDir = moveDir;
        }
        float interactDistance = 2f;
        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit hit, interactDistance, counterLayermask)){
            if(hit.transform.TryGetComponent(out BaseCounter baseCounter)){
                // Has Clear Counter
                if(baseCounter != selectedCounter) {
                   SetSelectedCounter(baseCounter);
                }
                
            }
            else{
                   SetSelectedCounter(null);
            }
        }
        else{
           SetSelectedCounter(null);
        }

    }
    private void MovementHandle(){
        inputVector = _input.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveStep = speed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 1.5f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveStep);

        if(!canMove){
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0).normalized;
            canMove = (moveDirX.x < -0.5f || moveDirX.x >  0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveStep);
            if(canMove) {
                moveDir = moveDirX;
            }
            else{
                Vector3 moveDirZ = new Vector3 (0 , 0, moveDir.z).normalized;
                canMove = (moveDirZ.z < -0.5f || moveDirZ.z >  0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveStep);

                if (canMove) {
                    moveDir = moveDirZ;
                }

                else{
                    //Cannot move in any direction
                }
            }
        }
        if(canMove){
            transform.position += moveDir * moveStep;
        }
        isWalking = moveDir != Vector3.zero;
        stepSoundTimer -= Time.deltaTime;
        if(isWalking && stepSoundTimer <= 0) 
        {
            stepSoundTimer = 0.1f;
            OnStep?.Invoke(this, EventArgs.Empty);
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    void SetSelectedCounter(BaseCounter selectedCounter){
        this.selectedCounter = selectedCounter;

        OnSelectCounterChanged?.Invoke(this, new OnSelectCounterChangedEventArgs{ 
            selectedCounter = selectedCounter
        }); 

        OnItemDrop?.Invoke(this, EventArgs.Empty);
    }
    
    public void ClearKitchenObject(){
        kitchenObject = null;
    }

    public void SetKitChenObject(KitchenObject kitchenObject){
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null){
            OnItemPickup?.Invoke(this, EventArgs.Empty);
        }
    }


    public Transform GetKitchenObjectFollowTransform(){
        return objectHoldpoint;
    }

    public bool HasKitchenObject(){
        return kitchenObject != null;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
}

