using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class AudioClipSO : ScriptableObject {
    public AudioClip[] itemDrop;
    public AudioClip[] itemPickUp;
    public AudioClip[] chop;
    public AudioClip[] footStep;
    public AudioClip[] deliverSuccess;
    public AudioClip[] deliverFail;
    public AudioClip[] stoveSizzle;
    public AudioClip[] trashDrop;
    public AudioClip[] warning;
}
