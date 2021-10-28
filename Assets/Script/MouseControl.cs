using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    //LineRenderer组件
    public LineRenderer lineRenderer;

    public AudioSource sound;

    private bool firstMouseDown = false;

    private bool mouseDown = false;

   

     void Update()
    {
        //当鼠标左键按下
        if(Input.GetMouseButtonDown (0))
        {
            firstMouseDown = true;
            mouseDown = true;
            sound.Play();
        }

        //当鼠标左键抬起
        if(Input.GetMouseButtonUp (0))
        {
            mouseDown = false;
        }
        //画线
        onDrawLine();

        firstMouseDown = false;
    }

    //保存LineRenderer坐标
    private Vector3[] positions = new Vector3[10];
    //当前保存坐标数量
    private int count = 0;

    //代表按下鼠标后第一帧鼠标位置
    private Vector3 head;
    //代表松上一帧鼠标位置
    private Vector3 last;

    /// <summary>
    /// 在场景中画线
    /// </summary>
    private void onDrawLine()
    {
        //第一次按下鼠标
        if(firstMouseDown)
        {
            count = 0;
            //屏幕坐标转换成世界坐标
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //第一帧头和上一帧坐标一样
            last = head;
        }

        //鼠标一直按住
        if(mouseDown)
        {
            //修改头坐标
            head = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //鼠标发生移动
            if(Vector3.Distance(head,last)>0.01f)
            {
                //保存到数组中
                savePosition(head);

                count++;

                onRayCast(head);
            }
            //将head赋值给last
            last = head;
        }
        else//当鼠标松开
        {
            //重新将坐标数组置空
            positions = new Vector3[10];
        }
        changePositions(positions);
    }

    
    private void savePosition(Vector3 pos)
    {
        //鼠标的z坐标和相机的z坐标一致，所以要置为0
        pos.z = 0;
        //数组中存储的点小于等于9个
        if(count <= 9)
        {
            //每增加一个点，都需要将后面所有的点都设置为一样的点，否则效果不理想
            for(int i= count; i<10;i++)
            {
                positions[i] = pos;
            }
        }
        else//数组中存满了10个点
        {
            //删除第一个元素
            for (int i=0;i<9;i++)
            {
                positions[i] = positions[i + 1];
            }
            //将新的值作为最后一个元素插入数组中
            positions[9] = pos;

        }
    }

    /// <summary>
    /// 射线检测
    /// </summary>
    /// <param name="position"></param>
    private void onRayCast(Vector3 position)
    {
        //世界坐标转换为屏幕坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        //向指定坐标发射射线
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        //水果有可能发生重叠，所以使用RayCastAll
        RaycastHit[] hits = Physics.RaycastAll(ray);
        for(int i=0;i<hits.Length;i++)
        {
            // Debug.Log(hits[i].collider.gameObject.name);
            // Destroy(hits[i].collider.gameObject);

            //调用所有发生碰撞的物体身上的OnCut方法，第二个参数为 没有接收者也不会报错
            hits[i].collider.gameObject.SendMessage("OnCut", SendMessageOptions.DontRequireReceiver);
        }
    }

    /// <summary>
    /// 修改直线渲染器的坐标
    /// </summary>
    /// <param name="positions"></param>
    private void changePositions(Vector3[] positions)
    {
        lineRenderer.SetPositions(positions);
    }
}