using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	const float GravityPower = 9.8f;
	//�@�ړI�n�ɂ����Ƃ݂Ȃ���~����.
	const float StoppingDistance = 0.6f;

	// ���݂̈ړ����x.
	Vector3 velocity = Vector3.zero;
	// �L�����N�^�[�R���g���[���[�̃L���b�V��.
	CharacterController characterController;
	// �����������i�������� true/�������Ă��Ȃ� false)
	public bool arrived = false;

	// �����������I�Ɏw�����邩.
	bool forceRotate = false;

	// �����I�Ɍ�������������.
	Vector3 forceRotateDirection;

	// �ړI�n.
	public Vector3 destination;

	// �ړ����x.
	public float walkSpeed = 6.0f;

	// ��]���x.
	public float rotationSpeed = 360.0f;



	// Use this for initialization
	void Start()
	{
		characterController = GetComponent<CharacterController>();
		destination = transform.position;
	}

	// Update is called once per frame
	void Update()
	{

		// �ړ����xvelocity���X�V����
		if (characterController.isGrounded)
		{
			//�@�����ʂł̈ړ����l����̂�XZ�݈̂���.
			Vector3 destinationXZ = destination;
			destinationXZ.y = transform.position.y;// ������ړI�n�ƌ��ݒn�𓯂��ɂ��Ă���.

			//********* ��������XZ�݂̂ōl����. ********
			// �ړI�n�܂ł̋����ƕ��p�����߂�.
			Vector3 direction = (destinationXZ - transform.position).normalized;
			float distance = Vector3.Distance(transform.position, destinationXZ);

			// ���݂̑��x��ޔ�.
			Vector3 currentVelocity = velocity;

			//�@�ړI�n�ɂ����Â����瓞��..
			if (arrived || distance < StoppingDistance)
				arrived = true;


			// �ړ����x�����߂�.
			if (arrived)
				velocity = Vector3.zero;
			else
				velocity = direction * walkSpeed;


			// �X���[�Y�ɕ��.
			velocity = Vector3.Lerp(currentVelocity, velocity, Mathf.Min(Time.deltaTime * 5.0f, 1.0f));
			velocity.y = 0;


			if (!forceRotate)
			{
				// �������s�����������Ɍ�����.
				if (velocity.magnitude > 0.1f && !arrived)
				{ // �ړ����ĂȂ�����������͍X�V���Ȃ�.
					Quaternion characterTargetRotation = Quaternion.LookRotation(direction);
					transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);
				}
			}
			else
			{
				// ���������w��.
				Quaternion characterTargetRotation = Quaternion.LookRotation(forceRotateDirection);
				transform.rotation = Quaternion.RotateTowards(transform.rotation, characterTargetRotation, rotationSpeed * Time.deltaTime);
			}

		}

		// �d��.
		velocity += Vector3.down * GravityPower * Time.deltaTime;

		// �ڒn���Ă�����v��������n�ʂɉ����t����.
		// (Unity��CharactorController�̓����̂��߁j
		Vector3 snapGround = Vector3.zero;
		if (characterController.isGrounded)
			snapGround = Vector3.down;

		// CharacterController���g���ē�����.
		characterController.Move(velocity * Time.deltaTime + snapGround);

		if (characterController.velocity.magnitude < 0.1f)
			arrived = true;

		// �����I�Ɍ�����ς��������.
		if (forceRotate && Vector3.Dot(transform.forward, forceRotateDirection) > 0.99f)
			forceRotate = false;


	}

	// �ړI�n��ݒ肷��.����destination�͖ړI�n.
	public void SetDestination(Vector3 destination)
	{
		arrived = false;
		this.destination = destination;
	}

	// �w�肵����������������.
	public void SetDirection(Vector3 direction)
	{
		forceRotateDirection = direction;
		forceRotateDirection.y = 0;
		forceRotateDirection.Normalize();
		forceRotate = true;
	}

	// �ړ�����߂�.
	public void StopMove()
	{
		destination = transform.position; // ���ݒn�_��ړI�n�ɂ��Ă��܂�.
	}

	// �ړI�n�ɓ����������𒲂ׂ�. true�@��������/ false �������Ă��Ȃ�.
	public bool Arrived()
	{
		return arrived;
	}
}
