using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Panel currentPanel = null;


    private List<Panel> panelHistory = new List<Panel>();

    private void SetupPanels()
    {
        Panel[] panels = GetComponentsInChildren<Panel>();

        foreach(Panel panel in panels)
        {
            panel.Setup(this);
        }

        currentPanel.Show();
    }

    public void GoToPrevious()
    {
        if(panelHistory.Count == 0)
        {
            return;
        }
        else
        {
            SetCurrent(panelHistory[panelHistory.Count-1]);
            panelHistory.RemoveAt(panelHistory.Count - 1);
        }
    }

    public void SetCurrentWithHistory(Panel newp)
    {
        panelHistory.Add(currentPanel);
        SetCurrent(newp);
    }

    private void SetCurrent(Panel newp)
    {
        currentPanel.Hide();

        currentPanel = newp;
        currentPanel.Show();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupPanels();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
