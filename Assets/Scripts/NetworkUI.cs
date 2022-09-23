using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkUI : NetworkBehaviour
{
    [SerializeField]
    private NetworkManager _nm;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 100));

        if(!_nm.IsServer && !_nm.IsClient)
        {
            if (GUILayout.Button("HOST"))
                _nm.StartHost();

            if (GUILayout.Button("SERVER"))
                _nm.StartServer();

            if (GUILayout.Button("CLIENT"))
                _nm.StartClient();

        }

        GUILayout.EndArea();
    }
}
