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
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

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