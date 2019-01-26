using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStates : MonoBehaviour
{

    [SerializeField] private Sprite m_ActiveImage;
    [SerializeField] private Sprite m_InactiveImage;

    public bool state = true;

    private Image _image;
    // Use this for initialization

    void Awake()
    {
        _image = GetComponent<Image>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleState()
    {
        state = !state;
        if (state)
        {
            _image.sprite = m_ActiveImage;
        }
        else
        {
            _image.sprite = m_InactiveImage;
        }
    }
}
