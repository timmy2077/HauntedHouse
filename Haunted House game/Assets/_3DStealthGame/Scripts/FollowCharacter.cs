using UnityEngine;

public class FollowCharacter : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f; // 跟随速度
    [SerializeField] private float rotationSpeed = 5f; // 旋转速度
    [SerializeField] private float followDistance = 1f; // 保持的跟随距离
    [SerializeField] private Animator childAnimator; // 子物体的动画组件
    
    private bool isFollowing = false; // 是否正在跟随
    private bool isWalking = false; // 是否正在行走
    private GameObject playerToFollow = null; // 要跟随的玩家
    private Rigidbody rb; // 刚体组件

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 获取刚体组件
        rb = GetComponent<Rigidbody>();
        
        // 如果没有指定子物体动画组件，尝试从子物体中查找
        if (childAnimator == null)
        {
            childAnimator = GetComponentInChildren<Animator>();
        }
    }
    
    // 当物体进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是Player标签的物体
        if (other.CompareTag("Player"))
        {
            isFollowing = true;
            playerToFollow = other.gameObject;
            Debug.Log("开始跟随玩家");
        }
    }
    
    // 当物体离开触发器时调用
    private void OnTriggerExit(Collider other)
    {
        // 检查是否是当前跟随的玩家
        if (other.gameObject == playerToFollow)
        {
            isFollowing = false;
            playerToFollow = null;
            isWalking = false;
            UpdateAnimation();
            Debug.Log("停止跟随玩家");
        }
    }

    // 使用FixedUpdate处理物理相关逻辑
    void FixedUpdate()
    {
        // 默认为非行走状态
        bool wasWalking = isWalking;
        isWalking = false;
        
        // 如果正在跟随玩家
        if (isFollowing && playerToFollow != null)
        {
            // 计算目标方向
            Vector3 targetDirection = playerToFollow.transform.position - transform.position;
            targetDirection.y = 0; // 保持在同一水平面上
            
            // 计算距离
            float distanceToPlayer = targetDirection.magnitude;
            
            // 如果距离大于跟随距离，开始移动
            if (distanceToPlayer > followDistance)
            {
                isWalking = true;
                
                // 平滑旋转面向玩家
                if (targetDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                    rb.MoveRotation(newRotation);
                }
                
                // 使用刚体移动，保持物理交互
                Vector3 moveDirection = rb.transform.forward * followSpeed * Time.fixedDeltaTime;
                rb.MovePosition(rb.position + moveDirection);
            }
        }
        
        // 如果行走状态发生变化，更新动画
        if (wasWalking != isWalking)
        {
            UpdateAnimation();
        }
    }
    
    // 更新子物体动画
    private void UpdateAnimation()
    {
        if (childAnimator != null)
        {
            childAnimator.SetBool("IsWalking", isWalking);
        }
    }
}
