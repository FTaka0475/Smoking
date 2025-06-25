using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    InGame,
    Pause,
    GameOver,
    GameClear,
    Title
}
//GameMode gmode = GameMode.InGame;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    GameMode gmode = GameMode.Title;
    GameMode gmode_check;
    ///<summary>
    ///ゲームクリア
    ///</summary>>
    public static bool IsClear = false;


    /// <summary>
    /// 仮HP
    /// </summary>
    public static int HP_sum = 70;
    /// <summary>
    /// 仮)中間時点中フラグ
    /// </summary>
    public static bool InSafe = false;
    /// <summary>
    ///仮)ゲームオーバーフラグ
    /// </summary>
    public static bool IsDamage = false;
    /// <summary>
    /// 仮)ポーズ画面中フラグ
    /// </summary>
    public static bool InPoos = false;
    /// <summary>
    /// 仮)中間地点フラグ
    /// </summary>
    public static bool InMid = false;

    /// <summary>
    /// Inspectorで残機数を変更できる
    /// </summary>
    [SerializeField, Header("残機数")] private int Base_Life;
    /// <summary>
    /// 仮)残機数 
    /// </summary>
    public static int life = 5;
    /// <summary>
    /// 仮)リスポーン地点
    /// </summary>
    public static Vector2 Respos;


    /// <summary>
    /// BGM番号
    /// </summary>
    int audionumber = 0;
    /// <summary>
    /// BGM番号1
    /// </summary>
    int title_number = 1;
    /// <summary>
    /// BGM番号2
    /// </summary>
    int gameplay_number = 2;
    /// <summary>
    /// BGM番号3
    /// </summary>
    int gameover_number = 3;
    /// <summary>
    /// BGM番号4
    /// </summary>
    int gameclear_number = 4;
    /// <summary>
    ///オーディオソース
    /// </summary>
    [SerializeField, Header("Source")] AudioSource Maudio;
    /// <summary>
    /// タイトルBGM
    /// </summary>
    [SerializeField, Header("Title")] AudioClip audioclip_title;
    /// <summary>
    /// プレイ中BGM
    /// </summary>
    [SerializeField, Header("Gameplay")] AudioClip audioclip_gameplay;
    /// <summary>
    /// ゲームオーバーBGM
    /// </summary>
    [SerializeField, Header("Gameover")] AudioClip audioclip_gameover;
    /// <summary>
    /// リザルトBGM
    /// </summary>
    [SerializeField, Header("Gameclear")] AudioClip audioclip_gameclear;
    /// <summary>
    /// ボタンSE:OK
    /// </summary>
    [SerializeField, Header("Button_OK")] AudioClip audioclip_Btok;
    /// <summary>
    /// ボタンSE:No
    /// </summary>
    [SerializeField, Header("Button_NO")] AudioClip audioclip_Btno;

    [SerializeField, Header("Not Destroy")] GameObject[] gameObjects;

    //[SerializeField] GameObject cursorObject;
    // Start is called before the first frame update
    void Awake()
    {
        //FPSの最大値を120にする
        Application.targetFrameRate = 60;
        DontDestroy();
        RamClear();
        Maudio.loop = true;
        
        gmode_check = gmode;
    }

    private void Start()
    {
        Audio_title();
    }
    private void Update()
    {
        if(gmode != gmode_check)
        {
            Debug.Log(gmode);
            gmode_check = gmode;
            if(gmode == GameMode.InGame)
            {
                //Cursor.visible = false;
                //Cursor.lockState = CursorLockMode.Locked;
                //cursorObject.SetActive(false);
                Debug.Log("カーソルが消えた!");
            }
            else
            {
               // cursorObject.SetActive(true);
            }
        }
    }
    public void Audio_title()
    {
        if (audionumber != title_number)
        {
            Maudio.Stop();
            Maudio.clip = audioclip_title;
            Maudio.Play();
            audionumber = title_number;
        }

    }

    public void Audio_gameplay()
    {
        if (audionumber != gameplay_number)
        {
            Maudio.Stop();
            Maudio.clip = audioclip_gameplay;
            Maudio.Play();
            audionumber = gameplay_number;
        }

    }

    public void Audio_gameover()
    {
        if (audionumber != gameover_number)
        {
            Maudio.Stop();
            Maudio.clip = audioclip_gameover;
            Maudio.Play();
            audionumber = gameover_number;
        }


    }

    public void Audio_gameclear()
    {
        if (audionumber != gameclear_number)
        {
            Maudio.Stop();
            Maudio.clip = audioclip_gameclear;
            Maudio.Play();
            audionumber = gameclear_number;
        }


    }


    /// <summary>
    /// 初期化
    /// </summary>
    public void RamClear()
    {
        InSafe = false;
        InMid = false;
        IsDamage = false;
        life = Base_Life;
        IsClear = false;
        InPoos = false;
        gmode = GameMode.Title;
    }

    /// <summary>
    /// DontDestroy作成
    /// </summary>
    void DontDestroy()
    {
        if (instance == null)
        {
            instance = this;
            for(int i = 0; i < gameObjects.Length;i++)
            {
                DontDestroyOnLoad(gameObjects[i]);
            }
            
            //Debug.Log(gameObjects.Length);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBGMvolume(float volume)
    {
        Maudio.volume = volume;
    }

    public GameObject OICanvas;

    /*操作説明スイッチ*/
    public void Sousa()
    {
        OICanvas.SetActive(true);
    }

    public void SousaBack()
    {
        OICanvas.SetActive(false);
    }

    public void OK()
    {
        Maudio.PlayOneShot(audioclip_Btok);
        //Debug.Log("ボタンOK");
    }

    public void NO()
    {
        Maudio.PlayOneShot(audioclip_Btno);
        //Debug.Log("ボタンNO");
    }

    public void SetBGMVolume(float volume)
    {
        instance.SetBGMvolume(volume);
    }

    public void Change_Mode(GameMode Gmode)
    {
        gmode = Gmode;
    }

    public GameMode Gmode
    {
        get { return gmode; }
        private set { gmode = value; }
    }
}
