using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {


	public Material steppedOnMaterial;

	Material originalMaterial; 

	void Awake()
	{
		
	}

	public void GetOriginalMaterial()
	{

		originalMaterial = gameObject.GetComponent<MeshRenderer>().materials[0];
	}

	public void SteppedOn()
	{
		gameObject.GetComponent<MeshRenderer>().material = steppedOnMaterial;
	}

	public void SteppedOff()
	{
		gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
	}

	Player player;
	void OnTriggerEnter(Collider col)
	{
		player = col.gameObject.GetComponent<Player>();
		if(player)
		{
			player.gameObject.transform.SetParent(transform);
		}
	}

	void OnTriggerStay(Collider col)
	{
		player = col.gameObject.GetComponent<Player>();
		if(player)
		{
			SteppedOn();
		}
	}

	void OnTriggerExit(Collider col)
	{

	 	player = col.gameObject.GetComponent<Player>();
		if(player)
		{
			player.gameObject.transform.SetParent(null);

			SteppedOff();
		}
	}
}
