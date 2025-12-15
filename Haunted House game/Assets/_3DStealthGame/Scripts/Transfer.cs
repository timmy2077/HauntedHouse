using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Transfer : MonoBehaviour
{
    [SerializeField] private Transform targetPosition; // 传送目标位置
    [SerializeField] private float delay = 0f; // 传送延迟
    [SerializeField] private bool destroyAfterTransfer = false; // 传送后是否销毁传送点
    
    private bool isPlayerInTrigger = false; // 玩家是否在传送区域内
    private GameObject currentPlayer = null; // 当前在传送区域内的玩家

    // 当物体进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是Player标签的物体
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            currentPlayer = other.gameObject;
            Debug.Log("按E键传送");
        }
    }
    
    // 当物体离开触发器时调用
    private void OnTriggerExit(Collider other)
    {
        // 检查是否是Player标签的物体
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            currentPlayer = null;
        }
    }
    
    // 每帧更新
    private void Update()
    {
        // 检查玩家是否在传送区域内且按下了E键
        if (isPlayerInTrigger && currentPlayer != null && Input.GetKeyDown(KeyCode.E))
        {
            // 如果设置了目标位置，执行传送
            if (targetPosition != null)
            {
                // 立即传送或延迟传送
                if (delay <= 0)
                {
                    TransferPlayer(currentPlayer);
                }
                else
                {
                    StartCoroutine(DelayedTransfer(currentPlayer, delay));
                }
            }
        }
    }

    // 传送玩家到目标位置
    private void TransferPlayer(GameObject player)
    {
        player.transform.position = targetPosition.position;
        player.transform.rotation = targetPosition.rotation;
        
        // 如果需要，销毁传送点
        if (destroyAfterTransfer)
        {
            Destroy(gameObject);
        }
        
        // 重置状态
        isPlayerInTrigger = false;
        currentPlayer = null;
    }

    // 延迟传送协程
    private IEnumerator DelayedTransfer(GameObject player, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        TransferPlayer(player);
    }
}
