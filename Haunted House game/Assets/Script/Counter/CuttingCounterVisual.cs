using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT="Cut";
   private Animator animator;
   void Start()
   {
       animator=GetComponent<Animator>();
   }
public void PlayCut()
{
    animator.SetTrigger(CUT);
}

}
