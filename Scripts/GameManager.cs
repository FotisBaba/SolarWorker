using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject character, light, camera2;
    public GameObject menuPanel, howToPanel;
    public List<GameObject> panels;
    public bool inGame = false;
    public AudioSource soundtrack;
    public List<GameObject> Panels
    {
        get => panels;
        set => panels = value;
    }

    public GameObject Character
    {
        get => character;
        set => character = value;
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!inGame)
            return;
        if (camera2 == null)
        {
            camera2 = GameObject.Find("Camera2");
            camera2.SetActive(false);
        }
            
        if (light == null)
            light = GameObject.Find("Directional Light");

        if (Character != null && Character.GetComponent<Character>().Health <= 0)
        {
            Character.GetComponent<CharacterMovement>().enabled = false;
            Character.GetComponent<Character>().defeatPanel.SetActive(true);
        }
        
        if (panels.Count > 0)
        {
            if (panels[0].GetComponent<SolarPanel>().ready && panels[1].GetComponent<SolarPanel>().ready &&
                panels[2].GetComponent<SolarPanel>().ready)
            {
                character.GetComponent<CharacterMovement>().enabled = false;
                Character.GetComponent<Character>().victoryPanel.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (camera2.activeSelf)
            {
                camera2.SetActive(false);
                character.GetComponent<Character>().camera.SetActive(true);
            }
            else
            {
                camera2.SetActive(true);
                character.GetComponent<Character>().camera.SetActive(false);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            foreach (var panel in panels)
            {
                if (panel.GetComponent<SolarPanel>().canInteract && !panel.GetComponent<SolarPanel>().ready)
                {
                    panel.transform.Rotate(Vector3.up, 45f);
                    if ((int)light.transform.rotation.eulerAngles.y != 180)
                    {
                        if ((int)light.transform.rotation.eulerAngles.y == 270)
                        {
                            if((int)panel.transform.rotation.eulerAngles.y == 90)
                                panel.GetComponent<SolarPanel>().ready = true;
                        }
                        else if ((int)panel.transform.rotation.eulerAngles.y ==
                            (int)light.transform.rotation.eulerAngles.y + 180)
                        {
                            panel.GetComponent<SolarPanel>().ready = true;
                        }
                    }
                    else if ((int)light.transform.rotation.eulerAngles.y == 180)
                    {
                        if ((int)panel.transform.rotation.eulerAngles.y == 0)
                        {
                            panel.GetComponent<SolarPanel>().ready = true;
                        }
                    }
                }
            }
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        inGame = true;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        menuPanel.SetActive(true);
        howToPanel.SetActive(false);
    }
    
    public void HowToPlay()
    {
        howToPanel.SetActive(true);
        menuPanel.SetActive(false);
    }
}
