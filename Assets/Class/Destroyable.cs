using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour {

	[HideInInspector]
	public bool IsDestroyable = true;


	public bool ShowGauge = true;
	public int Health = 10;

	UI_3DGauge UI_3DGauge;
	float gaugeValue;
	void Start()
	{
		 

		MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

		if(ShowGauge)
		{
			GameObject UI_3DGauge_Object = (GameObject)Instantiate(Resources.Load(PrefabHash.UI_3DGauge_Health), 
			 	transform.position + (Vector3.up * meshRenderer.bounds.extents.y * 3)
			 	,transform.rotation);

			UI_3DGauge_Object.transform.SetParent(transform);

			UI_3DGauge = UI_3DGauge_Object.GetComponentInChildren<UI_3DGauge>();

			gaugeValue = 1.0f / Health;
		}
	}

	void Update()
	{
		CheckHealth();
	}

	public void ReceiveDamage(int Damage)
	{
		Health-=Damage;
	}

	void CheckHealth()
	{
		if(Health <= 0)
		{
			Destroy(gameObject);
		}

		else if(ShowGauge)
		{
			

			UI_3DGauge.Value = gaugeValue * Health;

		}
	}
}
