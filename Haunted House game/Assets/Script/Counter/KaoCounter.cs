using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaoCounter : BaseCounter
{

    [SerializeField] private FryingRecipeSO fryingRecipeList;
    [SerializeField] private FryingRecipeSO burningRecipeList;
    [SerializeField] private StoveCounterVisual stoveCounterVisual;
    [SerializeField] private ProgressBarUI progressBarUI;
   [SerializeField] private ContainCounterVisual containCounterVisual;
    
    public enum StoveState
    {
        Idle,
        Frying,
        Burning
    }

    private FryingRecipe fryingRecipe;
    private float fryingTimer = 0;
    private StoveState state = StoveState.Idle;
   private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

   public void PlayOpen(){
        animator.SetTrigger("OpenClose");
   }



    public override void Interact(Player player)
    {
        if (player.IsHaveKitchenObject())
        {//手上有食材
            if (IsHaveKitchenObject() == false )
            {//当前柜台 为空

                if(fryingRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe fryingRecipe))
                {
                    TransferKitchenObject(player, this);
                    containCounterVisual.PlayOpen();
                    StartFrying(fryingRecipe);
                }else if(burningRecipeList.TryGetFryingRecipe(
                player.GetKitchenObject().GetKitchenObjectSO(), out FryingRecipe burningRecipe))
                {
                    TransferKitchenObject(player, this);
                    containCounterVisual.PlayOpen();
                    StartBurning(burningRecipe);
                }
                else
                {

                }
                
            }
            else
            {//当前柜台 不为空

            }
        }
        else
        {//手上没食材
            if (IsHaveKitchenObject() == false)
            {//当前柜台 为空

            }
            else
            {//当前柜台 不为空
                TurnToIdle();
                containCounterVisual.PlayOpen();
                TransferKitchenObject(this, player);
            }
        }

    }

    private void Update()
    {
        
        switch (state)
        {
            case StoveState.Idle:
                break;
            case StoveState.Frying:
                fryingTimer += Time.deltaTime;
                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);
                
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    
                    DestroyKitchenObject();
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    
                    
                    
                    // 确保有burningRecipeList并且成功获取了厨房对象
                    if (burningRecipeList != null && IsHaveKitchenObject())
                    {
                        var currentKitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();
                       
                        
                        // 尝试获取燃烧配方
                if (burningRecipeList.TryGetFryingRecipe(currentKitchenObjectSO,
                 out FryingRecipe newBurningRecipe))
                {
                    
                    StartBurning(newBurningRecipe);
                }
                else
                {
                   
                    // 临时测试代码：强制进入Burning状态，使用当前fryingRecipe
                    StartBurning(fryingRecipe); // 使用当前烹饪配方作为测试
                }
                    }
                    else
                    {
                        
                        TurnToIdle();
                    }
                }
                break;
            case StoveState.Burning:
              
                if (fryingRecipe == null)
                {
                   
                    TurnToIdle();
                    break;
                }
                
                fryingTimer += Time.deltaTime;
                
                progressBarUI.UpdateProgress(fryingTimer / fryingRecipe.fryingTime);
                
                if (fryingTimer >= fryingRecipe.fryingTime)
                {
                    
                    DestroyKitchenObject();
                    // 使用当前fryingRecipe的输出（烧焦的食物预制体）
                    CreateKitchenObject(fryingRecipe.output.prefab);
                    TurnToIdle();
                }
                
                break;
            default:
                break;
        }
    }
        
    private void StartFrying(FryingRecipe fryingRecipe)
    {
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Frying;
        
        // 显示炉灶视觉效果
        if (stoveCounterVisual != null)
        {
            stoveCounterVisual.ShowStoveEffect();
        }
    }
    private void StartBurning(FryingRecipe fryingRecipe)
    {
        Debug.Log("尝试进入Burning状态，配方: " + (fryingRecipe != null ? fryingRecipe.ToString() : "null"));
        
        if (fryingRecipe == null)
        {
            Debug.LogWarning("无法获取Buring的食谱，无法进行Buring。");
            TurnToIdle();
            return;
        }
        
        fryingTimer = 0;
        this.fryingRecipe = fryingRecipe;
        state = StoveState.Burning;
        Debug.Log("成功进入Burning状态");
        
        // 显示炉灶视觉效果
        if (stoveCounterVisual != null)
        {
            stoveCounterVisual.ShowStoveEffect();
        }
    }
        
    private void TurnToIdle()
    {
        progressBarUI.Hide();
        state = StoveState.Idle;
        
        // 隐藏炉灶视觉效果
        if (stoveCounterVisual != null)
        {
            stoveCounterVisual.HideStoveEffect();
        }
    }

}
