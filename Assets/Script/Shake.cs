using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    private float cameraShake = 2;//震动系数


    // Update is called once per frame
    void Update()
    {
        if (ObjectControl.Instance.isBoom)
        {

            //X,Y轴震动
            transform.position = new Vector3((Random.Range(0f, cameraShake)) - cameraShake * 0.5f, transform.position.y, transform.position.z);

            cameraShake = cameraShake / 1.05f;
            if (cameraShake < 0.05f)
            {
                cameraShake = 0;
                ObjectControl.Instance.isBoom = false;
            }
        }
        else
        {
            cameraShake = 5;
        }
    }
}
