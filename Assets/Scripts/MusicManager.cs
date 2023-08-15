using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    [SerializeField] private AudioSource musicSrc;
    private float volume = 0.3f;
    private void Awake() {
        if(Instance == null){
            Instance = this;
        }
        else {
            Debug.LogWarning("Duplicates Instance!!");
        }
        musicSrc = GetComponent<AudioSource>();
    }
    

   public void ChangeVolume(){
        volume += .1f;
        if(volume > 1f){
            volume = 0f;
        }
        musicSrc.volume = volume;
    }

    public float GetVolume(){
        return volume;
    }
}
