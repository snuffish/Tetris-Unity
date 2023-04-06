using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnController : Singleton<SpawnController> {
    private GameManager gameManager;

    public override void Awake() {
        gameManager = GameManager.Instance;
    }

    private void Start() {
        SpawnTetrisBlock();
    }

    public void SpawnTetrisBlock() {
        Instantiate(gameManager.blocks[new Random().Next(gameManager.blocks.Length)], transform.position,
            Quaternion.identity);
    }
}