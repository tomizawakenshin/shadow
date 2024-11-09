using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    private List<PlayerState> playbackStates;
    private int currentIndex = 0;
    private float startTime;

    public void StartReplay(List<PlayerState> states)
    {
        // 記録された状態をコピー
        playbackStates = new List<PlayerState>(states);
        startTime = Time.time;
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
            transform.position = state.position;
            transform.rotation = state.rotation;
            currentIndex++;
        }
    }
}
