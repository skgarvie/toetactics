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
    [SerializeField] private Text m_WinTextField;

    [SerializeField] private GameObject m_Overlay;

    [SerializeField] private Color m_TextDefaultColor; //Move to a text manager

    private bool _canPlay = false;
    // Use this for initialization
    void Start()
    {
        m_Overlay.SetActive(false);
        SetCanPlay(false);
        m_PlayerManager.SetStartingPlayer(0);
        StartCoroutine(BeginGame());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator BeginGame()
    {
        m_GridManager.SetupGrid();
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
            m_TextField.color = winningPlayer.PlayerColor;
            m_TextField.text = winningPlayer.PlayerName + " Wins Round!";

            winningPlayer.WinRound();
            var losingPlayer = m_PlayerManager.inactivePlayer;
            losingPlayer.LoseRound();

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

    private IEnumerator StartNewRound()
    {
        yield return new WaitForSeconds(1f);
        m_TextField.text = "";
        m_GridManager.FlipAll();
        yield return new WaitForSeconds(1f);
        var losingPlayerIndex = m_PlayerManager.activePlayerIndex == 0 ? 1 : 0;
        m_PlayerManager.SetStartingPlayer(losingPlayerIndex);

        StartCoroutine(BeginGame());
    }

    private void EndGame()
    {
        var winningPlayer = m_PlayerManager.activePlayer;
        m_Overlay.SetActive(true);
        m_TextField.text = "";
        m_WinTextField.color = winningPlayer.PlayerColor;
        m_WinTextField.text = winningPlayer.PlayerName + "\n Wins GAME!!!!"; ;
        winningPlayer.WinGame();
        var losingPlayer = m_PlayerManager.inactivePlayer;
        losingPlayer.LoseGame();

    }
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnHomeButton()
    {
        SceneManager.LoadScene("Main");
    }

    public void ToggleAudio()
    {
        var audioPlayer = GetComponent<AudioSource>();
        var musicPlayer = GameObject.Find("Music").GetComponent<AudioSource>();
        audioPlayer.mute = !audioPlayer.mute;
        musicPlayer.mute = !musicPlayer.mute;
    }
}
