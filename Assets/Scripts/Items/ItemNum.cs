using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemNum : MonoBehaviour
{
    public Item item;
    private TextMeshPro textMesh;

    void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        textMesh.gameObject.SetActive(false);
        textMesh.text = ("x" + item.ammount.ToString());
        textMesh.gameObject.SetActive(true);
    }
}
