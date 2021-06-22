using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public PlayerHUD playerHUD;
    public PlayerKnightController player;
    int scorePoints = 0;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score) {
        scorePoints = score + scorePoints;
        playerHUD.txtScore.text = scorePoints.ToString();
    }

}
