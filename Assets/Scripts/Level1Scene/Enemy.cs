using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour, IPooleableObject
{
    [SerializeField] private float _speed = 10f;

    private Transform _target;
    private int _waypointsIndex = 0;

    void Start()
    {
        _target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 _dir = _target.position - transform.position;
        transform.Translate(_dir.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if(_waypointsIndex >= Waypoints.points.Length -1)
        {
            EnemyFactory.Instance.ReturnObjectToPool(this);
            //Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return;
        }
        _waypointsIndex++;
        _target = Waypoints.points[_waypointsIndex];
    }

    public void Reset()
    {
        //_target = Waypoints.points[0];
        _waypointsIndex = 0;
    }

    public static void TurnOn(Enemy enemy)
    {
        enemy.Reset();
        enemy.gameObject.SetActive(true);
    }

    public static void TurnOff(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
