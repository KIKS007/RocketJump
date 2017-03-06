using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplitudeEvents : MonoBehaviour {

    ChunksManager ChunksManager;

    float runStartTime;
    int runs;

    void Start ()
    {
        GameManager.Instance.OnGameOver += GameOver;
        GameManager.Instance.OnPlaying += Playing;
	}

    void Playing()
    {
        runStartTime = Time.unscaledTime;
        runs += 1;

        ChunksManager = FindObjectOfType<ChunksManager>();

        Debug.Log(ChunksManager);
    }

    void GameOver()
    {
        int score = ScoreManager.Instance.CurrentScore;
        int chunks = ChunksManager.ChunkIndex;
        int mixtape = MixtapesManager.Instance.MixtapeIndex;
        float deltaRunTime = runStartTime - Time.unscaledTime;

        Amplitude.Instance.logEvent("RunEnd", new Dictionary<string, object>()
        {
            {"Score", score},
            {"Number of chunks", chunks},
            /*{"Chunk ID" , },*/
            {"Mixtape on dead", mixtape},
            {"Run time", deltaRunTime }
        });
    }

    void OnApplicationQuit()
    {
        Amplitude.Instance.logEvent("Game Session End", new Dictionary<string, object>()
        {
            {"Play time", Time.unscaledTime},
            {"Number of runs", runs}
        });
    }

}
