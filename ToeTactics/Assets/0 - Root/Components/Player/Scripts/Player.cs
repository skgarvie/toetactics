using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_Sprite;

    [SerializeField] private Animator m_Animator;

    public string PlayerName;
    public Color PlayerColor;
    [SerializeField] private Color m_ActiveColour;
    [SerializeField] private Color m_InactiveColour;
    [SerializeField] private Image m_HPBar;
    [SerializeField] private int m_DmgAmount = 25;

    public int hp = 100;

    private Coroutine flashCoroutine = null;
    private float hpFade = 0.4f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetAsActive()
    {
        m_Sprite.color = m_ActiveColour;
        SetHPBarOpacity(1f);
    }

    public void SetAsInactive()
    {
        m_Sprite.color = m_InactiveColour;

        SetHPBarOpacity(hpFade);

    }

    public void LoseRound()
    {
        SetAsActive();
        m_Animator.SetTrigger("isHit");
        StartCoroutine(LoseHealth(m_DmgAmount));
    }

    public void WinRound()
    {
        m_Animator.SetTrigger("attack");
        //juice this: bounce
        // flashCoroutine = StartCoroutine(Flash(3));
    }

    public void LoseGame()
    {
        SetAsActive();
        m_Animator.SetTrigger("loseGame");
        //notify game manager?
    }

    public void WinGame()
    {
        SetAsActive();
    }
    private void SetHPBarOpacity(float opacity)
    {
        var tempColor = m_HPBar.color;
        tempColor.a = opacity;
        m_HPBar.color = tempColor;
    }

    private IEnumerator LoseHealth(int amount)
    {
        var startHp = hp;
        hp = hp - amount <= 0 ? 0 : hp - amount;
        var time = 1f;
        var t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / time;

            var newHp = Mathf.Lerp(startHp, hp, t);
            m_HPBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newHp);
            yield return null;
        }

        m_HPBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hp);
    }

    private IEnumerator Bounce(int times)
    {
        var startPos = m_Sprite.gameObject.transform.position;
        var endPos = new Vector3(m_Sprite.gameObject.transform.position.x, m_Sprite.gameObject.transform.position.y + 30, m_Sprite.gameObject.transform.position.z);

        var time = 0.4f;
        var t = 0.0f;
        // for (var i = 0; i < times; i++)
        while (true)
        {
            time = 0.3f;
            t = 0.0f;

            while (t < 1.0f)
            {
                t += Time.deltaTime / time;

                var newPos = Vector3.Lerp(startPos, endPos, t);
                m_Sprite.gameObject.transform.position = newPos;
                yield return null;
            }

            m_Sprite.gameObject.transform.position = startPos;
        }

    }

}
