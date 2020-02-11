using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Quit : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        NetworkManager.Instance.Disconnect();
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("GameController"))
            Destroy(player);
    }
}
