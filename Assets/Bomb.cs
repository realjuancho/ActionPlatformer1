using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {


	
	public bool debugBomb;
	public float bombSpeed = 10.0f;
    public float minDistanceToObjective = 10.0f;
	public float minGroundDistance = 0.3f;
	public int LineResolution = 10;

    public float Velocidad = 15.0f;

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
        origen = transform.position;
		objetivo = p.floorBelowPosition;

        float distanciaObjetivo = (origen - p.floorBelowPosition).magnitude;

        if (distanciaObjetivo <= minDistanceToObjective)
        {
            if (debugBomb) Debug.Log("Distancia Objetivo:" + distanciaObjetivo);

            lineRenderer.positionCount = LineResolution +1;

            Vector3[] linePositions = new Vector3[lineRenderer.positionCount +1];


            for (int i = 0; i < lineRenderer.positionCount +1; i++)
            {
                float percent = (1.0f / lineRenderer.positionCount +1) * i;
                Vector3 newPosition = Vector3.Lerp(origen, objetivo, percent);


                linePositions[i] = newPosition;
            }



            lineRenderer.SetPositions(linePositions);
        }
        else
        {
            lineRenderer.positionCount = 0;
        }
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



}
