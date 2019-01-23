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
    [SerializeField] private int m_DmgAmount = 35;
    [SerializeField] private Fireball m_Fireball;


    public int hp = 100;
    public int visualHp = 100;

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
        StartCoroutine(TakeDamage());
    }

    public void WinRound()
    {
        m_Animator.SetTrigger("attack");
        m_Fireball.Fire();
        //juice this: bounce
        // flashCoroutine = StartCoroutine(Flash(3));
    }

    public void LoseGame()
    {
        SetAsActive();
        StartCoroutine(TakeDamage(true));

        //notify game manager?
    }

    public void WinGame()
    {
        SetAsActive();
        m_Animator.SetTrigger("winGame");

    }
    private void SetHPBarOpacity(float opacity)
    {
        var tempColor = m_HPBar.color;
        tempColor.a = opacity;
        m_HPBar.color = tempColor;
    }

    private IEnumerator TakeDamage(bool lethal = false)
    {
        hp = hp - m_DmgAmount <= 0 ? 0 : hp - m_DmgAmount;

        yield return new WaitForSeconds(0.9f);
        if (lethal)
        {
            m_Animator.SetTrigger("loseGame");

        }
        else
        {
            m_Animator.SetTrigger("isHit");

        }

        StartCoroutine(LoseHealth(m_DmgAmount));


    }
    private IEnumerator LoseHealth(int amount)
    {
        var startHp = visualHp;
        visualHp = visualHp - amount <= 0 ? 0 : visualHp - amount;
        var time = 1f;
        var t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / time;

            var newHp = Mathf.Lerp(startHp, visualHp, t);
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
