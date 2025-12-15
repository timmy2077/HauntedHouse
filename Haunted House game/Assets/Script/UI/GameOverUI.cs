using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameOverUI : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOverState())
        {
            Time.timeScale = 0;
            ShowCursor();
        }
        else
        {
            Time.timeScale = 1;
            HideCursor();
        }
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
}