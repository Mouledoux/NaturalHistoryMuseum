using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActiveButtonManager : MonoBehaviour
{
    private UnityEngine.UI.Button m_currentActiveButton;
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

        m_currentActiveButton = activeButton;
        m_currentActiveButton.interactable = false;
    }

    public void InvokeButton(int index)
    {
        m_currentActiveButton = m_managedButtons[index];
        m_currentActiveButton.onClick.Invoke();
        UpdateButtons(m_currentActiveButton);
    }

    public void DisableAll()
    {
        foreach(UnityEngine.UI.Button b in m_managedButtons)
        {
            b.interactable = false;
        }
    }

    public void EnableAll()
    {
        UpdateButtons(m_currentActiveButton);
    }
}
