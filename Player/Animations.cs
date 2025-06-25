using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : MonoBehaviour
{

    Smokingspace smoking;

    // Start is called before the first frame update
    /// <summary>
    /// プレイヤー動作アニメーション
    /// </summary>
    [SerializeField, Header("プレイヤー")] Player player;
    /// <summary>
    /// プレイヤー動作アニメーション
    /// </summary>
    [SerializeField, Header("プレイヤー動作")] private Animator ply_anime;
    /// <summary>
    /// シフトパラメータ名
    /// </summary>
    private string Name_shift = "OnShift";
    /// <summary>
    /// 上下キーパラメータ名
    /// </summary>
    private string Name_ud = "Up";

    /// <summary>
    /// 上下入力変数
    /// </summary>
    float axisV;

    /// <summary>
    /// 現在のシフト状態
    /// </summary>
    private bool isShifted = false;
    private bool isShiftKeyDown = false;
    private bool up = false;
    private bool now = false;
    /// <summary>
    /// 上にあげるフラグ
    /// </summary>
    private bool isUp = false;
    //ゲームオブジェクト
    [SerializeField, Header("たばこ")] GameObject tabako;

    ///<summary>
    ///たばこのアニメーター
    /// </summary>
    private Animator tabako_anime;


    private GameMode gameMode;

    void Start()
    {
        ply_anime = GetComponent<Animator>();
        tabako_anime = tabako.GetComponent<Animator>();
        smoking = new Smokingspace();
        smoking.Enable();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        
        Debug.Log("Pressed");
        up = !up;
    }

    public void Fire2(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("Released");
        isShiftKeyDown = !isShiftKeyDown;
    }
    void Update()
    {
        if (!GameManager.InPoos)
        {
            axisV = Input.GetAxisRaw("Vertical");
            
            now = (up || isShiftKeyDown);
            
            //Debug.Log(isShiftKeyDown);
            if (!GameManager.IsDamage)
            {
                
                    // シフトキーの状態が変化したときだけAnimatorに反映
                    if (isShiftKeyDown != isShifted)
                    {
                        isShifted = isShiftKeyDown;
                        //Debug.Log("<color=red>" + isShifted + "</color>");
                        ply_anime.SetBool(Name_shift, isShifted);
                        tabako_anime.SetBool(Name_shift, isShifted);
                    }
                    //Debug.Log(ply_anime.GetFloat(Name_ud));
                    // 上下入力が変化した場合だけAnimatorに反映
                    else if (ply_anime.GetBool(Name_ud) != up)
                    {
                        Debug.Log("<color=red>動いている</color>");
                        ply_anime.SetBool(Name_ud, up);
                        tabako_anime.SetBool(Name_ud, up);

                    }
            }
        }
    }

    public bool Getanime
    {
        get { return this.now; }
        private set { this.now = value; }
    }

    public bool GetUp
    {
        get { return this.up; }
        private set { this.up = value; }
    }

    public bool GetDown
    { 
        get { return this.isShiftKeyDown; }
        private set { this.isShiftKeyDown = value; }
    }
}
