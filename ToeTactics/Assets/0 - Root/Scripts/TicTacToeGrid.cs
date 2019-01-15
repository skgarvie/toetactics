using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TicTacToeGrid : MonoBehaviour
{

    //TODO make more state based
    //each round check: (players both moves)
    //1 available move
    //2 if none, check winning move
    //2 if none, draw
    //3 Set/switch to active player 
    //4 if allow play (so gridActive variable or something)
    //start new round

    //need a gamemanager

    [SerializeField] private List<TicTacToeTile> m_Tiles = new List<TicTacToeTile>();

    private int choicesX = 4;
    private int choicesO = 4;

    private int winningPattern;

    public bool canPlay = false;
    public int lastRowIndex = 0;
    public int lastColumnIndex = 0;
    public int lastValue = 2;

    [SerializeField] private AudioClip m_FlipAllAudio;
    // Use this for initialization
    public void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public bool CheckBoard()
    { //make more states. currently true means game still going
        var state = true;
        // check column for win
        var column = m_Tiles.Where(tile => tile.column == lastColumnIndex);

        if (column.Any(tile => tile.value != lastValue) || column.Any(tile => !tile.revealed))
        {
            //column not equal
            state = true;
        }
        else
        {
            //column equal
            state = false;
            Debug.Log("Column Matches");
            return state;
        }

        var row = m_Tiles.Where(tile => tile.row == lastRowIndex);
        if (row.Any(tile => tile.value != lastValue) || row.Any(tile => !tile.revealed))
        {
            //row not equal
            state = true;
        }
        else
        {
            //row equal
            state = false;
            Debug.Log("Row Matches");
            return state;
        }

        if (winningPattern == 3) //diagonal 1
        {
            var tile1 = m_Tiles.Where(tile => tile.column == 0 && tile.row == 0).First();
            var tile2 = m_Tiles.Where(tile => tile.column == 1 && tile.row == 1).First();
            var tile3 = m_Tiles.Where(tile => tile.column == 2 && tile.row == 2).First();
            if (((tile1.value == tile2.value) && (tile2.value == tile3.value)) && (tile1.revealed && tile2.revealed && tile3.revealed))
            {
                //diagonal equal
                state = false;
                Debug.Log("Diagonal Matches");
                return state;
            }
        }

        if (winningPattern == 4) //diagonal 2
        {
            var tile1 = m_Tiles.Where(tile => tile.column == 0 && tile.row == 2).First();
            var tile2 = m_Tiles.Where(tile => tile.column == 1 && tile.row == 1).First();
            var tile3 = m_Tiles.Where(tile => tile.column == 2 && tile.row == 0).First();
            if (((tile1.value == tile2.value) && (tile2.value == tile3.value)) && (tile1.revealed && tile2.revealed && tile3.revealed))
            {
                //diagonal equal
                state = false;
                Debug.Log("Diagonal Matches");
                return state;
            }
        }

        return state;
    }



    [ContextMenu("Setup Grid")]
    public void SetupGrid()
    {
        GameObject.FindObjectOfType<GameManager>().SetCanPlay(false);
        Debug.Log("Setting up Grid");

        var winningValue = Random.Range(0, 1); //randomly choose x or o as winner
        winningPattern = Random.Range(0, 7); //randomly choose 1 of 8 winning patters 0-2 column 3-4 diagonal 5-7 row

        Debug.Log("Winning Value: " + winningValue);
        Debug.Log("Winning Pattern: " + winningPattern);

        if (winningValue == 0)
        {
            choicesO -= 3;
            choicesX += 1;
        }
        else
        {
            choicesX -= 3;
            choicesO += 1;
        }

        if (winningPattern <= 2)
        {
            var column = m_Tiles.Where(tile => tile.column == winningPattern);
            foreach (var tile in column)
            {
                tile.SetCardValue(winningValue);
            }
        }
        else if (winningPattern == 3) //diagonal 1
        {
            m_Tiles.Where(tile => tile.column == 0 && tile.row == 0).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 1 && tile.row == 1).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 2 && tile.row == 2).First().SetCardValue(winningValue);

        }
        else if (winningPattern == 4) //diagonal 2
        {
            m_Tiles.Where(tile => tile.column == 0 && tile.row == 2).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 1 && tile.row == 1).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 2 && tile.row == 0).First().SetCardValue(winningValue);
        }
        else
        {
            var row = m_Tiles.Where(tile => tile.row == winningPattern - 5);
            foreach (var tile in row)
            {
                tile.SetCardValue(winningValue);
            }
        }

        //Empty tiles
        var empty = m_Tiles.Where(tile => tile.value == 2);
        foreach (var tile in empty)
        {
            if (choicesO > 0 && choicesX > 0)
            {
                var randomMove = Random.Range(0, 1); //randomly choose x or o as winner
                tile.SetCardValue(randomMove);
                choicesO = randomMove == 0 ? choicesO - 1 : choicesO;
                choicesX = randomMove == 1 ? choicesX - 1 : choicesX;
            }
            else
            {
                if (choicesO > 0)
                {
                    tile.SetCardValue(0);
                    choicesO--;
                }
                else if (choicesX > 0)
                {
                    tile.SetCardValue(1);
                    choicesX--;
                }
                else
                { //Always will be a blank or will it? is it fine to have multiple winners for the game? maybe
                    Debug.Log("hmmmmm");
                }
            }
        }

    }

    [ContextMenu("Reveal Grid")]
    public void RevealGrid()
    {
        foreach (var tile in m_Tiles)
        {
            tile.FlipCard(true);
        }
    }


    public void FlipAll()
    {
        choicesX = 4;
        choicesO = 4;
        foreach (var tile in m_Tiles)
        {
            tile.ResetCard();
        }
    }

    public void FlipUnlocked()
    {

        var unlocked = m_Tiles.Where(tile => !tile.locked && tile.revealed);
        if(unlocked.Count() >=  1) {
            GameObject.FindObjectOfType<GameManager>().GetComponent<AudioSource>().PlayOneShot(m_FlipAllAudio, 1f);
        }

        foreach (var tile in unlocked)
        {
            tile.FlipCard(false);
        }
    }

    public void MakeMove(int col, int row, int value)
    {
        GameObject.FindObjectOfType<GameManager>().SetCanPlay(false);
        GameObject.FindObjectOfType<PlayerManager>().MakeMove();
        lastRowIndex = row;
        lastColumnIndex = col;
        lastValue = value;
        GameObject.FindObjectOfType<GameManager>().CheckBoardState();
    }

}
