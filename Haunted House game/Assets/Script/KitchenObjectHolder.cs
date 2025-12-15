using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectHolder : MonoBehaviour
{
    public static event EventHandler OnDrop;
    public static event EventHandler OnPickUp;
   [SerializeField] private Transform PutPoint;
   private KitchenObject kitchenObject;

   public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }

   public KitchenObjectSO GetKitchenObjectSO(){
        return kitchenObject.GetKitchenObjectSO();
    }

public bool IsHaveKitchenObject(){
    return kitchenObject != null;
}

public void SetKitchenObject(KitchenObject kitchenObject)
{
    if(this.kitchenObject != kitchenObject&&kitchenObject != null&& this is BaseCounter)
    {
        OnDrop?.Invoke(this, EventArgs.Empty);
    }else if(this.kitchenObject != kitchenObject&&kitchenObject != null&& this is Player)
    {
        OnPickUp?.Invoke(this, EventArgs.Empty);
    }

    this.kitchenObject = kitchenObject;

        kitchenObject.transform.localPosition = Vector3.zero;
    
}

public Transform GetPutPoint(){
    return PutPoint;
}

    // 静态方法用于在两个持有器之间转移物体
    public static void TransferKitchenObject(KitchenObjectHolder sourceHolder, KitchenObjectHolder targetHolder)
    {
        // 参数验证
        if(sourceHolder == null || targetHolder == null)
        {
            Debug.LogWarning("源持有器或目标持有器为空");
            return;
        }
        
        // 检查源持有器是否有物体
        if(sourceHolder.GetKitchenObject() == null)
        {
            Debug.LogWarning("源持有器为空，无法转移物体");
            return;
        }
        
        // 检查目标持有器是否已有物体
        if(targetHolder.GetKitchenObject() != null)
        {
            Debug.LogWarning("目标持有器已被占用");
            return;
        }
        
        // 实现物体转移
        targetHolder.AddKitchenObject(sourceHolder.GetKitchenObject());
        sourceHolder.ClearKitchenObject();
        
        Debug.Log("物体转移成功");
    }
    public void AddKitchenObject(KitchenObject kitchenObject)
    {
        if(kitchenObject != null)
        {
            kitchenObject.transform.SetParent(PutPoint);
            SetKitchenObject(kitchenObject);
        }
    }



    public void ClearKitchenObject(){
       this.kitchenObject = null;
    }

    public void DestroyKitchenObject(){
       Destroy(kitchenObject.gameObject);
       ClearKitchenObject();
    }

    public void CreateKitchenObject(GameObject kitchenObjectPrefab){
       KitchenObject kitchenObject = GameObject.Instantiate(kitchenObjectPrefab, GetPutPoint()).GetComponent<KitchenObject>();
        SetKitchenObject(kitchenObject);
    }

    public static void ClearStaticData(){
        OnDrop = null;
        OnPickUp = null;
    }
}
