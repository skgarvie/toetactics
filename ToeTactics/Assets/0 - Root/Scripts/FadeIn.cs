using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    public float fadeDuration = 1;
	public float fadeDelay = 0;

    // Use this for initialization
    void Start()
    {
		StartCoroutine(FadeInText());
    }

    // Update is called once per frame
    void Update()
    {

    }


    public IEnumerator FadeInText()
    {

		yield return new WaitForSeconds(fadeDelay);
        var t = 0.0f;

        var startColour = GetComponent<Text>().color;
        var endColour = new Color(startColour.r, startColour.g, startColour.b, 1f);

        while (t < 1.0f)
        {
            t += Time.deltaTime / fadeDuration;
            var newColor = Color.Lerp(startColour, endColour, t);
            GetComponent<Text>().color = newColor;
            yield return null;
        }

        GetComponent<Text>().color = endColour;
    }
}
