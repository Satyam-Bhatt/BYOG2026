using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineEnd : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnEnable()
    {
        timeline.stopped += End;
    }

    private void End(PlayableDirector director)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
