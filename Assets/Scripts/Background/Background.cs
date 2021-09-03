using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    Material material;
    Vector2 offset;
    public float scrollSpeed;
    Vector2 position;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2((transform.position.x - position.x), (transform.position.y - position.y)) * scrollSpeed;
        material.mainTextureOffset += offset * Time.deltaTime;
        position = transform.position;
    }
}
