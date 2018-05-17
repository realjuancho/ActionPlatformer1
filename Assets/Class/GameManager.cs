using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	public Mode mode;
	public SplitScreen splitScreen;

	public Transform camTransform;
	PlayerInputManager im;
	public Player[] players;

	#region MonoBehaviour Methods
	void Awake()
	{
		im = gameObject.AddComponent<PlayerInputManager>();
		players = FindObjectsOfType<Player>();

		camTransform = Camera.main.transform;
	
		CheckModes();

		AssignPlayers();

		#if UNITY_EDITOR
			LoadDebugUI();
		#endif
	}

	void Update()
	{

		#region Handle Controllers
		int availableInputs = im.AvailableInputs;	
		for(int i = 0; i < availableInputs; i++)
		{
			if(players.Length > availableInputs)

				#if UNITY_EDITOR
					Debug.Log("Connect "+ (players.Length - availableInputs) + " Controllers");
				#endif
		}
		#endregion

		#if UNITY_EDITOR

			ShowDebugUI();

		#endif
	}
	#endregion

	#region Custom Methods
	void CheckModes()
	{
		
		mode = Mode.LOCAL;

		switch(players.Length)
		{
			case 2:
				splitScreen = SplitScreen.TWO;
			break;

			case 3:
				splitScreen = SplitScreen.THREE;
			break;

			case 4:
				splitScreen = SplitScreen.FOUR;
			break;

			default:
				splitScreen = SplitScreen.SINGLE;
			break;
		}
	}
	void AssignPlayers()
	{
		if(mode == Mode.LOCAL && splitScreen != SplitScreen.SINGLE)
		{
			Player.PlayerNumber playerNumber = Player.PlayerNumber.PLAYER1;

			foreach(Player p in players)
			{
				p.InitializeInputPlayer(playerNumber);
				p.gameObject.name+=("("+playerNumber+")");

				playerNumber++;
			}
		}
		else
		{
			Player p = players[0];
				p.InitializeInputPlayer(p.playerNumber);
				p.gameObject.name+=("("+p.playerNumber+")");
		}
	}
	#endregion


	///LOAD UI
	#if UNITY_EDITOR 

	Text txtPlayersOnScreen;
	Text txtControllersAvailable;
	void LoadDebugUI()
	{
		//txtPlayersOnScreen = (Text)GameObject.Find("txtPlayersOnScreen").GetComponent<Text>();
		//txtControllersAvailable = (Text)GameObject.Find("txtControllersAvailable").GetComponent<Text>();
	}

	void ShowDebugUI()
	{
//		txtPlayersOnScreen.text = players.Length.ToString();
//		txtControllersAvailable.text = im.AvailableInputs.ToString();

	}
	#endif



	public static class LevelEnvironment 
	{
		public static int gravity =1;
	    public static void SetGravity()	
	    {
	    	gravity = 1;
	    }


	}

	public enum SplitScreen { SINGLE, TWO, THREE, FOUR }
	public enum Mode { LOCAL , LAN , INTERNET};

}
