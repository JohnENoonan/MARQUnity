using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class sliderClick : MonoBehaviour {

    Image img, tri;
    TextMeshPro text;

    public void Awake()
    {
        img = gameObject.GetComponent<Image>();
        tri = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        text = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();
    }

    public void onPress()
    {
        gameObject.SetActive(true);
        //StartCoroutine(spritefade(60, 0, 1));
        StartCoroutine(textfade(.25f,0,1));
    }

    public void onRelease()
    {
        StartCoroutine(textfade(.25f, 1, 0));
        StartCoroutine(wait(.25f));
        Color color = img.color;
        color.a = 0;
        img.color = color;
        tri.color = color;
        gameObject.SetActive(false);
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator textfade(float duration, int from, int to)
    {
        float counter = 0;
        
        // Get color
        //Color textcolor = text.renderer.material.color;
        Color imgcolor = img.color;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, counter / duration);
            Debug.Log(alpha);
            //render.color.a = alpha;
            //Change alpha only
            imgcolor.a = alpha;
            //textcolor.a = alpha;
            img.color = imgcolor;
            tri.color = imgcolor;
            //text.renderer.material.color = textcolor;
            //Wait for a frame
            yield return null;
        }
    }

    IEnumerator spritefade(float duration, int from, int to)
    {
        float counter = 0;
        // Get color
        Color color = img.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, counter / duration);
            Debug.Log(alpha);
            //render.color.a = alpha;
            //Change alpha only
            color.a = alpha;
            img.color = color;
            //Wait for a frame
            yield return null;
        }
    }


}
