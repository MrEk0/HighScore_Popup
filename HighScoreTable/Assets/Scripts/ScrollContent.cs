using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollContent : MonoBehaviour
{
    [SerializeField] RectTransform contentRect;

    float defaultBorder;
    int resultCount=0;

    private void Awake()
    {
        defaultBorder = contentRect.GetComponent<RectTransform>().rect.height;
    }

    public void SetContentHigh(GameObject resultPrefab, float offset)
    {
        foreach (Transform child in transform)
        {
            resultCount++;
        }

        GameObject playerPanel = resultPrefab.GetComponentInChildren<Image>().gameObject;
        float height = playerPanel.GetComponent<RectTransform>().rect.height;

        if (resultCount > 5)
        {
            contentRect.sizeDelta = new Vector2(0, height+(resultCount-1) * offset);
        }
        else
        {
            contentRect.sizeDelta = new Vector2(0, defaultBorder);
        }

        resultCount = 0;
    }
}
