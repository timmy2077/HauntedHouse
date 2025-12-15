using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 第一人称相机控制器
/// </summary>
public class CameraControl : MonoBehaviour
{
    public Transform player; // 玩家引用
    private float mouseY; // 鼠标输入值（仅用于上下旋转）
    public float sensitivity = 100f; // 鼠标灵敏度
    public float xRotation = 0f; // 相机上下旋转角度
    public float headHeight = 1.6f; // 玩家头部高度

    private void Start()
    {
        // 锁定鼠标到屏幕中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // 初始化相机位置到玩家头部
        UpdateCameraPosition();
    }

    private void Update()
    {
        // 获取鼠标输入
        mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        
        // 计算上下旋转角度并限制范围
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);
        
        // 相机上下旋转（绕X轴）
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        // 保持相机位置在玩家头部
        UpdateCameraPosition();
    }

    /// <summary>
    /// 更新相机位置到玩家头部（同时包含高度和水平偏移）
    /// </summary>
    private void UpdateCameraPosition()
    {
        // 设置相机位置为玩家位置加上头部高度偏移和水平偏移
        transform.position = player.position + Vector3.up * headHeight + player.forward * 0.2f;
    }

    private void OnDestroy()
    {
        // 恢复鼠标状态
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
