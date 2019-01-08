using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeTile : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _frontCard;
    [SerializeField] private SpriteRenderer m_backCard;

    [SerializeField] private Sprite m_frontO;
    [SerializeField] private Sprite m_frontX;

    [SerializeField] private Sprite m_lockedO;
    [SerializeField] private Sprite m_lockedX;

    public bool revealed = false;
    [SerializeField] private int ownerIndex;

    private Sprite _lockedSprite;

    public int row;
    public int column;
    public int value = 2; //0 for o 1 for x 2 for empty

    public bool locked = false;

    //Move to audio manager
    [Header("Audio Clips")]
    [SerializeField] private AudioClip m_FlipAudio;
    [SerializeField] private AudioClip m_LockAudio;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCardValue(int _value)
    {
        value = _value;
        switch (value)
        {
            case 0: //o
                _frontCard.sprite = m_frontO;
                _lockedSprite = m_lockedO;
                break;
            case 1: //x
                _frontCard.sprite = m_frontX;
                _lockedSprite = m_lockedX;
                break;
        }
    }

    public void FlipCard(bool flip, int activePlayer = 0)
    {
        revealed = flip;
        _frontCard.gameObject.SetActive(revealed);
        m_backCard.gameObject.SetActive(!revealed);

        if (revealed)
        {
            GameObject.FindObjectOfType<AudioSource>().PlayOneShot(m_FlipAudio, 1f);
            _frontCard.color = GameObject.FindObjectOfType<PlayerManager>().activePlayer.PlayerColor;
        }
    }

    public void ResetCard()
    {
        locked = false;
        revealed = false;
        value = 2;
        _frontCard.gameObject.SetActive(false);
        m_backCard.gameObject.SetActive(true);
        _frontCard.color = Color.white;
        ownerIndex = -1;
    }

    public void LockCard(int _ownerIndex)
    {
        ownerIndex = _ownerIndex;
        locked = true;
        _frontCard.sprite = _lockedSprite;
            
        GameObject.FindObjectOfType<AudioSource>().PlayOneShot(m_LockAudio, 1f);

        
        if (_ownerIndex == 1)
        { //move this out
            _frontCard.color = new Color(0, 255, 143, 255);
        }

        //CHECK IF Winning value (other 2 in line revealed and matching) call on grid
        //coroutine
        //if yes end game
        //if no check if anymoves left
    }

    public void OnMouseDown()
    {
        SelectTile();
    }
    public void SelectTile()
    {
        if (!GameObject.FindObjectOfType<GameManager>().CanPlay()) return;
        if (locked) return;

        var activePlayer = GameObject.FindObjectOfType<PlayerManager>().activePlayerIndex;
        GameObject.FindObjectOfType<TicTacToeGrid>().MakeMove(column, row, value);
        if (!revealed)
        {
            FlipCard(true, activePlayer);
        }
        else
        {
            LockCard(activePlayer);
        }
    }
}
