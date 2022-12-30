using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public float distance = 5.0f;
	public float horizontalAngle = 0.0f;
	public float rotAngle = 180.0f; // ��ʂ̉������J�[�\�����ړ��������Ƃ����x��]���邩.
	public float verticalAngle = 10.0f;
	public Transform lookTarget;
	public Vector3 offset = Vector3.zero;

	InputManager inputManager;
	void Start()
	{
		inputManager = FindObjectOfType<InputManager>();
	}

	// Update is called once per frame
	void LateUpdate()
	{
		// �h���b�O���͂ŃJ�����̃A���O�����X�V����.
		if (inputManager.Moved())
		{
			float anglePerPixel = rotAngle / (float)Screen.width;
			Vector2 delta = inputManager.GetDeltaPosition();
			horizontalAngle += delta.x * anglePerPixel;
			horizontalAngle = Mathf.Repeat(horizontalAngle, 360.0f);
			verticalAngle -= delta.y * anglePerPixel;
			verticalAngle = Mathf.Clamp(verticalAngle, -60.0f, 60.0f);
		}

		// �J�������ʒu�Ɖ�]���X�V����.
		if (lookTarget != null)
		{
			Vector3 lookPosition = lookTarget.position + offset;
			// �����Ώۂ���̑��Έʒu�����߂�.
			Vector3 relativePos = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);

			// �����Ώۂ̈ʒu�ɃI�t�Z�b�g���Z�����ʒu�Ɉړ�������.
			transform.position = lookPosition + relativePos;

			// �����Ώۂ𒍎�������.
			transform.LookAt(lookPosition);

			// ��Q���������.
			RaycastHit hitInfo;
			if (Physics.Linecast(lookPosition, transform.position, out hitInfo, 1 << LayerMask.NameToLayer("Ground")))
				transform.position = hitInfo.point;
		}
	}
}
