using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    public static int height = 20;
    public static int width = 10;
    
    [SerializeField] private int score;
    
    public GameObject[] blocks;
    private Text scoreLabel;
    private Canvas nextBlockContainer;

    public override void Awake() {
        scoreLabel = GameObject.Find("ScoreLabel").GetComponent<Text>();
        nextBlockContainer = GameObject.Find("NextBlockContainer").GetComponent<Canvas>();
    }

    public void addScore(int _score) => score += _score;

    private void Update() {
        scoreLabel.text = "Score: " + score;
    }

    public void GameOver() {
        
    }
}