using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject sizzlingVisual;

   
    public void ShowStoveEffect()
    {
        stoveOnVisual.SetActive(true);
        sizzlingVisual.SetActive(true);
    }

   public void HideStoveEffect()
    {
        stoveOnVisual.SetActive(false);
        sizzlingVisual.SetActive(false);
    }

}
