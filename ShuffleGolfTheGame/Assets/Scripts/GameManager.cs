using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int holeNumber = -1;
    
    public GameObject scoreCard, quitButton;
    private ScorecardGenerator scorecardGenerator;    

    public GameObject playerSpawner, cameraSpawner;
    public int numberOfPlayers;

    [SerializeField]
    public List<Players> players = new List<Players>();

    [SerializeField]
    public List<Holes> holes;

    public ServerManager serverManager;

    UDPClient client = new UDPClient();

    private GameObject holesObject;

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

        //Delegates
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion  

    public void Start()
    {
        scorecardGenerator = scoreCard.GetComponent<ScorecardGenerator>();
        scorecardGenerator.StartMethod();

        //if (!NetworkManager.Instance.Networker.IsServer)

        //StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(.5f);
        AddPlayer();
    }
    IEnumerator Bruh()
    {
        yield return new WaitForSeconds(.2f);
        print("wut");
        serverManager.GetNumberOfPlayers();
    }
    public Players AddPlayer()
    {
        var Player = NetworkManager.Instance.InstantiatePlayerCharacter();       
        var playerRef = Player.transform.GetChild(0).GetComponent<Players>();
        playerRef.index = numberOfPlayers;        
        return playerRef;
    }  
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            holesObject = GameObject.Find("Holes");
            GenerateHoleData();
            StartCoroutine(Delay());
        }
    }
 
    public void GenerateHoleData()
    {
        if (holes.Count > 0)
            holes.Clear();

        foreach (Transform hole in holesObject.transform)
        {
            Holes temp = new Holes();
            foreach (Transform setPiece in hole.transform)
            {
                switch (setPiece.gameObject.name)
                {
                    case ("Tee"):
                        temp.teepad = setPiece.gameObject;
                        break;
                    case ("FowardDirection"):
                        temp.fowardDirection = setPiece.gameObject;
                        break;
                    case ("Hole"):
                        temp.hole = setPiece.gameObject;
                        break;
                }
            }
            holes.Add(temp);
        }
    
        holes = new List<Holes>(holes);
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