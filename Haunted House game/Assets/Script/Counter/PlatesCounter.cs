using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    [SerializeField] private float spawnRate = 3;
    [SerializeField] private KitchenObjectSO plateSO;
    [SerializeField] private int plateCountMax=5;//盘子最大数
   private List<KitchenObject> plateList=new List<KitchenObject>();
    private float timer = 0f;

    private void Update()
    {
        if(plateList.Count<plateCountMax)
        {
             timer += Time.deltaTime;
        }
       
        // 只有当计时器超过生成速率且当前没有盘子时才创建新盘子
        if (timer > spawnRate && plateList.Count<plateCountMax)
        {
            // 创建盘子
          SpawnPlate();
            // 重置计时器
            timer = 0f;
        }
    }
  
    public override void Interact(Player player)
    {
       if(player.IsHaveKitchenObject()==false)
        {
           if(plateList.Count>0)
           {
              // 实现物体转移
        player.AddKitchenObject(plateList[plateList.Count-1]);
        plateList.RemoveAt(plateList.Count-1);
           }
        }
    }  

     public void SpawnPlate(){
        if(plateList.Count>=plateCountMax)
        {
            timer = 0f;
            return;
        }
       KitchenObject kitchenObject = GameObject.Instantiate(plateSO.prefab, GetPutPoint()).GetComponent<KitchenObject>();
        kitchenObject.transform.localPosition = Vector3.zero+Vector3.up*0.1f*plateList.Count;
       
        plateList.Add(kitchenObject);
    }
}
