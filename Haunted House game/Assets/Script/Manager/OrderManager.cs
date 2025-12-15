using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrderManager : MonoBehaviour
{
public static OrderManager Instance{get;private set;}
public event EventHandler OnRecipeSpawned;
public event EventHandler OnRecipeSuccessed;
public event EventHandler OnRecipeFailed;

  [SerializeField] private RecipeListSO recipeSOList;
private List<RecipeSO> orderRecipeSOList = new List<RecipeSO>();
[SerializeField] private int orderMaxCount = 3;
[SerializeField] private float orderRate = 2f;
private float orderTimer = 0;
private bool isStartOrder = false;
private int orderCount = 0;
private int successDeliveryCount = 0;


private void Awake(){
    Instance = this;
}

private void Start(){
    GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
}

private void GameManager_OnStateChanged(object sender, EventArgs e){
   if(GameManager.Instance.IsGamePlayingState()){
        StartSpawnOrder();
    }
}



private void Update(){
    if(isStartOrder){
        OrderUpdate();
    }
}

private void OrderUpdate(){
    orderTimer += Time.deltaTime;
    if(orderTimer >= orderRate){
        orderTimer = 0;
       OrderANewRecipe();
    }
}
private void OrderANewRecipe(){
    if(orderCount >= orderMaxCount){
        return;
    }
    orderCount++;
int index = UnityEngine.Random.Range(0, recipeSOList.RecipeSOList.Count);
orderRecipeSOList.Add(recipeSOList.RecipeSOList[index]);
OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
}

// 添加DeliveryOrder方法以匹配DeliveryCounter的调用
public void DeliveryOrder(PlateKitchenObject plateKitchenObject){
    DeliveryRecipe(plateKitchenObject);
}

public void DeliveryRecipe(PlateKitchenObject plateKitchenObject){
    
    RecipeSO correctRecipe = null;
    foreach(RecipeSO recipe in orderRecipeSOList){
        if(IsCorrect(recipe,plateKitchenObject)){
            correctRecipe = recipe;
            break;
        }
    }

    if(correctRecipe == null){
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
       print("上菜失败");
    }
    else{
        orderRecipeSOList.Remove(correctRecipe);
        orderCount--; // 订单匹配成功后减少订单计数
        OnRecipeSuccessed?.Invoke(this, EventArgs.Empty);
        print("上菜成功");
        successDeliveryCount++;
        // 生成新订单以保持订单数量
        OrderUpdate();
    }
}   
 

private bool IsCorrect(RecipeSO recipe, PlateKitchenObject plateKitchenObject){
    List<KitchenObjectSO> list1 = recipe.KitchenObjectSOList; // 修正字段名大小写
    List<KitchenObjectSO> list2 = plateKitchenObject.GetKitchenObjectSOList();
    if(list1.Count != list2.Count){
        return false;
    }
    foreach(KitchenObjectSO kitchenObjectSO in list1){
        if(!list2.Contains(kitchenObjectSO)){
            return false;
        }
    }
    return true;
}

public List<RecipeSO> GetOrderList(){
    return orderRecipeSOList;
}



public void StartSpawnOrder(){
    isStartOrder = true;
}

public int GetSuccessDeliveryCount(){
    return successDeliveryCount;
}
}
