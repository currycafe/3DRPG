using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
	public GameObject newPrefab; 
	public string pushKey = "z"; 
	public float throwX = 0;     
	public float throwY = 3;     
	public float throwZ = 4;     
	public float offsetX = 0f;   
	public float offsetY = 1f;   
	public float offsetZ = 0.5f; 

	bool pushFlag = false;

	void Update()
	{
		if (Input.GetKey(pushKey)) 
		{
			if (pushFlag == false) 
			{
				pushFlag = true; 
				Vector3 newPos = this.transform.position;
				Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
				offset = this.transform.rotation * offset;
				newPos = newPos + offset;

				
				GameObject newGameObject = Instantiate(newPrefab) as GameObject;
				newGameObject.transform.position = newPos;


				// “Š‚°‚é
				Rigidbody rbody = newGameObject.GetComponent<Rigidbody>();
				Vector3 throwV = new Vector3(throwX, throwY, throwZ);
				throwV = this.transform.rotation * throwV;
				rbody.AddForce(throwV, ForceMode.Impulse);
			}
		}
		else
		{
			pushFlag = false;       
		}
	}
}
