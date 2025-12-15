using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class GameMenuUI : MonoBehaviour
{
   [SerializeField] private Button startButton;
   [SerializeField] private Button quitButton;

void Start(){
    // 显示并解锁鼠标，确保在菜单界面可以正常使用鼠标
    ShowCursor();
    
    startButton.onClick.AddListener(() => {
        Loader.Load(Loader.Scene.GameScene);
    });
    quitButton.onClick.AddListener(() => {
#if UNITY_EDITOR
        // 在编辑器中停止播放模式
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在构建版本中退出游戏
        Application.Quit();
#endif
    });
}
void Update(){
    
    }
    
    // 显示并解锁鼠标
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // 当游戏对象启用时显示鼠标
    private void OnEnable()
    {
        ShowCursor();
    }
}



