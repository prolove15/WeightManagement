using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //------------------------------------------------------------ fields
    public UIManager uiManager_Cp;
    public CalendarManager calManager_Cp;
    public GraphManager graphManager_Cp;
    public FileManager fileManager_Cp;

    [SerializeField]
    Animator CurtainAnim_Cp;

    //------------------------------------------------------------ methods
    // Awake : init screen resolution
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //---------------------------------------- Init
    void Init()
    {
        uiManager_Cp.Init();

        fileManager_Cp.Init();

        calManager_Cp.Init();

        graphManager_Cp.Init();

        Play();
    }

    //---------------------------------------- Play
    void Play()
    {
        fileManager_Cp.Play();

        calManager_Cp.Play();

        graphManager_Cp.Play();

        uiManager_Cp.Play();

        CurtainUp();
    }

    //---------------------------------------- Manage Curtain
    void CurtainUp()
    {
        
    }
}
