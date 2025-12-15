using UnityEngine;
using UnityEngine.Video;

public class Animation : MonoBehaviour
{
    [Header("触发设置")]
    [SerializeField] private GameObject targetObject; // 要显示/隐藏的特定物体（视频）
    [SerializeField] private float showDuration = 3f; // 显示的持续时间（秒）
    [SerializeField] private bool autoPlayVideo = true; // 是否自动播放视频
    [SerializeField] private bool loopVideo = false; // 是否循环播放视频
    
    private bool hasTriggered = false; // 触发器是否已经被触发
    private float timer = 0f; // 计时器
    private bool isShowing = false; // 是否正在显示特定物体
    private VideoPlayer videoPlayer; // 特定物体的VideoPlayer组件
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 初始化：隐藏特定物体
        if (targetObject != null)
        {
            targetObject.SetActive(false);
            videoPlayer = targetObject.GetComponent<VideoPlayer>();
            
            // 配置VideoPlayer
            if (videoPlayer != null)
            {
                videoPlayer.playOnAwake = false; // 禁用自动播放，由脚本控制
                videoPlayer.isLooping = loopVideo;
            }
        }
        else
        {
            Debug.LogError("Animation: 未指定要显示/隐藏的特定物体");
        }
    }
    
    // 当玩家进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是Player，且触发器未被触发过
        if (other.CompareTag("Player") && !hasTriggered)
        {
            TriggerEvent();
        }
    }
    
    // 触发事件处理
    private void TriggerEvent()
    {
        hasTriggered = true; // 标记为已触发
        
        // 显示特定物体
        if (targetObject != null)
        {
            targetObject.SetActive(true);
            isShowing = true;
            timer = 0f;
            
            // 播放视频
            if (autoPlayVideo && videoPlayer != null)
            {
                videoPlayer.Play();
            }
            
            // 暂停游戏
            Time.timeScale = 0f;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (isShowing)
        {
            // 使用不受时间刻度影响的时间来更新计时器
            timer += Time.unscaledDeltaTime;
            
            // 检查是否按下E键跳过视频
            if (Input.GetKeyDown(KeyCode.E))
            {
                SkipVideo();
            }
            
            // 检查是否达到显示时间
            if (timer >= showDuration)
            {
                // 停止视频播放
                if (videoPlayer != null && videoPlayer.isPlaying)
                {
                    videoPlayer.Stop();
                }
                
                // 隐藏特定物体
                if (targetObject != null)
                {
                    targetObject.SetActive(false);
                }
                
                // 恢复游戏
                Time.timeScale = 1f;
                isShowing = false;
            }
        }
    }
    
    // 跳过视频播放
    private void SkipVideo()
    {
        // 停止视频播放
        if (videoPlayer != null && videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }
        
        // 隐藏特定物体
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
        
        // 恢复游戏
        Time.timeScale = 1f;
        isShowing = false;
    }
    
    // 绘制触发器范围 gizmo
    private void OnDrawGizmosSelected()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        if (boxCollider != null && boxCollider.isTrigger)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
        }
    }
}
