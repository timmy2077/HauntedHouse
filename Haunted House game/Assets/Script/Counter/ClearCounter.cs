using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    /// <summary>
    /// 重写交互方法，实现物体放置和盘子添加食物的功能
    /// </summary>
    public override void Interact(Player player)
    {
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
                            // 添加成功后销毁柜台上的物体
                            DestroyKitchenObject();
                        }
                    }
                }
                // 如果柜台上没有物体，直接将盘子放在柜台上
                else
                {
                    TransferKitchenObject(player, this);
                }
            }
            // 玩家手上不是盘子的情况
            else
            {
                // 如果柜台上没有物体，将玩家手上的物体放在柜台上
                if (!IsHaveKitchenObject())
                {
                    TransferKitchenObject(player, this);
                }
                // 如果柜台上有物体，可能需要额外处理（目前不做任何操作）
           else{
            if(GetKitchenObject().TryGetComponent<PlateKitchenObject>(out plateKitchenObject))
           {
            if(plateKitchenObject.AddKitchenObjectSO(player.GetKitchenObjectSO()))
            {
                player.DestroyKitchenObject();
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
                TransferKitchenObject(this, player);
            }
            // 如果柜台上也没有物体，不做任何操作
        }
    }
}
