using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Destroyable {


	public bool DebugEnemy = false;

	public float bobHeight = 0.2f;
 	public float bobSpeed = 10.0f;
	public float detectionDistance = 10;
	public float bottomDistance = 2;

	// Update is called once per frame
	Vector3 meshExtents;
	void Awake()
	{
		meshExtents = gameObject.GetComponent<MeshRenderer>().bounds.extents;
	}

	float time = 0;
	float bottomOffset;
	void FixedUpdate ()
	{
		time+= Time.deltaTime;

		for(int i = 0; i < 8; i++)
		{
			Vector3 direccion = transform.forward * detectionDistance;		
			direccion = Quaternion.AngleAxis(-45 * i, Vector3.up) * direccion;

			if(DebugEnemy) Debug.DrawRay(transform.position, direccion, Color.blue);
		}

		Vector3 checkBottomOrigin = transform.position;
        bottomOffset = meshExtents.y;
		checkBottomOrigin += (Vector3.down * bottomOffset);
		bottomDistance += Senoidal(bobHeight,bobSpeed,0,0,time);
		Vector3 checkBottomDirection = Vector3.down * bottomDistance;

		Ray checkBottomRay = new Ray(checkBottomOrigin, checkBottomDirection);
		RaycastHit checkBottomHit;
	    Physics.Raycast(checkBottomRay, out checkBottomHit, bottomDistance);

		Collider col = checkBottomHit.collider;

		if(col)
		{
			if(checkBottomHit.collider.gameObject.tag == "Floor")
			{
				if(DebugEnemy) Debug.DrawRay(checkBottomOrigin, Vector3.down * checkBottomHit.distance, Color.red);

				Vector3 pos = transform.position;
                float height = checkBottomHit.point.y + bottomDistance + bottomOffset;

				transform.position = new Vector3(pos.x, height, pos.z);
			}
		}
		else
		{
			
			if(DebugEnemy) Debug.DrawRay(checkBottomOrigin, checkBottomDirection, Color.green);

			transform.position = Vector3.Lerp(transform.position, transform.position +  Physics.gravity, 1.0f * Time.deltaTime);
		}

		if(time > float.MaxValue -100) time = 0;
	}

	//f(x)= a sin(bx+c)+d

	float Senoidal(float a, float b, float c, float d, float x)
	{
		x = a * Mathf.Sin(b*x+c)+d;
		return x;
	}



}
