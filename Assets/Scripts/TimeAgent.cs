using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    private void Start()
    {
        init();
    }

    public void init()
    {
        GameManager.instance.dayTimeController.Subscribe(this);

    }

    public void Invoke()
    {
        onTimeTick?.Invoke();
    }

    private void OnDestroy()
    {
        GameManager.instance.dayTimeController.UnSubscribe(this);
    }

}
