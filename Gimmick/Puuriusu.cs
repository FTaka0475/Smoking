using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puuriusu : MonoBehaviour
{

    /// <summary>
    /// �ԉ摜
    /// </summary>
    [SerializeField, Header("�摜")] Sprite Puricar;

    /// <summary>
    /// �Ԃ�SpriteRenderer
    /// </summary>
    [SerializeField, Header("�摜�I�u�W�F�N�g")] SpriteRenderer Puricar_Ren;
    ///<summary>
    ///�Ԃ̃X�s�[�h
    /// <summary>
    [SerializeField, Header("Speed")] float speed = 0.5f;

    ///<summary>
    ///�I�[�f�B�I�\�[�X
    /// <summary>
    [SerializeField, Header("AudioSource")] AudioSource Asource;

    ///<summary>
    ///SE
    /// <summary>
    [SerializeField, Header("���ʉ�")] AudioClip AClip;
    /// �Ղ�Ղ�enum
    /// </summary>
    //�v���E�X�̏�Ԃ�����
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


    //�Ԃ���������<BoxCollider���ł��>
    private void OnTriggerEnter2D(Collider2D col)
    {
           if(col.gameObject.tag == "Player")
            {
                car_enum = PCar.Go;
                Puricar_Ren.sprite = Puricar;
                PSE();
            }
    }

    //�Ԃ̈ړ��֐�
    void Move()
    {
        this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x - speed, this.gameObject.transform.position.y);
    }
    
    //�v���E�X�̌��ʉ���炷
    void PSE()
    {
        Asource.PlayOneShot(AClip);
    }
}
