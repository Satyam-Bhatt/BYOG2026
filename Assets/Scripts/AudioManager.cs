using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance != null) return _instance;

            _instance = FindFirstObjectByType<AudioManager>();

            if (_instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                _instance = go.AddComponent<AudioManager>();
                DontDestroyOnLoad(go);
            }

            return _instance;
        }
    }

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

    public AudioSource music;
    public AudioSource sfx;
    public AudioSource dialogues;
    public AudioClip token, die, win, start, pickup, move, drop;
    public VoiceOverManager[] voiceOvers;

    private void OnEnable()
    {
        GameManager.Instance.OnTokenCollect += OnTokenCollected;
        GameManager.Instance.OnDie += OnDie;
        GameManager.Instance.OnWin += OnWin;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnTokenCollect -= OnTokenCollected;
        GameManager.Instance.OnDie -= OnDie;
        GameManager.Instance.OnWin -= OnWin;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnTokenCollected()
    {
        sfx.PlayOneShot(token);
    }

    public void OnDie()
    {
        sfx.PlayOneShot(die);
    }

    public void OnWin()
    {
        sfx.PlayOneShot(win);
    }

    public void OnPickup()
    {
        sfx.PlayOneShot(pickup);
    }

    public void OnDrop()
    {
        sfx.PlayOneShot(drop);
    }

    public void OnMove()
    {
        sfx.PlayOneShot(move);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        sfx.PlayOneShot(start);
        if (voiceOvers[SceneManager.GetActiveScene().buildIndex].isPlayed == false)
        {
            if(voiceOvers[SceneManager.GetActiveScene().buildIndex].voiceOver != null)
                dialogues.PlayOneShot(voiceOvers[SceneManager.GetActiveScene().buildIndex].voiceOver);
            voiceOvers[SceneManager.GetActiveScene().buildIndex].isPlayed = true;
        }
    }
}

[System.Serializable]
public class VoiceOverManager
{
    public bool isPlayed = false;
    public AudioClip voiceOver;
}

