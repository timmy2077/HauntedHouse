using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryResultUI : MonoBehaviour
{
    private const string IS_SHOW = "IsShow";
    [SerializeField] private Animator successUI;
    [SerializeField] private Animator failUI;
    [SerializeField] private GameObject spawnPrefab; // 要实例化的物体
    [SerializeField] private Transform spawnPosition; // 实例化的位置
    // Start is called before the first frame update
    void Start()
    {
       OrderManager.Instance.OnRecipeSuccessed += OrderManager_OnRecipeSuccessed;
       OrderManager.Instance.OnRecipeFailed += OrderManager_OnRecipeFailed;
    }

    private void OrderManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        failUI.gameObject.SetActive(true);
        failUI.SetTrigger(IS_SHOW);
    }

    private void OrderManager_OnRecipeSuccessed(object sender, System.EventArgs e)
    {
       successUI.gameObject.SetActive(true);
       successUI.SetTrigger(IS_SHOW);
       
       // 在指定位置实例化指定物体
       if (spawnPrefab != null && spawnPosition != null)
       {
           Instantiate(spawnPrefab, spawnPosition.position, spawnPosition.rotation);
       }
    }

   
}
