using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemove;
    [SerializeField] private KitchenSO plateKitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateCD = 4f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;


    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateCD && plateSpawnAmount < plateSpawnAmountMax){
            plateSpawnAmount++;
            OnPlateSpawned?.Invoke(this,  EventArgs.Empty);
            spawnPlateTimer = 0;
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject()){
            // Player hand is empty
            if(plateSpawnAmount > 0){
                //There is atleast one plate spawned
                plateSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
