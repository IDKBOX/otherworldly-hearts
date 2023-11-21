using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 6f;
    private float _checkDistance = 0.05f;
    
    private Transform _targetWaypoint;
    private int _currentWaypointIndex = 0;


    void Start()
    {
        _targetWaypoint = _waypoints[0];
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            _targetWaypoint.position,
            _speed * Time.deltaTime
            );

        if (Vector2.Distance(transform.position, _targetWaypoint.position) < _checkDistance)
        {
            _targetWaypoint = GetNextWaypoint();
        }
    }

    private Transform GetNextWaypoint()
    {
        _currentWaypointIndex++;
        if(_currentWaypointIndex >= _waypoints.Length)
        {
            _currentWaypointIndex = 0;
        }

        return _waypoints[_currentWaypointIndex];
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        var CharacterMovementScript = collision.collider.GetComponent<CharacterMovement>();
        if (CharacterMovementScript != null)
        {
            CharacterMovementScript.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var CharacterMovementScript = collision.collider.GetComponent<CharacterMovement>();
        if (CharacterMovementScript != null)
        {
            CharacterMovementScript.ResetParent();
            SceneManager.MoveGameObjectToScene(CharacterMovementScript.gameObject, SceneManager.GetSceneByName("Base"));

        }
    }
}
