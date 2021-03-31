using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    #region 
    private Vector3 touchPosition;
    private Vector3 direction;
    private Vector2 lastPosition;

    public float ForwardSpeed = 1;
    public float SideSpeed = 10f;
    private float sidewaysForce = 200f;
    private int count;

    public Text countText;   
    
    CharacterController cont;   
    #endregion
    private void Start()
    {
        cont = GetComponent<CharacterController>();    
        count = 0;     
        countText.text = "Score: " + count.ToString();
    }
    private void Update()
    {
        cont.Move(new Vector3(0, 0, ForwardSpeed * Time.deltaTime)); //moving forward forever
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                lastPosition = touch.position; //gets position of the first touch on the screen
            }
            if (touch.phase == TouchPhase.Moved)
            {
                var dir = touch.position - lastPosition; //gets the direction of where the user swipes
                var signedDir = Mathf.Sign(dir.x);
                

                cont.Move(new Vector3(SideSpeed * signedDir * Time.deltaTime / 2, 0, 0)); //moves horizontally based on the swipe duration and direction


                lastPosition = touch.position; //sets the next position to be the last position touched 
            }
        }
    }
    void OnTriggerEnter(Collider other) //all of the collisions and their interactions go here: if there are many more collisions, take switch(other)
    {
        if (other.tag == "Enemy")
        {
            SceneManager.LoadScene(0); //resets scene after colliding with an enemy
        }
        else if (other.tag == "Food")
        {
            count += 1;
            other.gameObject.SetActive(false); //picks up a collectible and adds +1 to count
            countText.text = "Score: " + count.ToString();     
        }

    }
}
