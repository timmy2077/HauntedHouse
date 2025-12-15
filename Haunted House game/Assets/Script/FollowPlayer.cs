using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Header("跟随参数")]
    [SerializeField] private float followSpeed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float triggerRange = 5f;
    
    private Rigidbody rb;
    private Animator[] childAnimators;
    private GameObject player;
    private bool isFollowing = false;
    private bool isWalking = false;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("FollowPlayer: 没有找到Rigidbody组件");
        }
        
        // 获取所有子物体的Animator组件
        GetChildAnimators();
    }
    
    private void GetChildAnimators()
    {
        // 获取所有子物体，包括嵌套子物体
        Animator[] allAnimators = GetComponentsInChildren<Animator>();
        
        // 过滤掉自身的Animator（如果有的话），只保留子物体的
        List<Animator> childAnimatorList = new List<Animator>();
        foreach (Animator animator in allAnimators)
        {
            if (animator.transform != transform)
            {
                childAnimatorList.Add(animator);
            }
        }
        
        childAnimators = childAnimatorList.ToArray();
        
        if (childAnimators.Length == 0)
        {
            Debug.LogWarning("FollowPlayer: 没有找到子物体的Animator组件");
        }
    }
    
    private void Update()
    {
        if (isFollowing && player != null)
        {
            // 平滑旋转朝向玩家
            Vector3 direction = player.transform.position - transform.position;
            direction.y = 0; // 只在水平面上旋转
            
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            
            // 计算是否在移动
            isWalking = rb.linearVelocity.magnitude > 0.1f;
            
            // 更新所有子物体的动画
            UpdateChildAnimations();
        }
        else
        {
            isWalking = false;
            UpdateChildAnimations();
        }
    }
    
    private void FixedUpdate()
    {
        if (isFollowing && player != null)
        {
            // 平滑移动向玩家
            Vector3 targetPosition = player.transform.position;
            targetPosition.y = transform.position.y; // 保持自身Y轴位置
            
            Vector3 moveDirection = targetPosition - transform.position;
            float distance = moveDirection.magnitude;
            
            if (distance > 0.5f) // 保持一定距离，避免过于靠近
            {
                Vector3 normalizedMoveDir = moveDirection.normalized;
                rb.MovePosition(transform.position + normalizedMoveDir * followSpeed * Time.fixedDeltaTime);
            }
        }
    }
    
    private void UpdateChildAnimations()
    {
        foreach (Animator animator in childAnimators)
        {
            if (animator != null)
            {
                animator.SetBool("IsWalking", isWalking);
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            isFollowing = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = null;
            isFollowing = false;
        }
    }
    
    // 绘制触发器范围 gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, triggerRange);
    }
}
