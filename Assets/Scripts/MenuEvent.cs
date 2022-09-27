    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvent : MonoBehaviour
{
    public GameObject menuPause;
    public GameObject menuGOver;
    private bool flag = false;

    static public bool isPause = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && flag == false)
        {
            menuPause.SetActive(true);
            isPause = true;
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPause.SetActive(false);
            flag = false;
            isPause = false;
            Debug.Log("jmet a faux menu");
        }
        if (!menuPause.activeSelf && flag == true)
            flag = false;

        if (Player_Manager.GameOver)
        {
            menuGOver.SetActive(true);
            isPause = true;
        }
    }
}
