using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipSO audioClipSO;
    private float volume = 1f;
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else {
            Debug.LogWarning("Duplicates Instance!!");
        }
    }
    private void Start() {
        DeliverManager.Instance.OnRecipeSuccess +=  DeliverManager_OnRecipeSuccess;
        DeliverManager.Instance.OnRecipeFailed += DeliverManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnStep += Player_OnStep;
        Player.Instance.OnItemPickup += Player_OnItemPickup;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnTrashDrop += TrashCounter_OnTrashDrop;
        
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        BaseCounter counter = (BaseCounter)sender;
        PlaySound(audioClipSO.itemDrop, counter.transform.position);
    }

    private void Player_OnItemPickup(object sender, EventArgs e)
    {
        PlaySound(audioClipSO.itemPickUp, Player.Instance.transform.position);
    }

    private void TrashCounter_OnTrashDrop(object sender, EventArgs e)
    {
        TrashCounter counter = sender as TrashCounter;
        PlaySound(audioClipSO.trashDrop, counter.transform.position);
    }

    private void Player_OnStep(object sender, EventArgs e)
    {
        PlaySound(audioClipSO.footStep, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipSO.chop, cuttingCounter.transform.position);
    }

    private void DeliverManager_OnRecipeFailed(object sender, EventArgs e)
    {
        PlaySound(audioClipSO.deliverFail, DeliverManager.Instance.deliverCounter.transform.position);
    }

    private void DeliverManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        PlaySound(audioClipSO.deliverSuccess, DeliverManager.Instance.deliverCounter.transform.position);
    }

    void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f){
        PlaySound(audioClips[UnityEngine.Random.Range(0, audioClips.Length)], position, volume);
    }

    void PlaySound(AudioClip audioclip, Vector3 position, float volumeMultiplier = 1f){
        AudioSource.PlayClipAtPoint(audioclip, position, volume * volumeMultiplier);
    }

    public void ChangeVolume(){
        volume += .1f;
        if(volume > 1f){
            volume = 0f;
        }
    }

    public float GetVolume(){
        return volume;
    }
}
