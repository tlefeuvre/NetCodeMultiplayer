using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class TrapSpawner : NetworkBehaviour
{
    public GameObject trap;
    public GameObject parent;
    public Vector3 m_pos;
    public GameObject Hitbox;

    private bool isOk = false;

    // Start is called before the first frame update
    void Start()
    {
        if (IsHost && !isOk)
        {

            //Vector3 pos = new Vector3(13.04f, 0.984f, -13.2f);
            Vector3 pos = transform.position;
            GameObject newObject = Instantiate(trap) as GameObject;
            newObject.GetComponent<NetworkObject>().Spawn();
            isOk = true;
        }

    }
  
    // Update is called once per frame
    void Update()
    {
        
        if (IsHost && !isOk)
        {
            m_pos = this.transform.position;
            m_pos.x += 0.70f;
            m_pos.y += 1.65f;
            m_pos.z += 0.07f;

            //Vector3 pos = new Vector3(13.04f, 0.984f, -13.2f);
            Vector3 pos = transform.GetChild(1).transform.position;
            GameObject newObject = Instantiate(trap) as GameObject;
            newObject.transform.position = m_pos;
            Hitbox.GetComponent<TrapActivation>().trap = newObject;
            newObject.GetComponent<NetworkObject>().Spawn();
            isOk = true;
        }
       
    }
}
