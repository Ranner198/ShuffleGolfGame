using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int holeNumber;

    public GameObject scoreCard, quitButton;
    private ScorecardGenerator scorecardGenerator;    

    public GameObject playerSpawner, cameraSpawner;
    public int numberOfPlayers;

    [SerializeField]
    public List<Players> players = new List<Players>();

    [SerializeField]
    public List<Holes> holes;

    string hostAddress = "127.0.0.1";
    ushort port = 15937;
    UDPClient client = new UDPClient();

    #region SingletonDeclration
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);
    }
    #endregion  

    public void Start()
    {              
        scorecardGenerator = scoreCard.GetComponent<ScorecardGenerator>();
        scorecardGenerator.StartMethod();

        client.serverAccepted += OnServerAccepted;
        client.Connect(hostAddress, port);
    }    
    public Players AddPlayer()
    {
        var Player = NetworkManager.Instance.InstantiatePlayerCharacter();
        var playerRef = Player.transform.GetChild(0).GetComponent<Players>();
        playerRef.index = numberOfPlayers;        
        print("times called");
        return playerRef;
    }    

    private void OnServerAccepted(NetWorker clientNetworker)
    {
        numberOfPlayers++;
     
        MainThreadManager.Run(() => {
            AddPlayer();
        });
    }
    IEnumerator NextHole()
    {    
        scorecardGenerator.UpdateScorecard(holeNumber);

        scoreCard.SetActive(true);
        
        CanvasGroup cg = scoreCard.GetComponent<CanvasGroup>();

        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        // Game Over
        if (holeNumber+1 >= holes.Count)
        {
            quitButton.SetActive(true);
            yield break;
        }

        yield return new WaitForSecondsRealtime(3);

        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        scoreCard.SetActive(false);

        holeNumber+=1;

        for (int i = 0; i < players.Count; i++)
        {            
            players[i].character.transform.position = holes[holeNumber].teepad.transform.position;
            Vector3 direction = holes[holeNumber].fowardDirection.transform.position - players[i].cameraFocusPoint.transform.position;
            players[i].cameraFocusPoint.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);          
        }

        yield return new WaitForSeconds(.1f);

        for (int i = 0; i < players.Count; i++)
            players[i].finished = false;
    }

    public void HoleFinished(int index)
    {
        players.Find(player => player.index == index).finished = true;

        // Check if all players are done if so move on to next hole
        bool finished = true;
        foreach (Players player in players)
        {
            if (!player.finished)
                finished = false;
        }
        if (finished)
            StartCoroutine(NextHole());
    }
    public void UnFinished(int index)
    {
        players.Find(player => player.index == index).finished = false;
    }
}