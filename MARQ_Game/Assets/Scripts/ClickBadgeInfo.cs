using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// attached to badge buttons in badge panel
public class ClickBadgeInfo : MonoBehaviour {

    GameObject badgeInfo, backBtn;

    public void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(badgeOnCLick);
        badgeInfo = gameObject.transform.parent.parent.Find("badgeInfo").gameObject;
        backBtn = gameObject.transform.parent.parent.parent.Find("infoBack").gameObject;
    }

    // on click if it is unlocked show badge info
    public void badgeOnCLick()
    {
        string badgename = gameObject.name;
        badgename = badgename.Split('_')[0];
        // see if this badge is unlocked
        if (GameControl.control.hasBadge(badgename))
        {
            // load info page and assign and show it
            badgeInfo.SetActive(true);
            string badgeinfo = badgename.Split('_')[0] + "_info";
            badgeInfo.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Badges/" + badgeinfo);
            // show info back button
            backBtn.SetActive(true);
        }
    }
}
