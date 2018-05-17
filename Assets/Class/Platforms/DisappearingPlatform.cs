using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingPlatform : MovingPlatform {

    LineRenderer lineRenderer;

	void Start () {

        lineRenderer = GetComponent<LineRenderer>();

	}


	private void Update()
	{
        

        lineRenderer.positionCount = base.movingPositions.Length;

        int i = 0;
        foreach(MovingPosition movingPosition in base.movingPositions)
        {

            lineRenderer.SetPosition(i, new Vector3(movingPosition.movePosition.position.x,
                                                    movingPosition.movePosition.position.y,
                                                    movingPosition.movePosition.position.z
                                                   ));

            i++;
        }



	}
}
