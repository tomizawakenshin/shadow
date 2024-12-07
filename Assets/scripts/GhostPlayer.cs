using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    private List<PlayerState> playbackStates;
    private int currentIndex = 0;
    private float startTime;

    private bool isActive = true;
    private int isActiveNumber = 0;

    public void StartReplay(List<PlayerState> states)
    {
        // 記録された状態をコピー
        playbackStates = new List<PlayerState>(states);
        startTime = Time.time;
        currentIndex = 0;  // currentIndex をリセット
        isActive = true;
    }

    void Update()
    {
        if (playbackStates == null || currentIndex >= playbackStates.Count)
        {
            // 再生終了
            return;
        }

        // 経過時間に応じて再生
        float elapsedTime = Time.time - startTime;
        PlayerState state = playbackStates[currentIndex];

        if (elapsedTime >= state.time)
        {
            if (isActive)
            {
                isActive = false;
                Debug.Log("Ghost replay is starting " + isActiveNumber);
                isActiveNumber++;
            }
            transform.position = state.position;
            transform.rotation = state.rotation;
            currentIndex++;
        }
    }
}
