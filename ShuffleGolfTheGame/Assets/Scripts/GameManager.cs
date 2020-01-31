using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int holeNumber;

    public GameObject scoreCard;
    private ScorecardGenerator scorecardGenerator;

    public GameObject playerSpawner, cameraSpawner;
    public int numberOfPlayers;

    [SerializeField]
    public List<Players> players = new List<Players>();

    [SerializeField]
    public List<Holes> holes;    

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
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(playerSpawner, holes[0].teepad.transform.position, Quaternion.Euler(-90,0,0));
            GameObject camera = Instantiate(cameraSpawner);

            Players playerRef = player.GetComponent<Players>();

            playerRef.character = player;
            playerRef.cameraFocusPoint = camera;
            playerRef.playerCamera = camera.transform.GetChild(0).GetComponent<Camera>();
            playerRef.index = i;
            playerRef.color = Random.ColorHSV();

            camera.GetComponent<CameraTracker>().playerGO = player;
            camera.GetComponent<CameraTracker>().player = playerRef;

            players.Add(playerRef);
        }
        
        scorecardGenerator = scoreCard.GetComponent<ScorecardGenerator>();
        scorecardGenerator.StartMethod();
    }

    IEnumerator NextHole()
    {
        scoreCard.SetActive(true);

        scorecardGenerator.UpdateScorecard();

        CanvasGroup cg = scoreCard.GetComponent<CanvasGroup>();

        while (cg.alpha < 1)
        {
            cg.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSecondsRealtime(3);

        while (cg.alpha > 0)
        {
            cg.alpha -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        scoreCard.SetActive(false);

        holeNumber++;

        for (int i =0; i < players.Count; i++)
        {
            players[i].finished = false;
            players[i].character.transform.position = holes[holeNumber].teepad.transform.position;
            Vector3 direction = holes[holeNumber].fowardDirection.transform.position - players[i].cameraFocusPoint.transform.position;
            players[i].cameraFocusPoint.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
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
}