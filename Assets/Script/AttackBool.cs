using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBool : MonoBehaviour
{
    public string parameterName = "";   // �p�����[�^���FInspector�Ŏw��

    void Update()
    {
        // �����A�㉺���E�L�[����������
        bool pushFlag = false;
        if (Input.GetKey("space"))
        {
            pushFlag = true;
        }
        

        // �p�����[�^�̒l��ύX
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool(parameterName, pushFlag);
    }
}
