using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeUI : MonoBehaviour
{
    // 引用UI元素，例如食谱名称、图片等
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform kitchenObjectParent;
    [SerializeField] private Image iconUITemplate;
  
  
  private void Start(){
    iconUITemplate.gameObject.SetActive(false);
  }

  public void UpdateUI(RecipeSO recipeSO)
  {
    recipeNameText.text = recipeSO.recipleName;
    foreach(KitchenObjectSO kitchenObjectSO in recipeSO.KitchenObjectSOList){
       Image newIcon = GameObject.Instantiate(iconUITemplate);
       newIcon.transform.SetParent(kitchenObjectParent);
       newIcon.gameObject.SetActive(true);
       newIcon.sprite = kitchenObjectSO.sprite;
    }
  }
}
