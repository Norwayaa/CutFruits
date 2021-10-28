using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour
{
    private static ObjectControl _instance=null;
    public static ObjectControl Instance { get { return _instance; } }

    //切碎后的水果
    public GameObject halfFruits;
    //水果切碎的特效
    public GameObject Splash;
    public GameObject SplashFlat;

    //爆炸特效
    public ParticleSystem boomParticle;

    //被切标志位
    public bool dead=false;

    //音效
    public AudioClip sound;

    //是否发生爆炸
    public bool isBoom=false;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }
    /// <summary>
    /// 被切后调用
    /// </summary>
    public void OnCut()
    {
        //if (dead == true)
        //{
        //    return;
        //}
        //判断切到的是否是炸弹
        if (gameObject.name=="Bomb"|| gameObject.name == "Bomb(Clone)")
        {
            isBoom = true;
            for (int i = 0; i < 3; i++)
            {
                //播放特效
                Instantiate(boomParticle, transform.position+ new Vector3((int)Random.Range(-2,2), (int)Random.Range(-2, 2), (int)Random.Range(-2, 2)), Quaternion.identity);
                //将周围所有水果摧毁
                GameObject[] fruits = GameObject.FindGameObjectsWithTag("Fruit");
                foreach (var item in fruits)
                {
                    Destroy(item);
                }
            }
            //计算已切炸弹个数
            UIScore.Instance.bomb();
        }
        else//切到水果
        {
            //生成两瓣水果
            for(int i=0;i<2;i++)
            {
                //生成的碎水果，随机旋转
                GameObject go= Instantiate<GameObject>(halfFruits,transform.position,Random.rotation);
                //添加一个随机的力
                go.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * 5, ForceMode.Impulse);
            }
            //水果特效
            Instantiate(Splash, transform.position, Quaternion.identity);
            Instantiate(SplashFlat, transform.position, Quaternion.identity);

            //计算得分
            UIScore.Instance.AddScore();
        }
        //播放被切的音效
        AudioSource.PlayClipAtPoint(sound, transform.position);

        //销毁当前物体
        Destroy(gameObject);

        //防止重复调用
        //dead = true;

    }
}
