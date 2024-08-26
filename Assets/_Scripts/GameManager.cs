using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PersonType
{
    None,
    Player,
    Enemy
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject enemySprite;

    [SerializeField] private List<StateDisplay> displays;
    [SerializeField] private float steadyOpenDelay;
    [SerializeField] private Vector2 rangeBangDelay;
    [SerializeField] private Vector2 rangeEnemyShootTime;

    private PersonType killPerson;

    private DateTime bangtime;
    private float enemyDistance;

    public void Awake()
    {
        Instance = this;
    }

    public void Play()
    {
        IEnumerator Animation()
        {
            Active(State.Ready);
            yield return new WaitForSeconds(steadyOpenDelay);
            Active(State.Steady);
            var randomSecond = Random.Range(rangeBangDelay.x, rangeBangDelay.y);
            yield return new WaitForSeconds(randomSecond);
            bangtime = DateTime.Now;
            enemyDistance = Random.Range(rangeEnemyShootTime.x, rangeEnemyShootTime.y);
            CountDown();
            Active(State.Bang);

        }

        StartCoroutine(Animation());
    }

    private void CountDown()
    {
        IEnumerator Do()
        {
            var passed = 0f;
            var time = enemyDistance;

            while (passed < time)
            {
                passed += Time.deltaTime;
                yield return null;
            }

            EnemyShoot();
        }

        StartCoroutine(Do());
    }

    private void Active(State state)
    {
        foreach (var item in displays)
            item.gameObject.SetActive(false);

        var current = GetDisplay(state);
        current.gameObject.SetActive(true);
    }

    private StateDisplay GetDisplay(State state)
    {
        return displays.Find(x => x.myState == state);
    }

    public void OnShoot()
    {
        if (killPerson != PersonType.None) return;

        killPerson = PersonType.Enemy;
         
        var distance = DateTime.Now - bangtime;
        Debug.Log(distance.TotalSeconds);
        Debug.Log(enemyDistance);

        if(enemyDistance > distance.TotalSeconds)
        {
            Debug.Log("Enemy Kill");
            
        }
        playerSprite.SetActive(false);
        OnKill();
    }

    private void EnemyShoot()
    {
        if (killPerson != PersonType.None) return;

        killPerson = PersonType.Player;

        
        Debug.Log("Player Kill");
        enemySprite.SetActive(false);

        OnKill();
        
    }

    private void OnKill()
    {
        Debug.Log(killPerson);
        var current = GetDisplay(State.Bang);
        current.gameObject.SetActive(false);
    }

  
}
