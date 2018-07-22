using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickBadge : MonoBehaviour {

    public GameObject badgeInfo;
    public GameObject infoBack;
    string badgename;

    void Awake()
    {
        badgename = gameObject.transform.parent.name + "_info";
    }

	void OnMouseDown()
    {
        Debug.Log("Clicked " + badgename);
        badgeInfo.SetActive(true);
        badgeInfo.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Badges/" + badgename);
        infoBack.SetActive(true);
    }
}
