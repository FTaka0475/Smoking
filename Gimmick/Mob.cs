using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    /// <summary>
    /// ���u�̃A�j���[�V�����ϐ�
    /// </summary>
    [SerializeField,Header("���u�̃A�j���[�V����")] private Animator mob_animetor;

    /// <summary>
    /// ���u�̃X�s�[�h
    /// </summary>
    [SerializeField, Header("���u�̈ړ����x")] private float mob_speed = 0.01f;

    /// <summary>
    /// �v���C���[��y�̏������W�ɍ��킹�邽�߂̕ϐ�
    /// </summary>
    [SerializeField, Header("�v���C���[��y�̏������W�ɍ��킹�邽��")] private GameObject player;

    /// <summary>
    /// ���̃G�t�F�N�g
    /// </summary>
    [SerializeField, Header("���I�u�W�F�N�g")] private GameObject water;


    /// <summary>
    /// �v���C���[�ڋ߃t���O
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

        /*OnTry��false�̂Ƃ��A�j���[�V������mob_speed��ݒ肷��*/
        if (!OnTry && !GameManager.InPoos)
        {
            /*�A�j���[�V�����̃p�����[�^��ݒ肷��*/
            mob_animetor.SetFloat("Speed", 0.5f);
            /*�Q�[���I�u�W�F�N�g���ړ�*/
            transform.position = new Vector2(transform.position.x - mob_speed, transform.position.y);
            //Debug.Log("aa");

        }
        
    }


    //���u�̃A�j���[�V�����ŌĂяo���Ă���
    public void Water()
    {
        water.SetActive(true);
        OnTry = true;
    }

}
