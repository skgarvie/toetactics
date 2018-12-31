using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private TicTacToeGrid m_GridManager;
	[SerializeField] private Players  m_PlayerManager;
	private bool _canPlay = false;
	// Use this for initialization
	void Start () {
		m_GridManager.SetupGrid();
		m_PlayerManager.SetStartingPlayer();
		SetCanPlay(true);
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
		var boardState = m_GridManager.CheckBoard(); //add more states later
		if(boardState) { //true - game still going
			m_PlayerManager.SetActivePlayer();
		} else
		{
			Debug.Log("game done");
		}
		//player 1 wins
		//player 2 wins
		//no more moves somehow
		
	}


}
