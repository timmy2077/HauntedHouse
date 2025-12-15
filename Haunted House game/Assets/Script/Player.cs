using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制器类 - 处理玩家的移动和交互
/// </summary>
public class Player : KitchenObjectHolder
{
public static Player Instance { get; private set; }


private List<string> m_OwnedKeys = new List<string>();

    #region 公共变量
    /// <summary>
    /// 玩家移动速度
    /// </summary>
    public float Speed = 6f;
    
    /// <summary>
    /// 玩家旋转速度
    /// </summary>
    public float rotateSpeed = 10f;
    
    /// <summary>
    /// 玩家是否正在行走的标志
    /// </summary>
    public bool IsWalking = false;
    
    /// <summary>
    /// 游戏输入管理器引用
    /// </summary>
    public GameInput gameInput;
    
    /// <summary>
    /// 交互射线的长度
    /// </summary>
    public float RayLength = 2f;
    
    /// <summary>
    /// 交互射线检测的层掩码
    /// </summary>
    public LayerMask counterLayerMask;
    private BaseCounter selectedCounter;
    #endregion

    /// <summary>
    /// 玩家动画控制器引用
    /// </summary>
    Animator m_Animator;
    
    /// <summary>
    /// 玩家刚体组件引用
    /// </summary>
    Rigidbody m_Rigidbody;

    #region 生命周期方法
    /// <summary>
    /// 初始化玩家组件
    /// </summary>

   AudioSource m_AudioSource;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        // 订阅游戏输入管理器的交互事件
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnOperateAction += GameInput_OnOperateAction;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
        // 检查PlayerSound组件是否存在
        if (GetComponent<PlayerSound>() == null)
        {
            Debug.LogWarning("Player: 没有找到PlayerSound组件，脚步声将不会播放");
            // 自动添加PlayerSound组件
            gameObject.AddComponent<PlayerSound>();
        }
    }

    
    /// <summary>
    /// 每帧更新
    /// </summary>
    private void Update()
    {
        HandleInteraction();
    }

    /// <summary>
    /// 固定时间间隔更新（用于物理相关计算）
    /// </summary>
    private void FixedUpdate()
    {
        // 处理玩家移动
        HandleMovement();
    }
    #endregion

    #region 事件处理方法
    /// <summary>
    /// 处理游戏输入管理器的交互事件
    /// </summary>
    /// <param name="sender">事件发送者</param>
    /// <param name="e">事件参数</param>
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
       
                selectedCounter?.Interact(this);
               
    }
    #endregion


private void GameInput_OnOperateAction(object sender, System.EventArgs e)
    {
        selectedCounter?.InteractOperate(this);
    }



    #region 功能方法
    /// <summary>
    /// 处理玩家移动逻辑 - 第三人称视角
    /// </summary>
    private void HandleMovement()
    {
        // 获取移动方向输入
        Vector3 inputDir = gameInput.GetMovement();
        
        // 获取相机的水平朝向（忽略Y轴，保持水平移动）
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();
        
        // 根据相机朝向计算实际移动方向
        Vector3 moveDir = cameraForward * inputDir.z + cameraRight * inputDir.x;
        
        // 更新行走状态
        IsWalking = moveDir.magnitude > 0.1f; // 使用一个小的阈值来判断是否有输入
        
        if (IsWalking)
        {
            moveDir.Normalize();
        }
        
        // 设置动画参数
        m_Animator.SetBool("IsWalking", IsWalking);
        
        if (IsWalking)
        {
            // 计算期望的前进方向
            Vector3 desiredForward = Vector3.RotateTowards(transform.forward, moveDir, rotateSpeed * Time.deltaTime, 0f);
            Quaternion rotation = Quaternion.LookRotation(desiredForward);
            
            // 使用刚体进行旋转和移动
            m_Rigidbody.MoveRotation(rotation);
            m_Rigidbody.MovePosition(m_Rigidbody.position + moveDir * Speed * Time.deltaTime);
        
        if (!m_AudioSource.isPlaying)
                {
                    m_AudioSource.Play();
                }
        }else
            {
                m_AudioSource.Stop();
            }

    
    }

    /// <summary>
    /// 处理玩家与环境的交互逻辑
    /// </summary>
    private void HandleInteraction()
    {
        // 从玩家位置向前发射一条射线
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, RayLength, counterLayerMask))
        {
            // 尝试获取射线击中物体上的BaseCounter组件
            if (hitInfo.transform.TryGetComponent<BaseCounter>(out BaseCounter counter))
            {
                // 如果成功获取组件，则设置选中状态，但不直接交互
                SetSelectedCounter(counter);
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else{
            SetSelectedCounter(null);
        }
    }

    public void SetSelectedCounter(BaseCounter counter){
        if(counter!=selectedCounter){
            // 只有当当前选中的柜台不为空时才取消选中
            selectedCounter?.CancelSelect();
            counter?.SelectCounter();
            this.selectedCounter = counter;
        }
       
    }
    #endregion

     public void AddKey(string keyName)
        {
            m_OwnedKeys.Add(keyName);
        }

        public bool OwnKey(string keyName)
        {
            return m_OwnedKeys.Contains(keyName);
        }
}
