using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour {


	public bool jumps;
	public int moveSpeed = 10;
	public int jumpPower = 5;
	public int gravityFactor = 10;
	public float airControl = 0.5f;

	public Vector3 floorBelowPosition;

	public PlayerNumber playerNumber;

	public PlayerInput playerInput ;

	CharacterController controller;
	Weapon attack;

	void Awake()
	{
		controller = GetComponent<CharacterController>();
		attack = GetComponent<Weapon>();
	}

	void Update()
	{

		CheckGround();


	}

	void FixedUpdate()
	{
		Vector3 v = new Vector3(float.MinValue, 0, float.MaxValue);

		v.Normalize();

		controller.Move(v);

		#region Handle Player Fall
		if(!controller.isGrounded)
		{
			 controller.Move(Vector3.down 
			    * GameManager.LevelEnvironment.gravity 
			    * gravityFactor
			 	* Time.deltaTime);
		}
		#endregion



		//Debug.Log(playerNumber +":"+ controller.isGrounded);
	}

	public void Jump()
	{
		if(controller.isGrounded)
		{
			controller.Move(Vector3.up 
					* jumpPower 
					);

		} 
	}

	bool[] groundCheck;
	void CheckGround()
	{
		groundCheck = new[]{false, false, false, false, false};
		for(int i=0; i<5; i++)
		{
			Vector3 offset;

			if(i==0) offset = new Vector3(controller.radius,0,controller.radius);
			else if(i==1) offset = new Vector3(-controller.radius,0,controller.radius);
			else if(i==2) offset = new Vector3(-controller.radius,0,-controller.radius);
			else if(i==3) offset = new Vector3(controller.radius,0,-controller.radius);
			else offset = Vector3.zero;

			float maxDistance=0.3f;
			Vector3 origin = transform.position + offset;
			origin.y += controller.center.y - (controller.height/2);
			Vector3 direction = Vector3.down * maxDistance;
			Debug.DrawRay(origin, direction);
			Ray rayCheckFloor = new Ray(origin,direction);
			RaycastHit hitCheckFloor;
			if(Physics.Raycast(rayCheckFloor, out hitCheckFloor))
			{
				if(hitCheckFloor.collider)
				{
					if(hitCheckFloor.distance <= maxDistance)
					{
						groundCheck[i] = true;
					}

					if(i==4)
					{
						if(hitCheckFloor.collider.gameObject.isStatic)
						{
							floorBelowPosition = hitCheckFloor.point;


						}
					}
					else
					{
						floorBelowPosition = Vector3.zero;
					}
				}

			}
		}
		//Debug.Log("Ground Check {"+groundCheck[0]+"," + groundCheck[1]+"," + groundCheck[2]+"," + groundCheck[3]+"}" );
	}

	public void MovePlayer(Vector3 direccionMovimiento)
	{

		
		if(!controller.isGrounded)
		{
			direccionMovimiento *= airControl; 
		}

		controller.Move(direccionMovimiento 
			* moveSpeed 
			* Time.deltaTime);
		
	}

	public void Attack()
	{

		Debug.Log("Pew");
		attack.Shoot();
	}




	public class PlayerInput : MonoBehaviour
	{
		public string xAxis_LeftJoystick;
		public string yAxis_LeftJoystick;

		public string xAxis_RightJoystick;
		public string yAxis_RightJoystick;

		public KeyCode jumpKey;
		public KeyCode attackKey;
		public KeyCode deflectKey;

		public string jumpButton;
		public string attackButton;
		public string deflectButton;
	}


	/// <summary>
	/// Initializes the input when multiplayer is played locally by 1 player.
		/// </summary>

	public void InitializeInputPlayer(PlayerNumber playerNumber)
		{
			this.playerNumber = playerNumber;

			if(playerInput == null)
				playerInput = gameObject.AddComponent<PlayerInput>();

			switch(playerNumber)

			{
				case PlayerNumber.PLAYER1:
					playerInput.xAxis_LeftJoystick = InputHash.P1_LStick_LeftRight;
					playerInput.yAxis_LeftJoystick = InputHash.P1_LStick_UpDown;

					playerInput.xAxis_RightJoystick = InputHash.P1_RStick_LeftRight;
					playerInput.yAxis_RightJoystick = InputHash.P1_RStick_UpDown;


					playerInput.jumpKey = KeyCode.Space; playerInput.jumpButton = InputHash.P1_Cross;
					playerInput.attackKey = KeyCode.Mouse0; playerInput.attackButton = InputHash.P1_R2_Button;
					playerInput.deflectKey = KeyCode.Mouse1; playerInput.deflectButton = InputHash.P1_L2_Button;
				break;

				case PlayerNumber.PLAYER2:
					playerInput.xAxis_LeftJoystick = InputHash.P2_LStick_LeftRight;
					playerInput.yAxis_LeftJoystick = InputHash.P2_LStick_UpDown;

					playerInput.xAxis_RightJoystick = InputHash.P2_RStick_LeftRight;
					playerInput.yAxis_RightJoystick = InputHash.P2_RStick_UpDown;

					playerInput.jumpKey = KeyCode.None; playerInput.jumpButton = InputHash.P2_Cross;
					playerInput.attackKey = KeyCode.None; playerInput.attackButton = InputHash.P2_R2_Button;
					playerInput.deflectKey = KeyCode.None; playerInput.deflectButton = InputHash.P2_L2_Button;
				break;

				case PlayerNumber.PLAYER3:
					playerInput.xAxis_LeftJoystick = InputHash.P3_LStick_LeftRight;
					playerInput.yAxis_LeftJoystick = InputHash.P3_LStick_UpDown;

					playerInput.xAxis_RightJoystick = InputHash.P3_RStick_LeftRight;
					playerInput.yAxis_RightJoystick = InputHash.P3_RStick_UpDown;

					playerInput.jumpKey = KeyCode.None; playerInput.jumpButton = InputHash.P3_Cross;
					playerInput.attackKey = KeyCode.None; playerInput.attackButton = InputHash.P3_R2_Button;
					playerInput.deflectKey = KeyCode.None; playerInput.deflectButton = InputHash.P3_L2_Button;
				break;

				case PlayerNumber.PLAYER4:

					playerInput.xAxis_LeftJoystick = InputHash.P4_LStick_LeftRight;
					playerInput.yAxis_LeftJoystick = InputHash.P4_LStick_UpDown;

					playerInput.xAxis_RightJoystick = InputHash.P4_RStick_LeftRight;
					playerInput.yAxis_RightJoystick = InputHash.P4_RStick_UpDown;

					playerInput.jumpKey = KeyCode.None; playerInput.jumpButton = InputHash.P4_Cross;
					playerInput.attackKey = KeyCode.None; playerInput.attackButton = InputHash.P4_R2_Button;
					playerInput.deflectKey = KeyCode.None; playerInput.deflectButton = InputHash.P4_L2_Button;

				break;
			}
		}

	public enum PlayerNumber { PLAYER1, PLAYER2, PLAYER3, PLAYER4 };
}
