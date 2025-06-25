using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPark : MonoBehaviour
{

    /// <summary>
    /// プリウスカー
    /// </summary>
    [SerializeField, Header("車")] GameObject PuCar;

    /// <summary>
    /// 水しぶき:判定なし
    /// </summary>
    [SerializeField, Header("水")] GameObject water;

    /// <summary>
    /// 水しぶき:判定有り
    /// </summary>
    [SerializeField] GameObject water_main;
    enum waterpark
    { 
        Go,
        Del,
        End
    }
    /// <summary>
    /// enum
    /// </summary>
    waterpark WP = waterpark.Go;

    // Start is called before the first frame update
    void Start()
    {
        //はじめは消しておく
        water.SetActive(false);
        water_main.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //WP(enum)をswitch文で判別処理
        switch (WP)
        {
            case waterpark.Go:
                MoveCar();
                break;
            case waterpark.Del:
                deletecar();
                WP = waterpark.End;
                break;
            default:
                return;
            
        }


    }

    /*水しぶきを出すための関数車のpositionで判断している*/
    void MoveCar()
    {
        //Debug.Log(PuCar.transform.position.x);
        if (PuCar.transform.position.x -4 < transform.position.x)
        {
            Debug.Log("水しぶき");
            LTwater();
        }

    }

    /*車の削除判定処理*/
    void deletecar()
    {
        if(PuCar.transform.position.x < -10)
        {
            Delete();

        }
    }

    /*水オブジェクトのSetActiveをtrueにする*/
    void LTwater()
    {
        water.SetActive(true);
        water_main.SetActive(true);
        WP = waterpark.Del;
    }

    /*車の削除関数*/
    void Delete()
    {
        PuCar.SetActive(false);
    }
}
