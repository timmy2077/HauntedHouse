using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrash;
   public override void Interact(Player player)
    {
       if(player.IsHaveKitchenObject())
       {
           player.DestroyKitchenObject();
           OnTrash?.Invoke(this, EventArgs.Empty);
       }
    }

    public new static void ClearStaticData(){
        OnTrash = null;
    }
}
