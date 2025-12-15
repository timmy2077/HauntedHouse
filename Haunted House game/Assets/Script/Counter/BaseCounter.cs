using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter :KitchenObjectHolder
{
    [SerializeField] private GameObject selectedCounter;

   public virtual void Interact(Player player)
    {
    
    Debug.Log("交互方法没有重写");
    }

    public virtual void InteractOperate(Player player)
    {
        Debug.Log("操作方法没有重写");
    }



    public void SelectCounter(){
        selectedCounter.SetActive(true);

    }

    public void CancelSelect(){
        selectedCounter.SetActive(false);
    }



}
