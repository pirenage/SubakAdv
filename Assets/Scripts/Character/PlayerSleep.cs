using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleep : MonoBehaviour
{
    public DayTimeController dayTimeController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bed") // Gantilah "Bed" dengan tag kasur Anda
        {
            dayTimeController.SleepUntilMorning();
        }
    }
}
