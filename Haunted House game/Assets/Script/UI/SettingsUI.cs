using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI Instance { get; private set; }
   
   [SerializeField] private GameObject uiParent;
   [SerializeField] private Button closeButton;
   [SerializeField] private Button soundButton;
   [SerializeField] private TextMeshProUGUI soundButtonText;
   [SerializeField] private Button musicButton;
   [SerializeField] private TextMeshProUGUI musicButtonText;

 private void Awake(){
        Instance = this;
    }

private void Start(){
    Hide();
    UpdateVisual();
    soundButton.onClick.AddListener(() => {
        SoundManager.Instance.ChangeVolume();
        UpdateVisual();
    });
    musicButton.onClick.AddListener(() => {
        MusicManager.Instance.ChangeVolume();
        UpdateVisual();
    });
    closeButton.onClick.AddListener(() => {
        Hide();
    });
}

public void Show(){    
    uiParent.SetActive(true);
    // 禁用Pause输入，防止在设置界面按Esc触发暂停
    GameInput.Instance.DisablePauseInput();
   
}

public void Hide(){    
    uiParent.SetActive(false);
    // 重新启用Pause输入
    GameInput.Instance.EnablePauseInput();
    
}
    

void UpdateVisual(){
    soundButtonText.text = "音效大小 " + SoundManager.Instance.GetVolume();
    musicButtonText.text = "音乐大小 " + MusicManager.Instance.GetVolume();
}
}