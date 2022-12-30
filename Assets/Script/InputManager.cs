using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	Vector2 slideStartPosition;
	Vector2 prevPosition;
	Vector2 delta = Vector2.zero;
	bool moved = false;

	void Update()
	{
		// �X���C�h�J�n�n�_.
		if (Input.GetButtonDown("Fire1"))
			slideStartPosition = GetCursorPosition();

		// ��ʂ̂P���ȏ�ړ���������X���C�h�J�n�Ɣ��f����.
		if (Input.GetButton("Fire1"))
		{
			if (Vector2.Distance(slideStartPosition, GetCursorPosition()) >= (Screen.width * 0.1f))
				moved = true;
		}

		// �X���C�h���삪�I��������.
		if (!Input.GetButtonUp("Fire1") && !Input.GetButton("Fire1"))
			moved = false; // �X���C�h�͏I�����.

		// �ړ��ʂ����߂�.
		if (moved)
			delta = GetCursorPosition() - prevPosition;
		else
			delta = Vector2.zero;

		// �J�[�\���ʒu���X�V.
		prevPosition = GetCursorPosition();
	}

	// �N���b�N���ꂽ��.
	public bool Clicked()
	{
		if (!moved && Input.GetButtonUp("Fire1"))
			return true;
		else
			return false;
	}

	// �X���C�h���̃J�[�\���̈ړ���.
	public Vector2 GetDeltaPosition()
	{
		return delta;
	}

	// �X���C�h����.
	public bool Moved()
	{
		return moved;
	}

	public Vector2 GetCursorPosition()
	{
		return Input.mousePosition;
	}
}
