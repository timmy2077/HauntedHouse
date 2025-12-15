using System;
using UnityEngine;

namespace StealthGame
{
    public class Instestory : MonoBehaviour
    {
        public string KeyName = "key1";
        
       
    
        void Start()
        {
           
        }
    
        private void OnCollisionEnter(Collision other)
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player == null)
                return;

            if (player.OwnKey(KeyName))
            {
                Destroy(gameObject);
            }
        }
    }
}