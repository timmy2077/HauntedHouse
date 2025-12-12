using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public UIDocument uiDocument;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;
    bool m_HasAudioPlayed;
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    
    private VisualElement m_EndScreen;
    private VisualElement m_CaughtScreen;
    private VisualElement m_CurrentScreen;
    
    void Start()
    {
        m_EndScreen = uiDocument.rootVisualElement.Q<VisualElement>("EndScreen");
        m_CaughtScreen = uiDocument.rootVisualElement.Q<VisualElement>("CaughtScreen");
        
        // 初始隐藏结束屏幕
        m_EndScreen.style.opacity = 0f;
        m_CaughtScreen.style.opacity = 0f;
        m_EndScreen.visible = false;
        m_CaughtScreen.visible = false;
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }
    
    public void CaughtPlayer ()
    {
        m_IsPlayerCaught = true;
    }
    
    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            m_CurrentScreen = m_EndScreen;
            EndLevel(false, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            m_CurrentScreen = m_CaughtScreen;
            EndLevel(true, caughtAudio);
        }
    }
    
    void EndLevel (bool doRestart, AudioSource audioSource)
    {
        // 显示当前结束屏幕
        m_CurrentScreen.visible = true;
        
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }
        
        m_Timer += Time.deltaTime;
        
        // 计算淡入效果
        float fadeProgress = Mathf.Clamp01(m_Timer / fadeDuration);
        m_CurrentScreen.style.opacity = fadeProgress;
        
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene ("Main");
            }
            else
            {
                Time.timeScale = 0;
                Application.Quit ();
            }
        }
    }
}