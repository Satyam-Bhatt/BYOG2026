using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindFirstObjectByType<GameManager>();

            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                _instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

    public event Action ViewChanged, OnWin, OnDie, OnTokenCollect;
    private bool _XYview = true;
    public bool XYview { get => _XYview; set { _XYview = value; ViewChanged?.Invoke(); } }
    public int totalCollectableCount = 0;
    public int collectedCount = 0;
    public static bool hasDied = false;
    public static bool easyMode = false;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        totalCollectableCount = GameObject.FindGameObjectsWithTag("Collectable").Length;
        collectedCount = 0;
        hasDied = false;
        easyMode = false;
    }

    private void Start()
    {
        XYview = true;
    }

    public void Collected()
    {
        collectedCount++;
        OnTokenCollect?.Invoke();

        if (collectedCount == totalCollectableCount)
        {
            Win();
        }
    }

    public void Win()
    {
        OnWin?.Invoke();
        Debug.Log("Win");
    }

    public void Die()
    {
        DOTween.KillAll();
        Timeline.animPlaying = false;
        OnDie?.Invoke();
        hasDied = true;
        Debug.Log("Die");
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }
}
