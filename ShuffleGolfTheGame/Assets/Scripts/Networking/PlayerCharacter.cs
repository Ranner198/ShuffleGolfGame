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
    }

    void Update()
    {
        if (!networkObject.IsServer)
        {
            networkObject.index = 0;
        }
    }
}
