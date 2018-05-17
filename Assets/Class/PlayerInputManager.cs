using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {


		public GameManager gameManager;

		public int AvailableInputs;

		public float aimSensitivity = 0.3f;

		void Awake()
		{
		   gameManager = FindObjectOfType<GameManager>();
		}

		void EnablePlayerInput()
		{
			int PlayerQty = gameManager.players.Length;

			foreach(Player p in gameManager.players)
			{	
				p.InitializeInputPlayer(p.playerNumber);
			}

		}
	
	 	void Update()
	 	{

	 		PlayerInput();

		}

		void PlayerInput()
		{

			#region Input in Gameplay
	 			foreach(Player p in gameManager.players)
		 		{


		 			#region handle Jump

					bool JumpKey = Input.GetKeyDown(p.playerInput.jumpKey);
					bool JumpButton = Input.GetButtonDown(p.playerInput.jumpButton);

		 			if(JumpKey || JumpButton)
		 			{
		 				p.Jump();
		 			}
		 		
		 			#endregion

		 			//References to main camera to Make Input relative to Camera
					Transform camTransform = gameManager.camTransform;
					Vector3 camRelativeForward = Vector3.Scale(camTransform.forward,new Vector3(1,0,1)).normalized;

					#region Handle Turn/Move With Joystick

					//rotacion
					Transform pointerTransform = p.transform.Find("Pointer");
					Transform bodyTransform = p.transform.Find("Body");

					Vector3 objective = pointerTransform.position;

		 		 	objective.x = Input.GetAxis(p.playerInput.xAxis_RightJoystick);
		 		 	objective.y = 0;
		 		 	objective.z = Input.GetAxis(p.playerInput.yAxis_RightJoystick);
					objective = -objective.x * camRelativeForward 
		 		  				- objective.z * camTransform.right;
		 		  		
					pointerTransform.localPosition = objective;


					Vector3 pointerHeading = (pointerTransform.position - p.transform.position);
					float distance = pointerHeading.magnitude;

					Vector3 pointingDirection = pointerHeading / distance;
					Debug.DrawRay(p.transform.position, pointingDirection * distance, Color.red);



					if(distance > aimSensitivity)
					{
						Vector3 pointerRotation = pointingDirection; 
						pointerRotation.y -= 90;
						pointerTransform.LookAt(pointerTransform.position + pointerRotation );	
					}




					Vector3 bodyHeading = (pointerTransform.position - p.transform.position);
					distance = bodyHeading.magnitude;
					pointingDirection = bodyHeading / distance;

					if(distance > aimSensitivity)
					{
						Vector3 bodyRotation = pointingDirection; 
						//bodyRotation.y -= 90;
						bodyTransform.LookAt(bodyTransform.position + bodyRotation );
					}


					//Movimiento
					Vector3 direccionMovimiento = new Vector3();
					direccionMovimiento.x = Input.GetAxis(p.playerInput.xAxis_LeftJoystick);
					direccionMovimiento.y = 0;
					direccionMovimiento.z = Input.GetAxis(p.playerInput.yAxis_LeftJoystick);

					direccionMovimiento = direccionMovimiento.x * camRelativeForward 
		 		 			+ direccionMovimiento.z * camTransform.right;

					if (direccionMovimiento.magnitude > 1f) 
						direccionMovimiento.Normalize();

					direccionMovimiento = p.transform.InverseTransformDirection(direccionMovimiento);
					direccionMovimiento = Vector3.ProjectOnPlane(direccionMovimiento, Vector3.up);


					Debug.DrawRay(p.transform.position, direccionMovimiento, Color.green);


					p.MovePlayer(direccionMovimiento);

		 		 	#endregion

		 		 	#region handle shooting

		 		 	if(Input.GetButtonDown(p.playerInput.attackButton))
		 			{

						p.Attack();

		 			}


	 		 	#endregion

	 			}
	 			#endregion


	 		#region Handle Available Inputs

	 			AvailableInputs = 1 + Input.GetJoystickNames().Length;
	 		
	 		#endregion


			#if UNITY_EDITOR
		 		string[] joystickNames = Input.GetJoystickNames();


		 		for(int i=0; i < joystickNames.Length; i++)
		 		{
					//string joystickName = joystickNames[i];


//		 			Debug.Log("Joystick "+ i +" Name: " + joystickName);

		 		}
			#endif

		}
	
	}


