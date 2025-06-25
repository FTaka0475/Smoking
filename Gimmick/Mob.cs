using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    /// <summary>
    /// モブのアニメーション変数
    /// </summary>
    [SerializeField,Header("モブのアニメーション")] private Animator mob_animetor;

    /// <summary>
    /// モブのスピード
    /// </summary>
    [SerializeField, Header("モブの移動速度")] private float mob_speed = 0.01f;

    /// <summary>
    /// プレイヤーのyの初期座標に合わせるための変数
    /// </summary>
    [SerializeField, Header("プレイヤーのyの初期座標に合わせるため")] private GameObject player;

    /// <summary>
    /// 水のエフェクト
    /// </summary>
    [SerializeField, Header("水オブジェクト")] private GameObject water;


    /// <summary>
    /// プレイヤー接近フラグ
    /// </summary>
    bool OnTry =false;
    // Start is called before the first frame update
    void Start()
    {
        mob_animetor = GetComponent<Animator>();
        //gameObject.transform.position = new Vector2(gameObject.transform.position.x, player.transform.position.y);
        water.SetActive(false);
    }

    // Update is called once per frame
    

    private void OnTriggerStay2D(Collider2D col)
    {

        /*OnTryがfalseのときアニメーションにmob_speedを設定する*/
        if (!OnTry && !GameManager.InPoos)
        {
            /*アニメーションのパラメータを設定する*/
            mob_animetor.SetFloat("Speed", 0.5f);
            /*ゲームオブジェクトを移動*/
            transform.position = new Vector2(transform.position.x - mob_speed, transform.position.y);
            //Debug.Log("aa");

        }
        
    }


    //モブのアニメーションで呼び出している
    public void Water()
    {
        water.SetActive(true);
        OnTry = true;
    }

}
