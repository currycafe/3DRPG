using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AttackArea : MonoBehaviour {
	Status status;
	
	void Start()
	{
		status = transform.root.GetComponent<Status>();
	}
	
	
	public class AttackInfo
	{
		public int attackPower; // この攻撃の攻撃力.
		public Transform attacker; // 攻撃者.
	}
	
	
	// 攻撃情報を取得する.
	AttackInfo GetAttackInfo()
	{			
		AttackInfo attackInfo = new AttackInfo();
		// 攻撃力の計算.
		attackInfo.attackPower = status.Power;
		attackInfo.attacker = transform.root;
		
		return attackInfo;
	}
	
	// 当たった.
	void OnTriggerEnter(Collider other)
	{
		// 攻撃が当たった相手のDamageメッセージをおくる.
		other.SendMessage("Damage",GetAttackInfo());
		// 攻撃した対象を保存.
		status.lastAttackTarget = other.transform.root.gameObject;
	}
	
	
	// 攻撃判定を有効にする.
	void OnAttack()
	{
		GetComponent<Collider>().enabled = true;
	}
	
	
	// 攻撃判定を無効にする.
	void OnAttackTermination()
	{
		GetComponent<Collider>().enabled = false;
	}
}
