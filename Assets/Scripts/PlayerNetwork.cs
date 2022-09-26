using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    public NetworkVariable<Vector3> _netPos = new(writePerm:NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);

    private void Update()
    {
        if (IsOwner)
        {
            _netPos.Value = transform.position;
            _netRot.Value = transform.rotation;
        }
        else
        {

            transform.position = _netPos.Value;
            transform.rotation = _netRot.Value;
        }
    }
}




