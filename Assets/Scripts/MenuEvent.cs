    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvent : MonoBehaviour
{
    public GameObject menuPause;
    private bool flag = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && flag == false)
        {
            menuPause.SetActive(true);
            flag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPause.SetActive(false);
            flag = false;
        }
        if (!menuPause.activeSelf && flag == true)
            flag = false;

    }
}
