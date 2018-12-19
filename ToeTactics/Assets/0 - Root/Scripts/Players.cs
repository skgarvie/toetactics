using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour {

    [SerializeField] private GameObject m_playerOneImage;
    [SerializeField] private GameObject m_playerTwoImage;


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
		if(movesLeft == 1) {
		}
		if(movesLeft <= 0) { //switch Player
			StartCoroutine(SwitchActivePlayer());
		}
	}
	private IEnumerator SwitchActivePlayer() {
		//move the delay and blocker to grid
		playing = false;

		yield return new WaitForSeconds(0.3f);
		grid.FlipUnlocked();
		yield return new WaitForSeconds(0.3f);

		playing = true;

		movesLeft = 2;
		activePlayerIndex = activePlayerIndex == 1 ? 0 : 1;

		//juice this up
		m_playerOneImage.SetActive(activePlayerIndex == 0);
		m_playerTwoImage.SetActive(activePlayerIndex != 0);

	}
}
