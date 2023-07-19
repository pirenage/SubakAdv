using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : MonoBehaviour
{
    [SerializeField] GameObject highlighter;
    GameObject currentHighlight;
    public void Highlight(GameObject target)
    {
        if (currentHighlight == target)
        {
            return;
        }
        Vector3 position = target.transform.position + Vector3.up * 0.5f;
        Highlight(position);
    }
    public void Highlight(Vector3 position)
    {
        highlighter.SetActive(true);
        highlighter.transform.position = position;
    }
    public void Hide()
    {
        currentHighlight = null;
        highlighter.SetActive(false);
    }
}
