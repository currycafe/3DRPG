using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
	// �̗�.
	public int HP = 100;
	public int MaxHP = 100;

	// �U����.
	public int Power = 10;

	// �Ō�ɍU�������Ώ�.
	public GameObject lastAttackTarget = null;

	//---------- GUI����уl�b�g���[�N�̏͂Ŏg�p���܂�. ----------
	// �v���C���[��.
	public string characterName = "Player";

	//--------- �A�j���[�V�����̏͂Ŏg�p���܂�. -----------
	//���.
	public bool attacking = false;
	public bool died = false;
}