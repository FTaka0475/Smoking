using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puuriusu : MonoBehaviour
{

    /// <summary>
    /// 車画像
    /// </summary>
    [SerializeField, Header("画像")] Sprite Puricar;

    /// <summary>
    /// 車のSpriteRenderer
    /// </summary>
    [SerializeField, Header("画像オブジェクト")] SpriteRenderer Puricar_Ren;
    ///<summary>
    ///車のスピード
    /// <summary>
    [SerializeField, Header("Speed")] float speed = 0.5f;

    ///<summary>
    ///オーディオソース
    /// <summary>
    [SerializeField, Header("AudioSource")] AudioSource Asource;

    ///<summary>
    ///SE
    /// <summary>
    [SerializeField, Header("効果音")] AudioClip AClip;
    /// ぷりぷりenum
    /// </summary>
    //プリウスの状態を示す
    enum PCar
    { 
        Wait,
        Go,
        End
    }
    PCar car_enum = PCar.Wait;
    // Start is called before the first frame update
    void Start()
    {
        Puricar_Ren.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (car_enum)
        {
            case PCar.Wait:
                break;
            case PCar.Go:
                Move();
                break;
            case PCar.End:
                break;

        }

    }


    //車が動く判定<BoxCollider等でやる>
    private void OnTriggerEnter2D(Collider2D col)
    {
           if(col.gameObject.tag == "Player")
            {
                car_enum = PCar.Go;
                Puricar_Ren.sprite = Puricar;
                PSE();
            }
    }

    //車の移動関数
    void Move()
    {
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x - speed, this.gameObject.transform.position.y);
    }
    
    //プリウスの効果音を鳴らす
    void PSE()
    {
        Asource.PlayOneShot(AClip);
    }
}
