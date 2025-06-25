using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Animations : MonoBehaviour
{

    Smokingspace smoking;

    // Start is called before the first frame update
    /// <summary>
    /// �v���C���[����A�j���[�V����
    /// </summary>
    [SerializeField, Header("�v���C���[")] Player player;
    /// <summary>
    /// �v���C���[����A�j���[�V����
    /// </summary>
    [SerializeField, Header("�v���C���[����")] private Animator ply_anime;
    /// <summary>
    /// �V�t�g�p�����[�^��
    /// </summary>
    private string Name_shift = "OnShift";
    /// <summary>
    /// �㉺�L�[�p�����[�^��
    /// </summary>
    private string Name_ud = "Up";

    /// <summary>
    /// �㉺���͕ϐ�
    /// </summary>
    float axisV;

    /// <summary>
    /// ���݂̃V�t�g���
    /// </summary>
    private bool isShifted = false;
    private bool isShiftKeyDown = false;
    private bool up = false;
    private bool now = false;
    /// <summary>
    /// ��ɂ�����t���O
    /// </summary>
    private bool isUp = false;
    //�Q�[���I�u�W�F�N�g
    [SerializeField, Header("���΂�")] GameObject tabako;

    ///<summary>
    ///���΂��̃A�j���[�^�[
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
                
                    // �V�t�g�L�[�̏�Ԃ��ω������Ƃ�����Animator�ɔ��f
                    if (isShiftKeyDown != isShifted)
                    {
                        isShifted = isShiftKeyDown;
                        //Debug.Log("<color=red>" + isShifted + "</color>");
                        ply_anime.SetBool(Name_shift, isShifted);
                        tabako_anime.SetBool(Name_shift, isShifted);
                    }
                    //Debug.Log(ply_anime.GetFloat(Name_ud));
                    // �㉺���͂��ω������ꍇ����Animator�ɔ��f
                    else if (ply_anime.GetBool(Name_ud) != up)
                    {
                        Debug.Log("<color=red>�����Ă���</color>");
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
