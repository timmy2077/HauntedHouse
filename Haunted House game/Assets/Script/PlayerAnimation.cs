using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家动画控制器类 - 控制玩家角色的动画状态
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    #region 常量定义
    /// <summary>
    /// 行走动画参数名称
    /// </summary>
    private const string IS_WALKING = "IsWalking";
    #endregion

    #region 私有变量
    /// <summary>
    /// 动画控制器组件引用
    /// </summary>
    private Animator animator;
    
    /// <summary>
    /// 玩家组件引用，通过编辑器赋值
    /// </summary>
    [SerializeField] private Player player;
    #endregion

    #region 生命周期方法
    /// <summary>
    /// 初始化动画组件
    /// </summary>
    private void Start()
    {
        // 获取自身的Animator组件
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// 每帧更新动画状态
    /// </summary>
    private void Update()
    {
        // 根据玩家的行走状态更新动画参数
        // 当player.IsWalking为true时播放行走动画，否则播放站立动画
        animator.SetBool(IS_WALKING, player.IsWalking);
    }
    #endregion
}
