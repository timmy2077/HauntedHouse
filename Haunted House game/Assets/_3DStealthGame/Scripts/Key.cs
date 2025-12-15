using System;
using UnityEngine;

namespace StealthGame
{
    public class Key : MonoBehaviour
    {
        public string KeyName = "key1";
        [SerializeField] private AudioSource audioSource; // 音效源（挂载在此脚本物体上）
        
        private bool isPlayerInTrigger = false;
        private GameObject currentPlayer = null;
        private Player currentPlayerComponent = null;
        private PlayerMovement currentPlayerMovementComponent = null;

        private void Start()
        {
            // 如果没有指定音效源，尝试从当前物体获取
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            // 尝试获取Player组件
            var player = other.gameObject.GetComponent<Player>();
            
            // 如果找到Player组件，检查是否有AddKey方法
            if (player != null)
            {
                var addKeyMethod = player.GetType().GetMethod("AddKey");
                if (addKeyMethod != null)
                {
                    isPlayerInTrigger = true;
                    currentPlayer = other.gameObject;
                    currentPlayerComponent = player;
                    Debug.Log("按E键拾取钥匙");
                    return;
                }
            }
            
            // 尝试获取PlayerMovement组件（兼容旧版本）
            PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                isPlayerInTrigger = true;
                currentPlayer = other.gameObject;
                currentPlayerMovementComponent = playerMovement;
                Debug.Log("按E键拾取钥匙");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            // 检查是否是当前玩家离开
            if (other.gameObject == currentPlayer)
            {
                isPlayerInTrigger = false;
                currentPlayer = null;
                currentPlayerComponent = null;
                currentPlayerMovementComponent = null;
            }
        }
        
        private void Update()
        {
            // 检查玩家是否在触发区域内且按下了E键
            if (isPlayerInTrigger && currentPlayer != null && Input.GetKeyDown(KeyCode.E))
            {
                PickUpKey();
            }
        }
        
        private void PickUpKey()
        {
            // 尝试使用Player组件拾取
            if (currentPlayerComponent != null)
            {
                var addKeyMethod = currentPlayerComponent.GetType().GetMethod("AddKey");
                if (addKeyMethod != null)
                {
                    addKeyMethod.Invoke(currentPlayerComponent, new object[] { KeyName });
                    PlayPickupSound();
                    Destroy(gameObject);
                    return;
                }
            }
            
            // 尝试使用PlayerMovement组件拾取（兼容旧版本）
            if (currentPlayerMovementComponent != null)
            {
                currentPlayerMovementComponent.AddKey(KeyName);
                PlayPickupSound();
                Destroy(gameObject);
                return;
            }
        }
        
        // 播放拾取音效
        private void PlayPickupSound()
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
