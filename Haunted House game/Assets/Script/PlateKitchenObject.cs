using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    [SerializeField] private PlateCompleteVisual plateCompleteVisual;
   [SerializeField] private KitchenObjectGradeUI kitchenObjectGradeUI;
private List<KitchenObjectSO> kitchenObjectSOList = new List<KitchenObjectSO>();


public bool AddKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
{
    if(kitchenObjectSOList.Contains(kitchenObjectSO))
    {
        return false;
    }
    if(validKitchenObjectSOList.Contains(kitchenObjectSO)==false)
    {
        return false;
    }
    plateCompleteVisual.ShowkitchenObject(kitchenObjectSO);
    kitchenObjectGradeUI.ShowKitchenObjectUI(kitchenObjectSO);
    kitchenObjectSOList.Add(kitchenObjectSO);
    return true;
}

public List<KitchenObjectSO> GetKitchenObjectSOList()
{
   return kitchenObjectSOList;
}
}
