using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class ServerManager : ServerManagerBehavior
{
    public int GetNumberOfPlayers()
    {
        return networkObject.numOfPlayers;
    }
    public void PlayerJoined()
    {
        networkObject.numOfPlayers++;
    }
    public void PlayerDisconnected()
    {
        networkObject.numOfPlayers--;
    }
}
