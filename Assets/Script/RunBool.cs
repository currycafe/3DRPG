using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBool : MonoBehaviour
{
    public string parameterName = "";   // �p�����[�^���FInspector�Ŏw��

    void Update()
    {
        // �����A�㉺���E�L�[����������
        bool pushFlag = false;
        if (Input.GetKey(KeyCode.W))
        {
            pushFlag = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pushFlag = true;
        }
        //if (Input.GetKey(KeyCode.A))
        //{
        //    pushFlag = false;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    pushFlag = false;
        //}
        // �p�����[�^�̒l��ύX
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool(parameterName, pushFlag);
    }
}
