using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPopUp : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float timer = 0.0f;
    private float popUpDuration = -1.0f;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        textMesh.enabled = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= popUpDuration)
        {
            textMesh.enabled = true;
        }
        else
        {
            textMesh.enabled = false;
        }
    }

    public void displayPopUp(string popUpTxt, float _popUpDuration)
    {
        timer = 0.0f;
        popUpDuration = _popUpDuration;

        gameObject.SetActive(false);
        textMesh.text = (popUpTxt);
        gameObject.SetActive(true);
    }
}
