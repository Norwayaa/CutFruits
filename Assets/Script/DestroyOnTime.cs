using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTime : MonoBehaviour
{
    //两秒后摧毁物体
    private void Start()
    {
        Invoke("Destroy", 2f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
