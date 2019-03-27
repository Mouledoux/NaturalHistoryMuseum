using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSubscribe : UIUtils
{
    private Mouledoux.Components.Mediator.Subscriptions m_subscriptions = new Mouledoux.Components.Mediator.Subscriptions();
    private Mouledoux.Callback.Callback m_subCallback = null;
    

    public string m_subMessage;
    public float m_delay = 0f;
    public UnityEngine.Events.UnityEvent m_event;
       
	void Awake ()
    {
        m_subCallback = InvokeUnityEvent;
        m_subscriptions.Subscribe(m_subMessage, m_subCallback);
	}

    private void InvokeUnityEvent(Mouledoux.Callback.Packet emptyPacket)
    {
        if (!isActiveAndEnabled) return;
        StartCoroutine(iInvokeUnityEvent(emptyPacket));
    }

    private IEnumerator iInvokeUnityEvent(Mouledoux.Callback.Packet emptyPacket)
    {
        yield return new WaitForSeconds(m_delay);
        m_event.Invoke();
    }

    private void OnDestroy()
    {
        m_subscriptions.UnsubscribeAll();   
    }
}