using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ZombieNetwork : NetworkBehaviour
{
    public NetworkVariable<Vector3> _netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> _netHealth = new(writePerm: NetworkVariableWritePermission.Owner);

    private void Update()
    {
        if (IsOwner)
        {
            _netPos.Value = transform.position;
            _netRot.Value = transform.rotation;
            _netHealth.Value = transform.GetComponent<BasicZombiController>().currentHealth;
        }
        else
        {
            transform.position = _netPos.Value;
            transform.rotation = _netRot.Value;
            transform.GetComponent<BasicZombiController>().currentHealth = _netHealth.Value;
        }
    }
}
