using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fruitScore : MonoBehaviour
{
    public Text fruitScoreTxt;
    private void Start()
    {
        fruitScoreTxt.text = "得分："+UIScore.Instance.fruitScore.ToString();
    }

}
