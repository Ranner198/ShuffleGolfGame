using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;

public class PlayerPuck : PlayerPuckBehavior
{    
    public Material playerSkin;
    public Color color;
    public Players player;
    public string playerName;

    protected override void NetworkStart()
    {
        base.NetworkStart();

        playerSkin = GetComponent<MeshRenderer>().materials[1];
        color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1);        
        if (networkObject.IsOwner)
            networkObject.characterColor = color;        
        playerSkin.color = color;
    }
   
    void Update()
    {
        if (networkObject != null)
        {
            if (!networkObject.IsOwner)
            {
                transform.position = networkObject.position;
                transform.rotation = networkObject.rotation;                
                player.strokeCount = networkObject.StrokeCount;
                player.holeScore = networkObject.HoleScore;
                if (playerSkin != null)
                    playerSkin.color = networkObject.characterColor;
                return;
            }

            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.StrokeCount = player.strokeCount;
            networkObject.HoleScore = player.holeScore;
        }
    }
}
