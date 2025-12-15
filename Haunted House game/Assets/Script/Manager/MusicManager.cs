using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    private const string MUSIC_KEY = "music";
    
    private AudioSource audioSource;
    private float originalVolume;
   private int volume=5;
    
    private void Awake(){
        Instance = this;
         LoadVolume();
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        originalVolume = audioSource.volume;
       
        UpdateVolume();
        
    }

   public void ChangeVolume(){
        volume ++;
        if(volume>10){
            volume = 0;
        }
        UpdateVolume();
        SaveVolume();
    }
   public int GetVolume(){
        return volume;
   }

   private void UpdateVolume(){
    if(volume==0){
        audioSource.enabled = false;
    }else{
        audioSource.enabled = true;
        audioSource.volume = originalVolume*(volume/10f);
    }
       
   }

   private void SaveVolume(){
        PlayerPrefs.SetInt(MUSIC_KEY,volume);
   }
   private void LoadVolume(){
        volume = PlayerPrefs.GetInt(MUSIC_KEY,volume);
      
   }
}
