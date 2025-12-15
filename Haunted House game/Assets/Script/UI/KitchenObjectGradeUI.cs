using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObjectGradeUI : MonoBehaviour
{
   [SerializeField] private KitchenObjectIconUI iconTemplateUI;
   

   private void Start()
   {
    iconTemplateUI.Hide();
   }
   public void ShowKitchenObjectUI(KitchenObjectSO kitchenObjectSO)
   {
  
   KitchenObjectIconUI newIconUI =GameObject.Instantiate(iconTemplateUI,transform);
  newIconUI.transform.SetParent(transform);
  newIconUI.Show(kitchenObjectSO.sprite);
   }
}
