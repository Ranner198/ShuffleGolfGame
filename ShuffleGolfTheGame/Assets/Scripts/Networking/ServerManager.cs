using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;

public class ServerManager : ServerManagerBehavior
{
    public int currentHole = 0;

    public void NextHole()
    {
        print("Next Hole Was Called");
        currentHole+=1;
        networkObject.CurrentHoleNumber = currentHole;        
    }

    public int GetCurrentHole()
    {
        currentHole = networkObject.CurrentHoleNumber;
        return currentHole;
    }
}
