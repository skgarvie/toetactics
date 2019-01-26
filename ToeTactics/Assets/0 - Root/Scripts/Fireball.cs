using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    [SerializeField] private Vector3 m_StartPosition;
    [SerializeField] private Vector3 m_EndPosition;

    [SerializeField] private float m_Speed = 1;
    [SerializeField] private GameObject m_Shadow;

    private SpriteRenderer m_Sprite;


    private void Awake()
    {
        m_Sprite = GetComponent<SpriteRenderer>();
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        StartCoroutine(Travel());
    }
    private IEnumerator Travel()
    {
        m_Sprite.enabled = true;
        m_Shadow.SetActive(true);
        var t = 0.0f;

        while (t < 1.0f)
        {
            t += Time.deltaTime / m_Speed;

            var newPos = Vector3.Lerp(m_StartPosition, m_EndPosition, t);
            transform.position = newPos;
            yield return null;
        }

        m_Sprite.enabled = false;
        m_Shadow.SetActive(false);

        yield return new WaitForSeconds(0.1f);
        transform.position = m_StartPosition;

    }
}
