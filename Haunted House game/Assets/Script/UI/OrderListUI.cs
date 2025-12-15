using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OrderListUI : MonoBehaviour
{
    [SerializeField] private Transform recipeParent;
    [SerializeField] private RecipeUI recipeUITemplate;

    private void Start()
    {
        recipeUITemplate.gameObject.SetActive(false);
        OrderManager.Instance.OnRecipeSpawned += OrderManager_OnRecipeSpawned;
        
        // 初始化时确保鼠标状态正确
        if (gameObject.activeInHierarchy)
        {
            ShowCursor();
        }
    }

    private void OrderManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        // 先清除所有现有的RecipeUI
        foreach (Transform child in recipeParent)
        {
            if(child != recipeUITemplate.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        // 获取订单列表并创建UI
        List<RecipeSO> recipeSOList = OrderManager.Instance.GetOrderList();
        foreach(RecipeSO recipeSO in recipeSOList)
        {
            RecipeUI recipeUI = GameObject.Instantiate(recipeUITemplate);
            recipeUI.transform.SetParent(recipeParent);
            recipeUI.gameObject.SetActive(true);
            recipeUI.UpdateUI(recipeSO);
          
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
