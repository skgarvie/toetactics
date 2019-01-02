using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    [SerializeField] private Image m_Image;
	public string PlayerName;
	public Color PlayerColor;	
	[SerializeField] private Color m_ActiveColour;
	[SerializeField] private Color m_InactiveColour;
    [SerializeField] private Image m_HPBar;
	public int hp = 100;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SetAsActive() {
		//juice this
		m_Image.color = m_ActiveColour;
	}

	public void SetAsInactive() {
		//juice this
		m_Image.color = m_InactiveColour;
	}

	public void LoseRound() {
		//juice this: sad
		hp = hp - 20 <= 0 ? 0 : hp -20;
		m_HPBar.GetComponent<RectTransform>( ).SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal, hp);
		
		m_Image.color = m_InactiveColour;
	}
	
	public void WinRound() {
		//juice this: bounce
		m_Image.color = m_ActiveColour;
	}

	public void LoseGame() {
		//notify game manager?
	}

	public void WinGame() {
		
	}
}
