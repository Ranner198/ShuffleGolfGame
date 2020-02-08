using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class PlayerCharacter : PlayerCharacterBehavior
{

    public GameObject playerCamera;
    public GameObject playerPuck;

    protected override void NetworkStart()
    {
        if (!networkObject.IsOwner)
            playerCamera.SetActive(false);

        GameManager.instance.players.Add(playerPuck.GetComponent<Players>());

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (networkObject != null)
        {
            if (!networkObject.IsServer)
            {
                networkObject.index = 0;
            }
        }
    }
}
