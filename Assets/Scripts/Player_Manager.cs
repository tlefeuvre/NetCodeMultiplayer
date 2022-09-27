using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using System.Net.Sockets;


public class Player_Manager : NetworkBehaviour
{

    [SerializeField] GameObject slider;
    private Slider sliderValue;

    private bool flagCoroutine = false;

    private bool activateHealthRegen = false;
    private float MaxHealth = 150;
    private float CurrHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrHealth = MaxHealth;
        
        sliderValue = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrHealth <= 0)
        {
            print("ratio");
        }

        sliderValue.value = CurrHealth/MaxHealth;

        if (!flagCoroutine)
        {
            StartCoroutine("WaitRegen");
        }
    }

    IEnumerator WaitRegen()
    {
        flagCoroutine = true;

        if (activateHealthRegen)
        {
            yield return new WaitForSeconds(1);
            CurrHealth += 30;
        }
        else
        {
            yield return new WaitForSeconds(5);
            activateHealthRegen = true;
        }

        flagCoroutine = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BasicZombie")
        {
            CurrHealth -= 40;
            activateHealthRegen = false;
        }
        if (collision.gameObject.tag == "BasicZombie2")
        {
            CurrHealth -= 40;
            activateHealthRegen = false;
        }
        if (collision.gameObject.tag == "BasicZombie")
        {
            CurrHealth -= 40;
            activateHealthRegen = false;
        }
    }
}
