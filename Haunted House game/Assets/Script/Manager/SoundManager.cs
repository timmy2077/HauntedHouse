using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    [SerializeField] private int volume =5;
private const string SOUND_KEY = "sound";


private void Awake(){
    Instance = this;
     LoadVolume();
}

private void Start(){
    
    OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
    OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    CuttingCounter.OnCut += CuttingCounter_OnCut;
    KitchenObjectHolder.OnPickUp += KitchenObjectHolder_OnPickUp;
    KitchenObjectHolder.OnDrop += KitchenObjectHolder_OnDrop;
    TrashCounter.OnTrash += TrashCounter_OnTrash;

   
}

private void TrashCounter_OnTrash(object sender,System.EventArgs e){
    print("你牛大了，你把东西丢到了垃圾桶");
    PlaySound(audioClipRefsSO.trash);
}
private void KitchenObjectHolder_OnPickUp(object sender,System.EventArgs e){
    PlaySound(audioClipRefsSO.objectPickup);
}
private void KitchenObjectHolder_OnDrop(object sender,System.EventArgs e){
    PlaySound(audioClipRefsSO.objectDrop);
}
private void CuttingCounter_OnCut(object sender,System.EventArgs e){
    PlaySound(audioClipRefsSO.chop);
}
private void OrderManager_OnRecipeSuccessed(object sender,System.EventArgs e){
    PlaySound(audioClipRefsSO.deliverySuccess);
}
private void OrderManager_OnRecipeFailed(object sender,System.EventArgs e){
    PlaySound(audioClipRefsSO.deliveryFail);
}

public void PlayWarningSound(){
    PlaySound(audioClipRefsSO.warning);
}

public void PlayStepSound(float volumeMultiplier = 0.5f){
    PlaySound(audioClipRefsSO.footstep, volumeMultiplier);
}

private void PlaySound(AudioClip[] clips,float volumeMultiplier = 0.5f){
   PlaySound(clips,Camera.main.transform.position);
}

    private void PlaySound(AudioClip[] clips, Vector3 position, float volumeMultiplier = 0.5f){
        if(volume==0){
            return;
        }
        int index = Random.Range(0, clips.Length);
        AudioSource.PlayClipAtPoint(clips[index], position, volumeMultiplier*volume/10f);
    }

public void ChangeVolume(){
  volume++;
  if(volume>10){
    volume = 0;
  }
 SaveVolume();
}
public int GetVolume(){
    return volume;
}

private void SaveVolume(){
    PlayerPrefs.SetInt(SOUND_KEY,volume);
}
private void LoadVolume(){
    volume = PlayerPrefs.GetInt(SOUND_KEY,volume);
}

}
