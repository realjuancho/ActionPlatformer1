using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {


	
	public bool StartMoving;
	public float bombSpeed = 10.0f;

	public float minGroundDistance = 0.3f;
	public int LineResolution = 10;


	LineRenderer lineRenderer;
	Vector3 origen;
	Vector3 objetivo;
	Vector3 extents;


	Player p;

	// Use this for initialization
	void Awake () {

		origen = transform.position;

		extents = GetComponentInChildren<MeshRenderer>().bounds.extents;

		p = FindObjectOfType<Player>();

		lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame


	void Update () {


		UpdateLineRenderer();

		CheckGround();
	}


	void UpdateLineRenderer()
	{

		objetivo = p.floorBelowPosition;

		lineRenderer.positionCount = LineResolution +1;
		
		Vector3[] linePositions = new Vector3[lineRenderer.positionCount];


		for(int i = 0; i < lineRenderer.positionCount; i++)
		{
			
			float percent = (1.0f / lineRenderer.positionCount) *i;


			Vector3 newPosition = Vector3.Lerp(origen, objetivo, percent);
				
//					newPosition.y = Cuadratica(
//						AFrom(objetivo.x, objetivo.y, origen.x, origen.y)
//						,1
//						,1
//						, percent);
		
			linePositions[i] = newPosition; 
		}

		Vector3 lastPosition = new Vector3(objetivo.x, objetivo.y, objetivo.z);

		linePositions[linePositions.Length-1] = lastPosition;

		lineRenderer.SetPositions(linePositions);





	}



	void CheckGround()
	{
		Vector3 transformOrigin = transform.position;
		transformOrigin += Vector3.down * extents.y;
		
		Ray groundRay = new Ray(transformOrigin, Vector3.down * minGroundDistance);
		RaycastHit groundRayHit;

		Physics.Raycast(groundRay, out groundRayHit, minGroundDistance);
		Debug.DrawRay(transformOrigin, Vector3.down * minGroundDistance, Color.blue);

		Collider col = groundRayHit.collider;

		if(col)
		{
			if(groundRayHit.collider.gameObject.tag == "Floor")
			{
				//Explotar bomba
			}
		}

	}

	public float a;
	float AFrom(float x, float y, float h, float k)
	{
		a = (y-k)/Mathf.Pow(x-h,2);
		return a;
	}

	public float b;
	float BFrom(float x, float y, float a, float k)
	{
		return 1.0f;
	}

	float Cuadratica(float a, float b, float c, float x)
	{
		x = (a * Mathf.Pow(x,2)) + (b * x) + c;
		return x;
	}

}
