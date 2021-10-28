using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //水果数组
    public GameObject[] Fruits;
    //炸弹
    public GameObject Bomb;

    //生成水果间隔时间
    float spawnTime = 3f;
    //开始时间
    float startTime = 0f;

    //水果的z轴值，避免发生碰撞
    private int tmpZ = -2;

    //生成水果音效
    public AudioSource fruit_launch;

    private void onSpawn(bool isfruit)
    {   
        //随机生成x坐标
        float x = Random.Range(-8f, 8f);
        //y坐标
        float y = transform.position.y;
        //z坐标
        float z = tmpZ;
        tmpZ -= 2;
        //重置z轴坐标
        if (z <= -14)
        {
            tmpZ = -2;
        }

        GameObject go;
        //判断生成的是否是水果
        if (isfruit)
        {
            //随机生成水果的品种
            int fruitsIndex = Random.Range(0, Fruits.Length);
            fruit_launch.Play();
            go = Instantiate<GameObject>(Fruits[fruitsIndex], new Vector3(x, y,z), Random.rotation);
            
        }
        else
        {
            //生成炸弹
            go = Instantiate<GameObject>(Bomb, new Vector3(x, y, z), Random.rotation);
        }

        //随机生成一个力向量
        Vector3 velocity = new Vector3(-x * Random.Range(0.5f, 1.0f), -Physics.gravity.y * Random.Range(1.2f, 1.5f), 0);

        Rigidbody rigidbody1 = go.GetComponent<Rigidbody>();
        //对游戏物体施加这个力（发射水果）
        rigidbody1.velocity = velocity;
    }

    //碰撞检测
    private void OnCollisionEnter(Collision collision)
    {
        //摧毁游戏物体
        Destroy(collision.gameObject);
    }


    void Update()
    {
        //开始时间
        startTime += Time.deltaTime;
        //每3秒生成一次水果
        if (startTime >= spawnTime)
        {
            //随机生成水果的个数
            int a = Random.Range(1, 5);
            for (int i=0; i<a; i++)
            {
                onSpawn(true);
            }
            //随机生成炸弹
            int bombNum = Random.Range(0, 100);
            //30%的概率
            if(bombNum<30)
            {
                onSpawn(false);
            }
            //重置时间
            startTime = 0f;
        }
    }
}
