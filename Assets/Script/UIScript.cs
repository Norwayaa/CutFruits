using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    //开始游戏按钮
    public Button btnPlay;
    //音频按钮
    public Button btn_StopSound;
    public Button btn_PlaySound;
    //音频组件
    public AudioSource bgSound;

  /*  public void getComponent()
    {
        btnPlay.transform.Find("btnplay").GetComponent<Button>();
        btnSound.transform.Find("btnsound").GetComponent<Button>();
        bgSound.transform.Find("btnsound").GetComponent<AudioSource>();
        imgSound.transform.Find("btnsound").GetComponent<Image>();
    }
    */

    //加载场景
    public void OnplayClick()
    {
        SceneManager.LoadScene("play");
    }

    void OnStopSoundClick()
    {
        //暂停音乐
        bgSound.Pause();
        //显示播放音乐图标
        btn_StopSound.gameObject.SetActive(false);
        btn_PlaySound.gameObject.SetActive(true);
    }

    void OnPlaySoundClick()
    {
        bgSound.Play();
        //显示暂停音乐图标
        btn_StopSound.gameObject.SetActive(true);
        //btn_PlaySound.gameObject.SetActive(false);
    }

    void Start()
    {
        // 获取组件
        // getComponent();

        //为button设置监听器
        btnPlay.onClick.AddListener(OnplayClick);
        btn_StopSound.onClick.AddListener(OnStopSoundClick);
        btn_PlaySound.onClick.AddListener(OnPlaySoundClick);

    }

    private void OnDestroy()
    {
        //当当前界面被销毁时 移除监听器
        btnPlay.onClick.RemoveListener(OnplayClick);
        btn_StopSound.onClick.RemoveListener(OnStopSoundClick);
        btn_PlaySound.onClick.RemoveListener(OnPlaySoundClick);

    }

 
}
