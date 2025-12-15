using UnityEngine;
using System.Collections;

public class Show : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow1; // 要显示的第一个物体
    [SerializeField] private GameObject objectToShow2; // 要显示的第二个物体
    [SerializeField] private GameObject objectToDisappear; // 要显示后消失的物体
    [SerializeField] private AudioSource audioSource; // 音效源（挂载此脚本物体上的音效）
    [SerializeField] private bool hideOnExit = false; // 离开触发器时是否隐藏物体
    [SerializeField] private bool showOnce = true; // 是否只显示一次
    [SerializeField] private float disappearDelay = 2f; // 消失延迟时间（秒）
    
    // 记录初始状态
    private bool initialState1;
    private bool initialState2;
    private bool initialStateDisappear;
    
    // 状态跟踪
    private bool isPlayerInTrigger = false;
    private bool hasShown = false; // 是否已显示过

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 保存初始状态
        if (objectToShow1 != null)
        {
            initialState1 = objectToShow1.activeSelf;
            // 默认隐藏
            objectToShow1.SetActive(false);
        }
        
        if (objectToShow2 != null)
        {
            initialState2 = objectToShow2.activeSelf;
            // 默认隐藏
            objectToShow2.SetActive(false);
        }
        
        
        
        // 如果没有指定音效源，尝试从当前物体获取
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }
    
    // 当物体进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        // 检查是否是Player标签的物体
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            Debug.Log("按E键显示物体");
        }
    }
    
    // 当物体离开触发器时调用
    private void OnTriggerExit(Collider other)
    {
        // 检查是否是Player标签的物体
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            
            // 如果设置了离开时隐藏，且已经显示过，则隐藏物体
            if (hideOnExit && hasShown)
            {
                HideObjects();
            }
        }
    }
    
    // 每帧更新
    private void Update()
    {
        // 检查玩家是否在触发器内，是否按下了E键，且尚未显示过（如果设置了只显示一次）
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && (!showOnce || !hasShown))
        {
            ShowObjects();
        }
    }
    
    // 显示物体
    private void ShowObjects()
    {
        // 显示第一个物体
        if (objectToShow1 != null)
        {
            objectToShow1.SetActive(true);
        }
        
        // 显示第二个物体
        if (objectToShow2 != null)
        {
            objectToShow2.SetActive(true);
        }
        
        // 显示要消失的物体
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(true);
            // 启动消失协程
            StartCoroutine(DisappearAfterDelay(disappearDelay));
        }
        
        hasShown = true;
        Debug.Log("显示物体");
    }
    
    // 隐藏物体
    private void HideObjects()
    {
        // 隐藏第一个物体
        if (objectToShow1 != null)
        {
            objectToShow1.SetActive(false);
        }
        
        // 隐藏第二个物体
        if (objectToShow2 != null)
        {
            objectToShow2.SetActive(false);
        }
        
        // 隐藏要消失的物体
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
        }
        
        Debug.Log("隐藏物体");
    }
    
    // 延迟消失协程
    private IEnumerator DisappearAfterDelay(float delay)
    {
        // 等待指定时间
        yield return new WaitForSeconds(delay);
        
        // 隐藏物体
        if (objectToDisappear != null)
        {
            objectToDisappear.SetActive(false);
        }
        
        // 播放音效
        if (audioSource != null)
        {
            audioSource.Play();
        }
        
        Debug.Log($"物体已消失，播放音效");
    }
}
