using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//仓库类柜台
public class ContainCounter : BaseCounter
{
     [SerializeField] private KitchenObjectSO KitchenObjectsSO;
   
    [SerializeField] private ContainCounterVisual containCounterVisual;
    

    // 仓库类柜台的交互方法
    public override void Interact(Player player)
    {
       
        if(player.IsHaveKitchenObject()) return;
        
          
           
           ClearKitchenObject(KitchenObjectsSO.prefab);
            
      
            TransferKitchenObject(this, player);
        containCounterVisual.PlayOpen();
    }

    public void ClearKitchenObject(GameObject kitchenObjectPrefab){
       KitchenObject kitchenObject = GameObject.Instantiate(KitchenObjectsSO.prefab, GetPutPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }
}
