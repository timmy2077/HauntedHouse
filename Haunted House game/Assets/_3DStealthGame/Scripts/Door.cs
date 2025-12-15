using System;
using UnityEngine;

namespace StealthGame
{
    public class Door : MonoBehaviour
    {
        public string KeyName = "key1";
        public Animator m_Animator;
        private bool m_IsOpen = false;
    
        void Start()
        {
           
        }
    
        private void OnCollisionEnter(Collision other)
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player == null)
                return;

            if (player.OwnKey(KeyName) && !m_IsOpen)
            {
                m_Animator.SetTrigger("Open");
                m_IsOpen = true;
            }
        }
    }
}