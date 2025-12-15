using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏输入管理类 - 处理玩家输入并分发事件
/// </summary>
public class GameInput : MonoBehaviour
{
public static GameInput Instance { get; private set; }

       

    
    #region 事件定义
    /// <summary>
    /// 当玩家按下交互键(E)时触发的事件
    /// </summary>
    public event EventHandler OnInteractAction;
    #endregion

    /// <summary>
    /// 当玩家按下操作键时触发的事件
    /// </summary>
    public event EventHandler OnOperateAction;

    /// <summary>
    /// 当玩家按下暂停键时触发的事件
    /// </summary>
    public event EventHandler OnPauseAction;



    #region 私有变量
    /// <summary>
    /// 输入控制系统实例
    /// </summary>
    private PlayerControl playerControl;
    #endregion

    #region 生命周期方法
    /// <summary>
    /// 初始化输入控制系统
    /// </summary>
    private void Awake()
    {
         Instance = this;
        // 实例化PlayerControl类
        playerControl = new PlayerControl();
        // 启用PlayerControl类，默认是禁用的
        playerControl.Player.Enable();
        // 订阅交互按钮的performed事件
        playerControl.Player.Interact.performed += Interact_Performed;
        playerControl.Player.Operate.performed += Operate_Performed;
        playerControl.Player.Pause.performed += Pause_Performed;
    }
    #endregion

private void OnDestroy()
    {
        // 取消订阅交互按钮的performed事件，防止内存泄漏
        playerControl.Player.Interact.performed -= Interact_Performed;
        playerControl.Player.Operate.performed -= Operate_Performed;
        playerControl.Player.Pause.performed -= Pause_Performed;
        // 禁用PlayerControl类，防止继续接收输入
        playerControl.Disable();
    }

    /// <summary>
    /// 当玩家按下暂停键时触发的事件
    /// </summary>
    /// <param name="obj">输入回调上下文</param>
    private void Pause_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        print("11");
        // 安全地触发暂停事件，确保只有在有订阅者时才调用
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }



private void Operate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 安全地触发操作事件，确保只有在有订阅者时才调用
        OnOperateAction?.Invoke(this, EventArgs.Empty);
    }

    #region 输入处理方法
    /// <summary>
    /// 处理交互按钮按下事件
    /// </summary>
    /// <param name="obj">输入回调上下文</param>
    private void Interact_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        // 安全地触发交互事件，确保只有在有订阅者时才调用
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// 获取玩家的移动方向向量
    /// </summary>
    /// <returns>单位化的3D移动方向向量</returns>
    public Vector3 GetMovement()
    {
        // 读取玩家的2D移动输入
        Vector2 inputVector2 = playerControl.Player.Move.ReadValue<Vector2>();
        
        // 将2D输入转换为3D向量，Y轴设为0（平面移动）
        Vector3 dir = new Vector3(inputVector2.x, 0, inputVector2.y);
        
        // 单位化处理，确保斜向移动时速度不会因向量长度为根号2而加快
        dir = dir.normalized;
        
        return dir;
    }
    #endregion
    
    /// <summary>
    /// 禁用Pause输入，防止在设置界面按Esc触发暂停
    /// </summary>
    public void DisablePauseInput()
    {
        playerControl.Player.Pause.Disable();
    }
    
    /// <summary>
    /// 启用Pause输入
    /// </summary>
    public void EnablePauseInput()
    {
        playerControl.Player.Pause.Enable();
    }
}
