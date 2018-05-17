using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public bool DebugCamera;
	public bool CameraFollows;
	public bool RequestUpdatePlayers;

	public float smoothRate = 0.25f;
	public Vector3 cameraOffset;

	Player[] players;
	Vector3 cameraTarget;

	// Use this for initialization
	void Awake () {

		UpdatePlayers();


	}

	public void UpdatePlayers()
	{
		players = GameObject.FindObjectsOfType<Player>();
	}

	// Update is called once per frame
	void Update () {


		if(RequestUpdatePlayers)
		{
			RequestUpdatePlayers = false;
			UpdatePlayers();
		}

		if(CameraFollows)
		{
			int playerCount = players.Length;
			if(playerCount == 1)
			{
				cameraTarget = players[0].transform.position;
			}
			else
			{
				UpdateCentroid();
			}
		}

		UpdateCameraTarget();
	}


	void LateUpdate()
	{

		Vector3 cameraTargetPosition = cameraTarget + cameraOffset;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, cameraTargetPosition, smoothRate);
		transform.position = smoothedPosition;


	}


	void UpdateCameraTarget()
	{
		if(DebugCamera) 
		{
			Debug.DrawLine(transform.position, cameraTarget, Color.red);


		}
	}

	void UpdateCentroid()
	{
		int playerCount = players.Length;
		switch(playerCount)
			{
				case 2:
					cameraTarget = FindCentroid(players[0].transform.position, 
						players[1].transform.position);
				break;

				case 3:
					cameraTarget = FindCentroid(players[0].transform.position,
						players[1].transform.position,
						players[2].transform.position
						);
				break;

				case 4:
					cameraTarget = FindCentroid(players[0].transform.position,
						players[1].transform.position,
						players[2].transform.position,
						players[3].transform.position
						);
				break;

			}
	}


	Vector3 FindCentroid(Vector3 a, Vector3 b)
	{

		Vector3 centroid = new Vector3(
				(a.x + b.x)/2
				,(a.y + b.y)/2
				,(a.z + b.z)/2
			);

		Debug.Log(centroid);

		return centroid;
	}


	Vector3 FindCentroid(Vector3 a, Vector3 b,Vector3 c)
	{
		Vector3 centroid = new Vector3(
				(a.x + b.x + c.x)/3
				,(a.y + b.y + c.y)/3
				,(a.z + b.z +c.z)/3			
			);
		return centroid;
	}


	Vector3 FindCentroid(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
	{
		Vector3 centroid = new Vector3(
				(a.x + b.x + c.x + d.x)/4
				,(a.y + b.y + c.y + d.y)/4
				,(a.z + b.z + c.z + d.z)/4
			);
		return centroid;

	}
}
