using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mob_move : MonoBehaviour
{
    /// <summary>
    /// ���u�̃A�j���[�V�����ϐ�
    /// </summary>
    [SerializeField, Header("���u�̃A�j���[�V����")] private Animator mob_animetor;

    /// <summary>
    /// �v���C���[�ڋ߃t���O
    /// </summary>
    bool OnTry = false;
    // Start is called before the first frame update

    ///<summary>
    ///���u
    ///</summary>
    [SerializeField, Header("���u�I�u�W�F�N�g")] GameObject mobobject;

    /// <summary>�U��Ԃ邩�ǂ���</summary>
    [SerializeField] bool IsRotation = false;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        /*Player���ڋ߂��Ă���*/
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("�v���C���[�ɓ�������");
            /*OnTry��true�ɂ���*/
            OnTry = true;
            if (IsRotation) { mobobject.transform.localScale = new Vector2(this.gameObject.transform.lossyScale.x * -1, this.gameObject.transform.lossyScale.y); }
            /*�A�j���[�V�������~�߂�*/
            mob_animetor.SetBool("Ontry", OnTry);
            mob_animetor.SetFloat("Speed", 0);

        }
    }
}
