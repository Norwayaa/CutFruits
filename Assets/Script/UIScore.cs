using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScore : MonoBehaviour
{
    //单例对象
    public static UIScore _instance = null;
    public static UIScore Instance { get { return _instance; } }
    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    

    //水果得分UI
    public Text fruitScoreTxt;
    //成绩
    public int fruitScore = 0;

    //炸弹个数UI
    public Text bombScoreTxt;
    //炸弹个数
    public int bombScore = 0;

    //加分函数
    public void AddScore()
    {
        this.fruitScore ++;
        fruitScoreTxt.text = "X "+this.fruitScore.ToString();
    }

    public void bomb()
    {
        bombScore++;
        bombScoreTxt.text = "X " + bombScore.ToString();
        if (bombScore == 3)
        {

            Invoke("skipScene", 1.5f);
        }
    }

    public void skipScene()
    {
        SceneManager.LoadScene("Over", LoadSceneMode.Single);
    }
}