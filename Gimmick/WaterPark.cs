using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPark : MonoBehaviour
{

    /// <summary>
    /// �v���E�X�J�[
    /// </summary>
    [SerializeField, Header("��")] GameObject PuCar;

    /// <summary>
    /// �����Ԃ�:����Ȃ�
    /// </summary>
    [SerializeField, Header("��")] GameObject water;

    /// <summary>
    /// �����Ԃ�:����L��
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
        //�͂��߂͏����Ă���
        water.SetActive(false);
        water_main.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //WP(enum)��switch���Ŕ��ʏ���
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

    /*�����Ԃ����o�����߂̊֐��Ԃ�position�Ŕ��f���Ă���*/
    void MoveCar()
    {
        //Debug.Log(PuCar.transform.position.x);
        if (PuCar.transform.position.x -4 < transform.position.x)
        {
            Debug.Log("�����Ԃ�");
            LTwater();
        }

    }

    /*�Ԃ̍폜���菈��*/
    void deletecar()
    {
        if(PuCar.transform.position.x < -10)
        {
            Delete();

        }
    }

    /*���I�u�W�F�N�g��SetActive��true�ɂ���*/
    void LTwater()
    {
        water.SetActive(true);
        water_main.SetActive(true);
        WP = waterpark.Del;
    }

    /*�Ԃ̍폜�֐�*/
    void Delete()
    {
        PuCar.SetActive(false);
    }
}
