using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Platform {


	public float moveSpeed;
	public MovingPosition[] movingPositions;

	List<Vector3> listOfMovingPositions = new List<Vector3>();

	void Awake()
	{
		base.GetOriginalMaterial();

		CheckMovingPositions();
		nextPositionId = 0;
		currentPositionId = 0;
	}

	void FixedUpdate()
	{
		UpdateCurrentPosition();
		MoveToNextPosition();
	}




	#region MovePlatform
	float timeInCurrentPosition;
	int currentPositionId;
	void UpdateCurrentPosition()
	{
		if(currentPositionId < listOfMovingPositions.Count)
		{
			if(currentPositionId == nextPositionId)
			{
				timeInCurrentPosition += Time.deltaTime;

				if(timeInCurrentPosition > movingPositions[currentPositionId].timeStaysInPosition)
				{
					if(nextPositionId < listOfMovingPositions.Count -1)
						nextPositionId++;
					else
						nextPositionId=0;

					timeInCurrentPosition = 0;
				}
				else
				{
					status = "Currently in position:" + currentPositionId;

				}
			}
		}
	}

	float distanceToNextPosition;
	int nextPositionId;
	string status = "";
	void MoveToNextPosition()
	{
		status.ToString();
		if(currentPositionId != nextPositionId)
		{
			Vector3 targetPosition = listOfMovingPositions[nextPositionId];
			
			distanceToNextPosition = (targetPosition - transform.position).magnitude;

			if(distanceToNextPosition < 0.01f)
			{
				currentPositionId = nextPositionId;
			} 
			else
			{
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
				status = "Moving to position: " + nextPositionId;
			}
		}
	}

	public void CheckMovingPositions()
	{

		foreach(MovingPosition movingPosition in movingPositions)
		{
			if(movingPosition.enabled)
			{
				listOfMovingPositions.Add(new Vector3(movingPosition.movePosition.position.x,
					movingPosition.movePosition.position.y,
					movingPosition.movePosition.position.z));
			}
		}

	}
	#endregion


	[System.Serializable]
	public class MovingPosition
	{
		public Transform movePosition;

		public float timeStaysInPosition;

		public bool enabled;

		public void EnableorDisableMovingPosition(bool enable)
		{
			enabled = enable;
		}
	}
}
