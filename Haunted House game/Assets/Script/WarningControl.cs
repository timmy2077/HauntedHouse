using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningControl : MonoBehaviour
{
    private const string IS_WARNING = "isburn";
    [SerializeField] private GameObject warningUI;
    [SerializeField] private Animator progressBarAnimator;
    private bool isWarning = false;
private float warningSoundRate = 0.3f;
private float warningSoundTimer = 0;

void Update()
{
    if(isWarning){
        warningSoundTimer+=Time.deltaTime;
        if(warningSoundTimer>warningSoundRate){
            warningSoundTimer=0;
           SoundManager.Instance.PlayWarningSound();
        }
    }
}

    public void ShowWarning()
    {
        if(isWarning==false){
            isWarning = true;
            warningUI.SetActive(true);
            progressBarAnimator.SetBool(IS_WARNING, true);
        }
       
    }
    public void StopWarning()
    {
        isWarning = false;
        warningUI.SetActive(false);
        progressBarAnimator.SetBool(IS_WARNING, false);
    }
}
