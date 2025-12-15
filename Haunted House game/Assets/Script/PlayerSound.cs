using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float stepSoundInterval = 0.5f; // 调整为更合理的脚步声间隔
    private float stepSoundTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        player=GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("PlayerSound: 没有找到Player组件");
        }
    }

    // Update is called once per frame
    void Update()
    {
        stepSoundTimer += Time.deltaTime;
        if (stepSoundTimer > stepSoundInterval)
        {
            stepSoundTimer = 0;
            if(player != null && player.IsWalking){
                float volume = 0.5f;
                SoundManager.Instance.PlayStepSound(volume);
            }
        }
    }
    
}
