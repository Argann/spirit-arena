using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_manager : MonoBehaviour
{
    public GameObject[] tutorialPanels;
    private int selected;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject panel in tutorialPanels) {
            panel.SetActive(false);
        }
        selected = 0;
        tutorialPanels[selected].SetActive(true);
        InvokeRepeating("SwapPanel", 3.5f, 3.5f);
    }

    void SwapPanel() {
        tutorialPanels[selected].SetActive(false);
        selected = (selected + 1) % tutorialPanels.Length;
        tutorialPanels[selected].SetActive(true);
    }
}
