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

	
	public void SetAsWinner() {
		//juice this: bounce
		m_Image.color = m_ActiveColour;
	}

	public void SetAsLoser() {
		//juice this: sad
		m_Image.color = m_InactiveColour;
	}
}
