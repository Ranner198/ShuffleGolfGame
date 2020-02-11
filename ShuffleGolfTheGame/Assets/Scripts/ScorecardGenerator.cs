using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScorecardGenerator : MonoBehaviour
{    
    public GameObject ScoreCardObject;
    public GameObject scoreCardPrefab;
    [SerializeField]
    public List<PlayerScore> players = new List<PlayerScore>();
    public void StartMethod()
    {        
        /*
        for (int i = 0; i < GameManager.instance.numberOfPlayers; i++)
        {
            players.Add(new PlayerScore());
            GameObject scoreCardEntity = Instantiate(scoreCardPrefab, ScoreCardObject.transform);
            players[i].scoreCard = new List<TextMeshProUGUI>(scoreCardEntity.GetComponent<ScoreCardDataHolder>().DataCells);
        }
        */
        gameObject.SetActive(false);
    }
    public void UpdateScorecard(int currentHole)
    {
        for (int i = 0; i < GameManager.instance.players.Count; i++)
        {
            players[i].scoreCard[currentHole+1].text = GameManager.instance.players[i].holeScore.ToString();
            players[i].scoreCard[players[i].scoreCard.Count - 1].text = GameManager.instance.players[i].strokeCount.ToString();
            GameManager.instance.players[i].holeScore = 0;
        }
    }
}
[System.Serializable]
public class PlayerScore
{    
    public List<TextMeshProUGUI> scoreCard = new List<TextMeshProUGUI>();
}
