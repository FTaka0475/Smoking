using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_move : MonoBehaviour
{
    /// <summary>
    /// モブのアニメーション変数
    /// </summary>
    [SerializeField, Header("モブのアニメーション")] private Animator mob_animetor;

    /// <summary>
    /// プレイヤー接近フラグ
    /// </summary>
    bool OnTry = false;
    // Start is called before the first frame update

    ///<summary>
    ///モブ
    ///</summary>
    [SerializeField, Header("モブオブジェクト")] GameObject mobobject;

    /// <summary>振り返るかどうか</summary>
    [SerializeField] bool IsRotation = false;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        /*Playerが接近してきた*/
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("プレイヤーに当たった");
            /*OnTryをtrueにする*/
            OnTry = true;
            if (IsRotation) { mobobject.transform.localScale = new Vector2(this.gameObject.transform.lossyScale.x * -1, this.gameObject.transform.lossyScale.y); }
            /*アニメーションを止める*/
            mob_animetor.SetBool("Ontry", OnTry);
            mob_animetor.SetFloat("Speed", 0);

        }
    }
}
