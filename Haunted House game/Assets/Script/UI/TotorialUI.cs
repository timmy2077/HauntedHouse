using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotorialUI : MonoBehaviour
{
    [SerializeField] private GameObject uiParent;
   
   private void Start(){
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Show();
   }
   
    private void GameManager_OnStateChanged(object sender, System.EventArgs e){
        if(GameManager.Instance.IsWaitingToStartState()){
            Show();
        }else{
            Hide();
        }
    }

    void Update()
    {
        
    }

    private void Show(){
        uiParent.SetActive(true);
    }
    private void Hide(){
        uiParent.SetActive(false);
    }
}
