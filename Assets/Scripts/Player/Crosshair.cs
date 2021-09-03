using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Crosshair : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;

    public GameObject player;
    public float speed = 5;
    public float range;
    public bool isAimed = false;

    public Vector2 playerPositionChange;

    void Awake()
    {
        controls = new PlayerControls();
        Cursor.visible = false;
        playerPositionChange = player.transform.position;

        //Croshair aim controlls
        controls.Gameplay.Aim.performed += ctx => isAimed = true;
        controls.Gameplay.Aim.canceled += ctx => isAimed = false;
        controls.Gameplay.MoveCrosshair.performed += ctx => MoveCrosshairAimed(ctx.ReadValue<Vector2>());
        controls.Gameplay.MoveCrosshair.canceled += ctx => move = Vector2.zero;
    }

    void Update()
    {
        //Update croshair position 
        Vector2 newPosition = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(newPosition, Space.World);

        //Keep crosshair within range
        float distance = Vector2.Distance(transform.position, player.transform.position); 
        if (distance > range)
        {
            Vector3 playerToCrosshair = transform.position - player.transform.position;
            playerToCrosshair *= range / distance; 
            transform.position = player.transform.position + playerToCrosshair;
        }

        //only appear if aiming
        if (!isAimed)
        {
            transform.position = player.transform.position + GetVectorFromAngle(player.transform.rotation.eulerAngles.z) * (range / 2);
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        else if (!player.GetComponent<PlayerController>().isDead)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }

    void MoveCrosshairAimed (Vector2 _move)
    {
        if (isAimed)
        {
            move = _move * speed;
        }
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        // returns vector from given angle
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void SetIsAimed(bool _isAimed)
    {
        isAimed = _isAimed;
    }
}
