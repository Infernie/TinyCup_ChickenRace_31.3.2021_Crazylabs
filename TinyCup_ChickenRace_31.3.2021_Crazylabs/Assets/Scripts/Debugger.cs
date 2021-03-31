using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using IngameDebugConsole;
using System;

public class Debugger : MonoBehaviour //There are 2 debug menus, DebugLogConsole(unity asset store asset) and a custom made one
{
    
    #region Public objects
    public Button Pause;
    public Button speedUp;
    public Button speedDown;
    public Button Resume;
    public Button HorizontalDown;
    public Button HorizontalUp;
    public Button StopMovement;

    public GameObject Panel;
    public GameObject Diamondo;
    public GameObject FlowerPot;

    private bool working=false;
   
    public CameraFollow cam;
    public PlayerController swiper;
    #endregion 
    

    void Start()
    {
        DebugLogConsole.AddCommand<Vector3>("Cube","Creates a cube at a specified x location", CreateCubeAt);
        DebugLogConsole.AddCommand<Vector3, int>("Collectible", "Creates a gem at the start of the scene", CreateGemAt);
        DebugLogConsole.AddCommand<Vector3, int>("Enemy", "Creates an enemy at the start of the scene", CreateEnemyAt);
        DebugLogConsole.AddCommand<Vector3, Quaternion>("Camera", "Move and rotate the camera", MoveRotateCamera);
        //2nd debug console, it's better for instantiating objects

        Button stopmovement = StopMovement.GetComponent<Button>();
        Button btn = Pause.GetComponent<Button>();
        Button speedup = speedUp.GetComponent<Button>();
        Button speeddown = speedDown.GetComponent<Button>();
        Button resume = Resume.GetComponent<Button>();
        Button horizontaldown = HorizontalDown.GetComponent<Button>();
        Button horizontalup = HorizontalUp.GetComponent<Button>();
        //gets all the buttons(remember to assign them in the editor!)

        stopmovement.onClick.AddListener(StopMovementOnClick);
        btn.onClick.AddListener(OnPauseClickFirst);
        speeddown.onClick.AddListener(SpeedDown);
        speedup.onClick.AddListener(SpeedUp);
        resume.onClick.AddListener(OnPauseClickSecond);
        horizontaldown.onClick.AddListener(HorizontalSpeedDown);
        horizontalup.onClick.AddListener(HorizontalSpeedUp);
        //sets onClick function on every button

    }

    #region All methods here are for the DebugConsoleLog
    private void MoveRotateCamera(Vector3 position, Quaternion rotation) //changes the position and the rotation of the camera attached to the player
    {
        cam.offset = position;
        cam.transform.position = cam.offset;
        cam.transform.rotation = rotation;
    }

    private void CreateEnemyAt(Vector3 zero, int howMuch) //creates enemy at x,y,z; user can also choose how many, they will be place in each other
    {
        for (int i = 0; i < howMuch; i++)
        GameObject.Instantiate(FlowerPot, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0));
    }

    private void CreateGemAt(Vector3 zero, int howMuch) //creates a collectible at x,y,z; user can also choose how many, they will be place in each other
    {
       for(int i=0;i<howMuch;i++)
       GameObject.Instantiate(Diamondo, new Vector3(0,0,0), Quaternion.Euler(88,0,0));

    }
    public static void CreateCubeAt(Vector3 position) //creates a cube at x,y,z; user can choose to add whatever to it in the editor
    {
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = position;
    }
    #endregion
    #region All methods here affect the speed of the player (z axis and x axis)
    private void HorizontalSpeedUp()
    {
        swiper.SideSpeed += 2;
    }

    private void HorizontalSpeedDown()
    {
        swiper.SideSpeed -= 2;
    }

    private void SpeedUp()
    {
        swiper.ForwardSpeed += 2; 
        
    }

    private void SpeedDown()
    {
        swiper.ForwardSpeed -= 2;
    }

    private void StopMovementOnClick()
    {
        swiper.ForwardSpeed = 0;
    }

    #endregion
    
    private void OnPauseClickFirst()
    {
        
        Time.timeScale = 0;
        Panel.gameObject.SetActive(true); 
        //panel has speedDown, speedUp and Resume added as children so the code in the comment is useful if panel is deleted
        Pause.gameObject.SetActive(false);

        /*speedDown.gameObject.SetActive(true);
        speedUp.gameObject.SetActive(true);
        Resume.gameObject.SetActive(true);*/
        
    }

    private void OnPauseClickSecond()
    {
        Time.timeScale = 1;
        Panel.gameObject.SetActive(false);
        //panel has speedDown, speedUp and Resume added as children so the code in comments is useful if panel is deleted
        Pause.gameObject.SetActive(true);

        /*Resume.gameObject.SetActive(false);
        speedDown.gameObject.SetActive(false);
        speedUp.gameObject.SetActive(false);*/
    }
}
