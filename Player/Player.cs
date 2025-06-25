using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.StandaloneInputModule;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class Player : MonoBehaviour
{
    Smokingspace smoking;

    /// <summary>
    /// プレイヤー動作アニメーション
    /// </summary>
    [SerializeField, Header("アニメーション")] Animations animations;

    /// <summary>
    /// 入力(Horizontal)
    /// </summary>
    float axisH;

    /// <summary>
    /// 入力(Vertical)
    /// </summary>
    private float axisV = 0.0f;

    /// <summary>
    /// 　アニメーションスピード
    /// </summary>
    public float anime;

    /// <summary>
    /// プレイヤーのスピード
    /// </summary>
    [Header("プレイヤーのスピード")] public float SpeedX = 0.1f;

    /// <summary>
    /// プレイヤーが反転する　
    /// </summary>
    bool   returnflug = false;

    /// <summary>
    /// プレイヤーの座標
    /// </summary>
    Vector2 nowpos;

    /// <summary>
    /// スピードダウンしているか
    /// </summary>
    bool SpDown = false;

    /// <summary>
    /// ゲームオブジェクトhp
    /// </summary>
    [SerializeField, Header("HP,残機オブジェクト")] Test_HP hp;

    /// <summary>
    /// たばこゲームオブジェクト
    /// </summary>
    [SerializeField, Header("たばこ")] GameObject tabako;

    /// <summary>
    /// たばこの当たり判定オブジェクト
    /// </summary>
    [SerializeField, Header("たばこ当たり判定")] BoxCollider2D tabako_col;

    /// <summary>
    /// たばこのスケール
    /// </summary>
    Vector2 tabakoS = Vector2.zero;

    /// <summary>
    /// たばこの位置
    /// </summary>
    [SerializeField, Header("たばこの位置")] GameObject tabako_pos;

    /// <summary>
    /// Rigidbody2D
    /// </summary>
    Rigidbody2D rd;

    /// <summary>
    /// プレイヤーやられアニメーションのためのオブジェクト
    /// </summary>
    [SerializeField, Header("床")] BoxCollider2D tabako_yuka;

    /// <summary>
    /// 床のコライダー変数
    /// </summary>

    //プレイヤーが反転するときに使う
    Vector2 plypos = Vector2.zero;

    /// <summary>
    /// アニメーションの速さパラメータ
    /// </summary>
    private string Name_Anime = "Walkspeed";

    /// <summary>
    /// プレイヤーアニメーター
    /// </summary>
    private Animator animator;

    /// <summary>
    /// アニメーションの元となるゲームオブジェクト
    /// </summary>
    [SerializeField, Header("足のアニメーション")] GameObject foot_anime;

    /// <summary>
    /// 影アニメーター
    /// </summary>
    private Animator Shadow_animator;

    /// <summary>
    /// 影アニメーション
    /// </summary>
    [SerializeField, Header("影")] GameObject shadow;

    /// <summary>
    /// 影のパラメータ名
    /// </summary>
    private string Name_SHAnime = "On";

    /// <summary>
    /// ダメージ音のソース
    /// </summary>
    [SerializeField, Header("GameOver source")] AudioSource audio;

    /// <summary>
    /// ダメージ音のクリップ
    /// </summary>
    [SerializeField, Header("GameOver clip")] AudioClip audio_clip;

    /// <summary>
    /// Enum
    /// </summary>
    GameMode gmode;

    //子オブジェクト
    private SpriteRenderer sprite_parent;
    private GameObject child;
    private SpriteRenderer sprite_child;

    /// <summary>
    /// アニメーションストップ
    /// </summary>
    private string Name_animestop = "InOut";

    /// <summary>
    /// ゴールムーブスピード
    /// </summary>
    float speed_move = 0.01f;

    /// <summary>
    /// Shift確認
    /// </summary>
    bool IsShift;

    /// <summary>
    /// 上キー確認
    /// </summary>
    bool IsV;

    /// <summary>
    /// 移動キーフラグ
    /// </summary>
    bool walk;

    /// <summary>
    /// 最後のフェードインイメージ
    /// </summary>
    [SerializeField, Header("フェードイン")] Image FadeIn;
    // Start is called before the first frame update

    /// <summary>
    /// ゴールの真ん中
    /// </summary>
    [SerializeField, Header("ゴールセンター")] GameObject Goal_Center;
    void Start()
    {

        smoking = new Smokingspace();
        smoking.Enable();
        tabako_yuka.enabled = false;
        plypos = transform.localScale;
        tabakoS = tabako.transform.localScale;

        //Rigidbody2Dを取得
        rd = GetComponent<Rigidbody2D>();
        //初めのリスポーン地点
        if (GameManager.InMid)
        {
            //中間地点から始まる
            gameObject.transform.position = GameManager.Respos;
        }
        //足のアニメーションコンポーネント
        animator = foot_anime.GetComponent<Animator>();

        //影のアニメーションコンポーネント
        Shadow_animator = shadow.GetComponent<Animator>();

        //var se = GameManager.instance;
        GameManager gameoverse = GameObject.FindWithTag("GameMain").GetComponent<GameManager>();
        gameoverse.Audio_gameplay();

        gmode = gameoverse.Gmode;
        //StartCoroutine(MadeInWario());

    }

    private Vector2 _inputMove;

    /// <summary>
    /// 移動Action(PlayerInput側から呼ばれる)
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        // 入力値を保持しておく
        _inputMove = context.ReadValue<Vector2>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //ダメージ判定を受けたときtrue
        if (GameManager.IsDamage == true && gmode == GameMode.InGame)
        {
            PlayerDamage(null);
            //Mycol.enabled = true;
            gmode = GameMode.GameOver;
            Debug.Log("顔文字");
            //たばこの当たり判定を消す
            tabako_col.enabled = false;
            animator.SetFloat(Name_Anime, 0);
        }
        else if (GameManager.InPoos == false && gmode == GameMode.InGame)
        {
            if(smoking.Player.Move.triggered)
            {
                Debug.Log("処理されている");
            }
            axisH = _inputMove.x;
            
            if (axisH >= 0.5)
            {
                axisH = 1;
            }
            else if (axisH <= -0.5f)
            {
                axisH = -1;
            }
            else
            {
                axisH = 0;
            }
            Debug.Log("axisH:" + axisH);
            Debug.Log("_inputMove" + _inputMove);
            //axisH = _inputMove.x;
            
            //Debug.Log("IsShift:" + IsShift + " ::: IsV:" + IsV + "::" + SpDown);
            //スピードダウン処理
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) || Input.GetKey(KeyCode.S) || axisV >= 0.5f)
            {
                Debug.Log("処理され知恵r");
                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.S)) && !IsV)
                {
                    IsShift = true;
                    IsV = false;
                }
                else
                {
                    IsShift = false;
                    IsV = true;
                }
                SpDown = true;
            }
            else
            {
                SpDown = false;
            }
            //移動キーが押されたとき
            if (axisH != 0)
            {
                Playermove();
                Imagereturn();

            }
            //反転をできるようにする
            //if (Input.GetButtonUp("Horizontal"))
            //{
            //    returnflug = false;
            //    walk = false;
            //    if (axisH == 0)
            //    { animator.SetFloat(Name_Anime, 0); }
            //}
            if (axisH == 0)
            { animator.SetFloat(Name_Anime, 0); }
            if ((Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift) || Input.GetKeyUp(KeyCode.S)))
            {
                IsShift = false;
            }
            if (Input.GetButtonUp("Vertical"))
            {
                IsV = false;
            }
        }
        else
        {
            returnflug = false;
            animator.SetFloat(Name_Anime, 0);
        }
        //Debug.Log("SHIFTフラグ->" + IsShift + ":AXISVフラグ->" + IsV);
    }
    //反転関数
    void Imagereturn()
    {
        //Debug.Log(axisH);
        //if (axisH != 0 && returnflug == false)
        if (axisH != 0)
        {
            //プレイヤーの方向を反転する

            transform.localScale = new Vector3(plypos.x * axisH, transform.lossyScale.y, transform.lossyScale.z);

            //たばこの方向を反転する
            tabako.transform.localScale = new Vector2(tabakoS.x * axisH, tabako.transform.lossyScale.y);

            //tabako.transform.position = new Vector2(tabako.transform.position.x * axisH, tabako.transform.position.y);
            returnflug = true;
        }
    }

    //移動関数
    void Playermove()
    {
        //Debug.Log(axisH + "( *´艸｀)" + _inputMove.x);

        //上下矢印キー,SHIFTキーが押されたとき
        if (animations.Getanime)
        {
            anime = axisH * 0.65f;
            nowpos = new Vector2(transform.position.x + (SpeedX * anime), transform.position.y);
            //Debug.Log("<color=red> プレイヤースピード：" + Mathf.Abs(axisH * 0.5f) + "</color>");

        }
        else
        {
            anime = axisH;
            nowpos = new Vector2(transform.position.x + (SpeedX * anime), transform.position.y);
            //Debug.Log("<color=red> プレイヤースピード：" + Mathf.Abs(axisH) + "</color>");
        }
        Debug.Log("アニメーションのbool:" + animations.Getanime);
        //Colliderの判定を決めている

        Vector3 screen_LeftBottom = Camera.main.ScreenToWorldPoint(Vector3.zero);

        //左端の時止まる
        if (screen_LeftBottom.x + 1 < nowpos.x)
        {
            transform.position = nowpos;

        }
        //たばこを動かす
        tabako.transform.position = tabako_pos.transform.position;
        if (!walk || axisH == 0 || (animator.GetFloat(Name_Anime) != anime))
        {
            animator.SetFloat(Name_Anime, Mathf.Abs(anime));
            walk = true;

        }

    }

    //Playerが当たり判定に当たったら関数
    void OnTriggerStay2D(Collider2D col)
    {
        //やられた～(/ω＼)///
        //Debug.Log("<color=white> 当たった! </color>");
        if (col.gameObject.tag == "Bougai" && axisV < 1)
        {
            GameManager.IsDamage = true;
        }
        //喫煙所到着
        if (col.gameObject.tag == "Safe")
        {
            //セーフゾーンにいるフラグ
            GameManager.InSafe = true;
        }

    }

    //喫煙所についたとき
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Safe")
        {
            //中間についた!
            GameManager.InMid = true;
            //中間時点のポジションを記録する
            GameManager.Respos = new Vector2(col.gameObject.transform.position.x, gameObject.transform.position.y);

        }
        //ゲームクリア
        if (col.gameObject.tag == "Goal")
        {
            GameManager.IsClear = true;
            StartCoroutine("Clear", col);
            //Clear(col) ;
        }
    }

    //Playerが当たり判定から出たら関数
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Safe")
        {
            //出た
            GameManager.InSafe = false;
        }
    }

    //ダメージをくらったとき
    public void PlayerDamage(Collider2D col)
    {
        Shadow_animator.SetBool(Name_SHAnime, GameManager.IsDamage);
        tabako_yuka.enabled = true;
        hp.RespawnLife();
        if (col != null)
        {
            col.enabled = false;

        }
        //やられたときに
        audio.PlayOneShot(audio_clip);
        gmode = GameMode.GameOver;
        GameManager gameoverse = GameObject.FindWithTag("GameMain").GetComponent<GameManager>();
        gameoverse.Audio_gameover();
        gameoverse.Change_Mode(gmode);
        rd.constraints = RigidbodyConstraints2D.None;
        transform.Rotate(0, 0, transform.lossyScale.x * 5, Space.World);   //ここで倒れこむようにしている(アニメーションにするか考え中)

    }

    //クリア
    IEnumerator Clear(Collider2D col)
    {
        Shadow_animator.SetBool(Name_SHAnime, true);
        
        gmode = GameMode.GameClear;
        var se = GameManager.instance;
        se.Change_Mode(gmode);
        FadeIn.color = new Color(0, 0, 0, 0);
        Vector2 goalpos = Goal_Center.transform.position - transform.position;
        //自動でゴール地点に近づいていく
        while (!(transform.position.x >= col.transform.position.x))
        {
            //画面をだんだん暗くさせる(フェードイン)
            FadeIn.color = new Color(0, 0, 0, FadeIn.color.a + 0.01f);
            Debug.Log(FadeIn.color);
            animator.SetFloat(Name_Anime, 0.8f);
            transform.position = new Vector2(transform.position.x + goalpos.x * speed_move * 0.8f, transform.position.y + goalpos.y * speed_move);
            yield return null;
        }
        StartCoroutine(SceneChange());
        yield return null;
    }

    public IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(1f);
        GameManager gameoverse = GameObject.FindWithTag("GameMain").GetComponent<GameManager>();
        gameoverse.Audio_gameclear();
        SceneManager.LoadScene("Result");
        yield return null;
    }

    /// <summary>
    /// Playerスクリプト:SpDownフラグ
    /// </summary>
    public bool Getbool
    {
        get { return this.SpDown; }//取得用
        private set { this.SpDown = value; }//値入力用
    }

    /// <summary>
    /// Playerスクリプト:Shiftキーが押されているかのフラグ
    /// </summary>
    public bool Getshift_bool
    {
        get { return this.IsShift; }//取得用
        private set { this.IsShift = value; }//値入力用
    }
    /// <summary>
    /// Playerスクリプト:上キーが押されているかのフラグ
    /// </summary>
    public bool GetaxisV_bool
    {
        get { return this.IsV; }//取得用
        private set { this.IsV = value; }//値入力用
    }
}
