using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class BulletNetwork : NetworkBehaviour
{
    public NetworkVariable<Vector3> _netPos = new(writePerm: NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> _netRot = new(writePerm: NetworkVariableWritePermission.Owner);
    private void Start()
    {
        if (IsOwner)
        {
            if (IsHost)
                StartCoroutine(DestroyBullet());
            else
                SubmitRequestDestroyTimerServerRpc();
        }
     


    }
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
    IEnumerator DestroyBullet()
    {
        // suspend execution for 10 seconds
        yield return new WaitForSeconds(3f);
        NetworkObject m_SpawnedNetworkObject = this.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Despawn();
        Destroy(this);

    }

    [ServerRpc]
    private void SubmitRequestDestroyTimerServerRpc(ServerRpcParams rpcParams = default)
    {
        StartCoroutine(DestroyBullet());

    }

    [ServerRpc]
    private void SubmitRequestDestroyServerRpc(ServerRpcParams rpcParams = default)
    {
        //StartCoroutine(DestroyBullmet());
        NetworkObject m_SpawnedNetworkObject = this.GetComponent<NetworkObject>();
        m_SpawnedNetworkObject.Despawn();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("JE TOUCHE QQCHOSE");
        if (IsOwner)
        {
            if (IsHost)
            {
                //StartCoroutine(DestroyBullmet());
                NetworkObject m_SpawnedNetworkObject = this.GetComponent<NetworkObject>();
                //m_SpawnedNetworkObject.Despawn();
                Destroy(gameObject);

            }
            else
                SubmitRequestDestroyServerRpc();
        }
        
    }
   
}
