using UnityEngine;

public class TetrisBlock : MonoBehaviour {
    public Vector3 rotationPoint;
    private float previousTime;
    public float fallTime = 0.8f;

    private GameManager gameManager;
    private SpawnController spawnController;
    private InputManager inputManager;

    private static Transform[,] grid = new Transform[GameManager.width, GameManager.height];
    
    private void Awake() {
        gameManager = GameManager.Instance;
        spawnController = SpawnController.Instance;
        inputManager = InputManager.Instance;
    }

    private bool Move(Vector3 move) {
        transform.position += move;
        if (!ValidMove()) {
            transform.position -= move;
            return true;
        }

        return false;
    }

    private bool Flip() {
        transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, 90);
        if (!ValidMove()) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), Vector3.forward, -90);
            return true;
        }

        return false;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(Vector3.left);
            previousTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(Vector3.right);
            previousTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) Flip();

        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime)) {
           if (Move(Vector3.down)) {
                CheckForLines();
                AddToGrid();
                enabled = false;
                spawnController.SpawnTetrisBlock();
           }

           previousTime = Time.time;
        }
    }

    void CheckForLines() {
        for (int i = GameManager.height - 1; i >= 0; i--) {
            if (HasLine(i)) {
                DeleteLine(i);
                RowDown(i);
                gameManager.addScore(40);
            }
        }
    }

    bool HasLine(int i) {
        for (int j = 0; j < GameManager.width; j++) {
            if (grid[j, i] == null) return false;
        }

        return true;
    }

    void DeleteLine(int i) {
        for (int j = 0; j < GameManager.width; j++) {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i) {
        for (int y = i; y < GameManager.height; y++) {
            for (int j = 0; j < GameManager.width; j++) {
                if (grid[j, y] != null) {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= Vector3.up;
                }
            }
        }
    }

    void AddToGrid() {
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }
    }

    bool ValidMove() {
        foreach (Transform children in transform) {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= GameManager.width || roundedY < 0 || roundedY >= GameManager.height) {
                return false;
            }

            if (grid[roundedX, roundedY] != null) return false;
        }

        return true;
    }
}