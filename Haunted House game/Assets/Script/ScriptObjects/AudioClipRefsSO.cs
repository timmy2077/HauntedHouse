using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "AudioClipRefs", menuName = "ScriptableObjects/AudioClipRefs")]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] footstep;
    public AudioClip[] income;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] orderFail;
    public AudioClip[] stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] orderSuccess;
    public AudioClip[] platePop;
    public AudioClip[] warning;
}
