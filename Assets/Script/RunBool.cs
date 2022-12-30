using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBool : MonoBehaviour
{
    public string parameterName = "";   // パラメータ名：Inspectorで指定

    void Update()
    {
        // もし、上下左右キーを押したら
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
        // パラメータの値を変更
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool(parameterName, pushFlag);
    }
}
