using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TicTacToeGrid m_GridManager;
    [SerializeField] private PlayerManager m_PlayerManager;
    [SerializeField] private Text m_TextField;
    [SerializeField] private Color m_TextDefaultColor; //Move to a text manager

    private bool _canPlay = false;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(BeginGame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator BeginGame()
    {
		SetCanPlay(false);
        m_GridManager.SetupGrid();
        m_PlayerManager.SetStartingPlayer();
        m_TextField.color = m_TextDefaultColor;
        m_TextField.text = "BEGIN!";  //make a text manager
		yield return new WaitForSeconds(1f);
		m_TextField.text = ""; 
        SetCanPlay(true);
    }

    public void SetCanPlay(bool play)
    {
        _canPlay = play;
    }

    public bool CanPlay()
    {
        return _canPlay;
    }

    public void CheckBoardState()
    {
        //make a text manager
        m_TextField.text = "";
        var boardState = m_GridManager.CheckBoard(); //add more states later
        if (boardState)
        { //true - game still going
            m_PlayerManager.SetActivePlayer();
        }
        else
        {
            var winningPlayer = m_PlayerManager.activePlayer;
            winningPlayer.WinRound();
            var losingPlayer = m_PlayerManager.inactivePlayer;
            losingPlayer.LoseRound();

            m_TextField.color = winningPlayer.PlayerColor;
            m_TextField.text = winningPlayer.PlayerName + " Wins Round!";
            if (losingPlayer.hp <= 0)
            {
                EndGame();
                //add delay
            }
            else
            {
                StartCoroutine(StartNewRound());
            }
        }
        //player 1 wins
        //player 2 wins
        //no more moves somehow
    }

	private IEnumerator StartNewRound() {
		yield return new WaitForSeconds(1f);
		m_TextField.text = "";
		m_GridManager.FlipAll();
		yield return new WaitForSeconds(1f);
		StartCoroutine(BeginGame());
	}
    private void EndGame()
    {
        var winningPlayer = m_PlayerManager.activePlayer;
        winningPlayer.WinGame();
        var losingPlayer = m_PlayerManager.inactivePlayer;
        losingPlayer.LoseGame();
        m_TextField.color = winningPlayer.PlayerColor;
        m_TextField.text = winningPlayer.PlayerName + " Wins GAME!";
    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
