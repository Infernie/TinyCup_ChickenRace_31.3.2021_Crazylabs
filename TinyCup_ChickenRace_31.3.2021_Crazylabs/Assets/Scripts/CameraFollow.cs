using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField]
    private GameObject player;
    public Debugger dbg;

    [HideInInspector]
    public Vector3 offset; //distance between camera and the player

    private void Start ()
    {
        this.offset = this.transform.position - player.transform.position; //calculating the distance
    }

    private void LateUpdate ()
    {   
        this.transform.position = player.transform.position + this.offset; 
        //setting the camera position based on the distance between camera and the player and player position
    }
}
