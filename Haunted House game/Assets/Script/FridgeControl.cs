using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeControl : BaseCounter
{
    public GameObject openFridge;
    private Animator animator;
    // 标记是否正在执行延迟操作，避免在延迟期间重复操作
    private bool isProcessingAction = false;
    // 延迟时间（秒）
    private float delayTime = 0.5f;
   
    void Awake()
    {
        animator = openFridge.GetComponent<Animator>();
    }
   
   
    /// <summary>
    /// 重写交互方法，实现物体放置和盘子添加食物的功能
    /// </summary>
    public override void Interact(Player player)
    {
        // 如果正在执行延迟操作，不响应新的交互
        if (isProcessingAction)
        {
            return;
        }
        
        // 玩家手上有物体的情况
        if (player.IsHaveKitchenObject())
        {
            // 检查玩家手上的物体是否是盘子
            if (player.GetKitchenObject().TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject))
            {
                // 如果柜台上有物体
                if (IsHaveKitchenObject())
                {
                    // 尝试将柜台上的食物添加到盘子中
                    KitchenObject kitchenObject = GetKitchenObject();
                    if (kitchenObject != null)
                    {
                        bool isSuccess = plateKitchenObject.AddKitchenObjectSO(kitchenObject.GetKitchenObjectSO());
                        if (isSuccess)
                        {
                            // 立即触发开门动画
                            animator.SetTrigger("Open");
                            // 延迟执行实际的物体销毁操作
                            StartCoroutine(DelayedAction(() => 
                            {
                                DestroyKitchenObject();
                            }));
                        }
                    }
                }
                // 如果柜台上没有物体，直接将盘子放在柜台上
                else
                {
                    // 对于盘子操作，不需要动画，直接延迟执行放置
                    StartCoroutine(DelayedAction(() => 
                    {
                        TransferKitchenObject(player, this);
                    }));
                }
            }
            // 玩家手上不是盘子的情况
            else
            {
                // 如果柜台上没有物体，将玩家手上的物体放在柜台上
                if (!IsHaveKitchenObject())
                {
                    // 立即触发开门动画
                    animator.SetTrigger("Open");
                    // 延迟执行实际的物体转移操作
                    StartCoroutine(DelayedAction(() => 
                    {
                        TransferKitchenObject(player, this);
                    }));
                }
                // 如果柜台上有物体，可能需要额外处理
                else
                {
                    if (GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
                    {
                        if (plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
                        {
                            // 对于添加食物到盘子，不需要动画，直接延迟执行销毁
                            StartCoroutine(DelayedAction(() => 
                            {
                                player.DestroyKitchenObject();
                            }));
                        }
                    }
                }
            }
        }
        // 玩家手上没有物体的情况
        else
        {
            // 如果柜台上有物体，将其转移给玩家
            if (IsHaveKitchenObject())
            {
                // 立即触发开门动画
                animator.SetTrigger("Open");
                // 延迟执行实际的物体转移操作
                StartCoroutine(DelayedAction(() => 
                {
                    TransferKitchenObject(this, player);
                }));
            }
            // 如果柜台上也没有物体，不做任何操作
        }
    }
    
    /// <summary>
    /// 延迟执行操作的协程
    /// </summary>
    /// <param name="action">要执行的操作</param>
    private IEnumerator DelayedAction(System.Action action)
    {
        isProcessingAction = true;
        
        // 等待指定的延迟时间，在这段时间内显示动画效果
        yield return new WaitForSeconds(delayTime);
        
        // 执行操作
        if (action != null)
        {
            action.Invoke();
        }
        
        // 操作完成后，允许新的交互
        isProcessingAction = false;
    }
}
