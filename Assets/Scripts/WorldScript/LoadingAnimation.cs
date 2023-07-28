using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingAnimation : MonoBehaviour
{

    public TextMeshProUGUI loadingText;
    private bool isAnimating = false;
    private string baseText = "Loading";

    private void OnEnable()
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateText());
        }
    }

    IEnumerator AnimateText()
    {
        isAnimating = true;
        int dotCount = 0;

        while (true)
        {
            loadingText.text = baseText + new string('.', dotCount);

            dotCount++;
            if (dotCount > 5)
            {
                dotCount = 0;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnDisable()
    {
        isAnimating = false;
        StopCoroutine(AnimateText());
    }
}


