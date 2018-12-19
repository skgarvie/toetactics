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

    [SerializeField] private List<TicTacToeTile> m_Tiles = new List<TicTacToeTile>();

    private int choicesX = 4;
    private int choicesO = 4;

    // Use this for initialization
    void Start()
    {
        SetupGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("Setup Grid")]
    public void SetupGrid()
    {
        Debug.Log("Setting up Grid");

        var winningValue = Random.Range(0, 1); //randomly choose x or o as winner
        var winningPattern = Random.Range(0, 7); //randomly choose 1 of 8 winning patters 0-2 column 3-4 diagonal 5-7 row

        //make one tile blank?
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

        if (winningPattern <= 2) //columns
        {
            Debug.Log("Columns");

            var column = m_Tiles.Where(tile => tile.column == winningPattern);
            foreach (var tile in column)
            {
                tile.SetCardValue(winningValue);
            }
        }
        else if (winningPattern == 3) //diagonal 1
        {
            Debug.Log("Diagonal");
            m_Tiles.Where(tile => tile.column == 0 && tile.row == 0).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 1 && tile.row == 1).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 2 && tile.row == 2).First().SetCardValue(winningValue);

        }
        else if (winningPattern == 4) //diagonal 2
        {
            Debug.Log("Diagonal");
            m_Tiles.Where(tile => tile.column == 0 && tile.row == 2).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 1 && tile.row == 1).First().SetCardValue(winningValue);
            m_Tiles.Where(tile => tile.column == 2 && tile.row == 0).First().SetCardValue(winningValue);
        }
        else //rows
        {
            Debug.Log("Rows");
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


    public void FlipUnlocked()
    {
		var unlocked = m_Tiles.Where(tile => !tile.locked && tile.revealed);
        foreach (var tile in unlocked)
        {
            tile.FlipCard(false);
        }
    }
	
}
