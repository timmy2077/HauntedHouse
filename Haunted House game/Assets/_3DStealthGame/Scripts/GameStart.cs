using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace StealthGame
{
    public class GameStart : MonoBehaviour
    {
        public UIDocument uiDocument;
        
        private Button m_StartButton;
        private Button m_ExitButton;
        
        void Start()
        {
            // 获取UI元素
            m_StartButton = uiDocument.rootVisualElement.Q<Button>("StartButton");
            m_ExitButton = uiDocument.rootVisualElement.Q<Button>("ExitButton");
            
            // 添加按钮点击事件
            if (m_StartButton != null)
            {
                m_StartButton.RegisterCallback<ClickEvent>(OnStartButtonClick);
            }
            
            if (m_ExitButton != null)
            {
                m_ExitButton.RegisterCallback<ClickEvent>(OnExitButtonClick);
            }
        }
        
        private void OnStartButtonClick(ClickEvent evt)
        {
            // 加载下一个场景，假设下一个场景名为"Main"
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        private void OnExitButtonClick(ClickEvent evt)
        {
            // 退出游戏
            Application.Quit();
            
            // 在编辑器模式下停止播放
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }
}