using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActiveButtonManager : MonoBehaviour
{
    public bool ClickFirstOnStart = false;
    private List<UnityEngine.UI.Button> m_managedButtons = new List<UnityEngine.UI.Button>();
    
	void Start ()
    {
        foreach (UnityEngine.UI.Button button in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            button.onClick.AddListener(()=> UpdateButtons(button));
            button.image.alphaHitTestMinimumThreshold = 1f;
            m_managedButtons.Add(button);
        }

        if (ClickFirstOnStart)
        {
            InvokeButton(0);
        }
	}

    private void UpdateButtons(UnityEngine.UI.Button activeButton)
    {
        foreach (UnityEngine.UI.Button button in GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            button.interactable = true;
        }

        activeButton.interactable = false;
    }

    public void InvokeButton(int index)
    {
        m_managedButtons[index].onClick.Invoke();
        UpdateButtons(m_managedButtons[index]);
    }
}
