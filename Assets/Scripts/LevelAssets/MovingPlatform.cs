using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _checkDistance = 0.05f;
    

    private Transform _targerWatpoint;
    private int _currentWaypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        _targerWatpoint = _waypoints[0];
        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            _targerWatpoint.position,
            _speed * Time.deltaTime
            );

        if(Vector2.Distance(transform.position, _targerWatpoint.position) < _checkDistance)
        {
            _targerWatpoint = GetNextWaypoint();
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
        Debug.Log(CharacterMovementScript.gameObject.name);
        if (CharacterMovementScript != null)
        {
            CharacterMovementScript.SetParent(transform);
            //CharacterMovementScript.isWallSliding = false;
            //CharacterMovementScript.isOnMovingPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        var CharacterMovementScript = collision.collider.GetComponent<CharacterMovement>();
        if (CharacterMovementScript != null)
        {
            CharacterMovementScript.ResetParent();
            SceneManager.MoveGameObjectToScene(CharacterMovementScript.gameObject, SceneManager.GetSceneByName("Base") );
            
            //CharacterMovementScript.isWallSliding = true;
            //CharacterMovementScript.isOnMovingPlatform = false;

        }
    }
}
