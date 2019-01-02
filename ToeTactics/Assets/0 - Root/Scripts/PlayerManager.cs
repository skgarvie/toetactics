using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {


    [SerializeField] private List <Player> m_Players = new List<Player>();

	public Player activePlayer = null;
	public int activePlayerIndex = 0;
	public  int maxMoves = 2;
	public int movesLeft = 2;

	public bool playing = false;

	private TicTacToeGrid grid;
	// Use this for initialization
	void Start () {
		grid = GameObject.FindObjectOfType<TicTacToeGrid>();
		playing = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MakeMove() {	
		movesLeft--;
	}

	public void SetActivePlayer() {
		if(movesLeft <= 0) { //switch Player
			StartCoroutine(SwitchActivePlayer());
		} else {
			GameObject.FindObjectOfType<GameManager>().SetCanPlay(true);
		}
	}

	public void SetStartingPlayer() {
		activePlayer = m_Players[activePlayerIndex];
		activePlayer.SetAsActive();
	}

	private IEnumerator SwitchActivePlayer() {
		//move the delay and blocker to grid

		yield return new WaitForSeconds(0.3f);
		grid.FlipUnlocked();
		yield return new WaitForSeconds(0.3f);

		movesLeft = 2;
		activePlayerIndex = activePlayerIndex == 1 ? 0 : 1; //make take more than two players later
		activePlayer = m_Players[activePlayerIndex];
		activePlayer.SetAsActive();
		var otherPlayers = m_Players.Where((player, i) => i != activePlayerIndex).ToList();
		foreach (var player in otherPlayers)
		{
			player.SetAsInactive();
		}
		//juice this up
		// if(activePlayerIndex == 0) {
		// 	m_playerOneImage.color = Color.white;
		// 	m_playerTwoImage.color = new Color(255,255,255,0.4f);
		// } else {
		// 	m_playerOneImage.color = new Color(255,255,255,0.4f);
		// 	m_playerTwoImage.color = Color.white;
		// }

		GameObject.FindObjectOfType<GameManager>().SetCanPlay(true);

	}
}
