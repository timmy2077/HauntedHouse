using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
public class kitchenObjectSO_Model{
    public KitchenObjectSO kitchenObjectSO;
    public GameObject model;
}
   [SerializeField] private List<kitchenObjectSO_Model> modelMap;

   public void ShowkitchenObject(KitchenObjectSO kitchenObjectSO)
   {
    foreach(kitchenObjectSO_Model item in modelMap)
    {
        if(item.kitchenObjectSO == kitchenObjectSO)
        {
            item.model.SetActive(true);return;
        }
       
    }
   }
}
