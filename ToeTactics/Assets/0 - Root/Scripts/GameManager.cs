using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField] private TicTacToeGrid m_GridManager;
	[SerializeField] private PlayerManager  m_PlayerManager;
	[SerializeField] private Text  m_TextField;

	private bool _canPlay = false;
	// Use this for initialization
	void Start () {
		m_GridManager.SetupGrid();
		m_PlayerManager.SetStartingPlayer();
		SetCanPlay(true);
		m_TextField.color = Color.green;
		m_TextField.text = "BEGIN!";  //make a text manager
		Debug.Log("game starting");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetCanPlay(bool play) {
		_canPlay = play;
	}

	public bool CanPlay() {
		return _canPlay;
	}

	public void CheckBoardState() {
		//make a text manager
		m_TextField.text = "";
		var boardState = m_GridManager.CheckBoard(); //add more states later
		if(boardState) { //true - game still going
			m_PlayerManager.SetActivePlayer();
		} else
		{
			var winningPlayer = m_PlayerManager.activePlayer;
			m_TextField.color = winningPlayer.PlayerColor;
			m_TextField.text = winningPlayer.PlayerName+" Wins!";
			Debug.Log(winningPlayer.PlayerName+" Wins!!!!!!");
			Debug.Log("game done");
		}
		//player 1 wins
		//player 2 wins
		//no more moves somehow
		
	}

	public void ResetGame() {
		SceneManager.LoadScene( SceneManager.GetActiveScene().name );
	}

}
