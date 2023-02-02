using EZXR.Glass.Hand;
using EZXR.Glass.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristPanelInteraction : MonoBehaviour
{
    public GameObject wristPanel;
    public GameObject panel;
    public SpacialTrigger[] arTriggers;
    public Color activeColor;
    public Color inactiveColor;
    public SpacialPanel fixedPanel;
    public SpacialPanel bindPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        wristPanel.SetActive(ARHandManager.leftHand.handExist);

        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (fixedPanel.PositionType == UIBase.UIPoseType.FixedToHead)
        //    {
        //        fixedPanel.PositionType = UIBase.UIPoseType.Free;
        //        bindPanel.PositionType = UIBase.UIPoseType.OutReset;
        //    }
        //    else
        //    {
        //        fixedPanel.PositionType = UIBase.UIPoseType.FixedToHead;
        //        bindPanel.PositionType = UIBase.UIPoseType.Free;
        //    }
        //}
    }

    public void ShowPanel()
    {
        panel.SetActive(!panel.activeSelf);
    }

    public void TriggerButton(int id)
    {
        Debug.Log(id);
        for (int i = 0; i < arTriggers.Length; i++)
        {
            if (i == id)
            {
                arTriggers[i].SetColor(activeColor);
            }
            else
            {
                arTriggers[i].SetColor(inactiveColor);
            }
        }
    }
}
