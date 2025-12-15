using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CountDownUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI numberText;
    
    private void Start(){
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
       
    }
    private void Update(){
        if(GameManager.Instance.IsCountDownSate()){
            numberText.text = Mathf.Ceil(GameManager.Instance.GetCountDownTimer()).ToString();
        }
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e){
        if(GameManager.Instance.IsCountDownSate()){
            gameObject.SetActive(true);
            ShowCursor();
        }
        else{
            gameObject.SetActive(false);
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
    
    // 当游戏对象启用时显示鼠标
    private void OnEnable()
    {
        ShowCursor();
    }
    
    // 当游戏对象禁用时隐藏鼠标
    private void OnDisable()
    {
        HideCursor();
    }
    
}
