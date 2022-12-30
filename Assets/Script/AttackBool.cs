using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBool : MonoBehaviour
{
    public string parameterName = "";   // パラメータ名：Inspectorで指定

    void Update()
    {
        // もし、上下左右キーを押したら
        bool pushFlag = false;
        if (Input.GetKey("space"))
        {
            pushFlag = true;
        }
        

        // パラメータの値を変更
        Animator m_Animator = gameObject.GetComponent<Animator>();
        m_Animator.SetBool(parameterName, pushFlag);
    }
}
