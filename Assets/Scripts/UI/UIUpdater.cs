using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public string text;

    void Awake()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
        textMesh.text = (text);
        gameObject.SetActive(true);
    }

    public void UpdateText(string _text)
    {
        if (textMesh == null)
        {
            textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        }

        text = _text;
        gameObject.SetActive(false);
        textMesh.text = (text);
        gameObject.SetActive(true);
    }
}
