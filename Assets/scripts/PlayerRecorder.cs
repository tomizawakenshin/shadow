using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    public float recordDuration = 10f; // 記録する時間（秒）
    private List<PlayerState> recordedStates = new List<PlayerState>();
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 現在の状態を記録
        recordedStates.Add(new PlayerState(transform.position, transform.rotation, timer));

        // 古いデータを削除して10秒間のデータを保持
        while (recordedStates.Count > 0 && timer - recordedStates[0].time > recordDuration)
        {
            recordedStates.RemoveAt(0);
        }
    }

    // ゴースト再生用にデータを取得
    public List<PlayerState> GetRecordedStates()
    {
        return recordedStates;
    }

    // 記録データをクリア
    public void ClearRecordedStates()
    {
        recordedStates.Clear();
        timer = 0f;
    }
}
