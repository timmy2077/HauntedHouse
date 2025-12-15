using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Loader;


public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
    
    private void Start(){
        Hide();
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused += GameManager_OnGameUnPaused;
    }
    private void GameManager_OnGamePaused(object sender, System.EventArgs e){
        Show();
    }
    private void GameManager_OnGameUnPaused(object sender, System.EventArgs e){
        Hide();
    }
    private void Show(){
        uiParent.SetActive(true);
        ShowCursor();
    }
    private void Hide(){
        uiParent.SetActive(false);
        HideCursor();
    }
    
    // 显示鼠标
    private void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // 隐藏并锁定鼠标
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    private void OnDestroy(){
        // 取消订阅事件，防止对象销毁后仍尝试调用方法
        if(GameManager.Instance != null){
            GameManager.Instance.OnGamePaused -= GameManager_OnGamePaused;
            GameManager.Instance.OnGameUnPaused -= GameManager_OnGameUnPaused;
        }
    }
}
