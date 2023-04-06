using System;
using System.Collections;
using UnityEngine;

public enum Direction {
    UP, DOWN, RIGHT, LEFT, NONE
}

public class SwipeDetection : MonoBehaviour {
    [SerializeField] private float minimumDistance = .2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0,1f)] private float directionThreshold = .9f;
    [SerializeField] private GameObject trail;
    
    private InputManager inputManager;
    
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private Coroutine coroutine;

    [NonSerialized] public Direction direction;

    private void Awake() {
        inputManager = InputManager.Instance;
    }

    private void OnEnable() {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable() {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time) {
        startPosition = position;       
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = position;
        StartCoroutine(Trail());
        coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail() {
        while (true) {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return new WaitForEndOfFrame();
            // yield return null;
        }
    }
    
    private void SwipeEnd(Vector2 position, float time) {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe() {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime) {
            Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            this.direction = SwipeDirection(direction2D);
        }
    }

    private Direction SwipeDirection(Vector2 direction) {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold) {
            return Direction.UP;
        } else if (Vector2.Dot(Vector2.down, direction) > directionThreshold) {
            return Direction.DOWN;
        } else if (Vector2.Dot(Vector2.left, direction) > directionThreshold) {
            return Direction.LEFT;
        } else if (Vector2.Dot(Vector2.right, direction) > directionThreshold) {
            return Direction.RIGHT;
        }

        return Direction.NONE;
    }
}