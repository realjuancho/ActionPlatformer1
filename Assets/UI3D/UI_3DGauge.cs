using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_3DGauge : MonoBehaviour {


	[Range(0.0f,1.0f)]
	public float Value;
	float steps;
	
	Transform full;
	Transform cameraTransform;

	void Awake()
	{
		full = transform.Find("Full");

	}

	void Update()
	{

		full.localPosition =  new Vector3(full.localPosition.x, (Value-1), full.localPosition.z);
		full.localScale = new Vector3(full.localScale.x, Value, full.localScale.z);


	}

}
