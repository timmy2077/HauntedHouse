using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class GameClickUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    [SerializeField] private UnityEngine.UI.Image progressImage; // 明确指定使用UnityEngine.UI.Image
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private UIDocument uiDocument; // 添加UIDocument引用
    [SerializeField] private AudioSource exitAudio;
    [SerializeField] private AudioSource caughtAudio;
    
    // 添加与GameEnding一致的屏幕元素，支持多种游戏结束类型
    private VisualElement m_EndScreen;
    private VisualElement m_CaughtScreen;
    private VisualElement m_CurrentScreen; // 当前显示的屏幕
    private bool m_HasAudioPlayed; // 确保音效只播放一次

    
    void Start()
    {
        // 初始化UIDocument和VisualElement，与GameEnding保持一致
        if(uiDocument != null) {
            // 查找所有游戏结束屏幕元素
            m_EndScreen = uiDocument.rootVisualElement.Q<VisualElement>("GameOverScreen");
            m_CaughtScreen = uiDocument.rootVisualElement.Q<VisualElement>("CaughtScreen");
            
            // 初始隐藏所有屏幕
            InitializeScreen(m_EndScreen);
            InitializeScreen(m_CaughtScreen);
        }
        
        // 初始化音效播放标志
        m_HasAudioPlayed = false;
        
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    // 初始化屏幕元素
    private void InitializeScreen(VisualElement screen) {
        if(screen != null) {
            screen.style.opacity = 0f;
            screen.visible = false;
        }
    }

    void Update(){
        if(GameManager.Instance.IsGamePlayingState()){
            progressImage.fillAmount =  GameManager.Instance.GetGamePlayingTimerNormalized();
            progressText.text = Mathf.CeilToInt(GameManager.Instance.GetGamePlayingTimer()).ToString();
        }
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e){
        if(GameManager.Instance.IsGamePlayingState()){
            Show();
        }else if(GameManager.Instance.IsGameOverState()){
            ShowGameEndScreen(); // 游戏结束时显示合适的VisualElement
        }else{
            Hide();
        }
    }

    private void Show(){
        uiParent.SetActive(true);
        
        // 确保所有游戏结束屏幕隐藏
        HideAllScreens();
    }
    
    private void Hide(){
        uiParent.SetActive(false);
    }
    
    // 隐藏所有游戏结束屏幕
    private void HideAllScreens() {
        InitializeScreen(m_EndScreen);
        InitializeScreen(m_CaughtScreen);
    }
    
    // 显示游戏结束屏幕，与GameEnding一样的逻辑
    private void ShowGameEndScreen(){
        // 隐藏UGUI倒计时UI
        uiParent.SetActive(false);
        
        // 隐藏所有屏幕
        HideAllScreens();
        
        // 尝试查找并显示合适的屏幕，优先显示被抓住的屏幕
        // 如果没有被抓住的屏幕，显示普通游戏结束屏幕
        AudioSource audioSource = null; // 当前要播放的音效
        
        if(m_CaughtScreen != null) {
            m_CurrentScreen = m_CaughtScreen;
            audioSource = caughtAudio; // 被抓住屏幕使用caughtAudio
        } else if(m_EndScreen != null) {
            m_CurrentScreen = m_EndScreen;
            audioSource = exitAudio; // 普通游戏结束屏幕使用exitAudio
        }
        
        // 显示当前屏幕
        if(m_CurrentScreen != null) {
            m_CurrentScreen.visible = true;
            m_CurrentScreen.style.opacity = 1f;
            
            // 播放相应音效，确保只播放一次
            if(!m_HasAudioPlayed && audioSource != null) {
                audioSource.Play();
                m_HasAudioPlayed = true;
            }
        }
    }
}
