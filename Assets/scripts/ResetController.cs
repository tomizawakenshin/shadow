using UnityEngine;
using StarterAssets;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ResetController : MonoBehaviour
{
    public GameObject player; // プレイヤーオブジェクト
    public GameObject ghostPrefab; // ゴーストのプレハブ
    private PlayerRecorder recorder;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private ThirdPersonController thirdPersonController; // ThirdPersonControllerへの参照
    private GameObject currentGhost; // 生成されたゴーストの参照を保持

    void Start()
    {
        recorder = player.GetComponent<PlayerRecorder>();
        initialPosition = player.transform.position;
        initialRotation = player.transform.rotation;

        // ThirdPersonControllerの参照を取得
        thirdPersonController = player.GetComponent<ThirdPersonController>();
    }

    void Update()
    {
        // Enterキーが押されたときにリセットを実行
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ResetAndSpawnGhost();
        }
    }

    // リセットとゴーストの生成を行うメソッド
    void ResetAndSpawnGhost()
    {
        // 既存のゴーストが存在する場合は削除
        if (currentGhost != null)
        {
            Destroy(currentGhost);
        }

        // CharacterControllerを無効化
        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        // プレイヤーを初期位置に戻す
        player.transform.position = initialPosition;
        player.transform.rotation = initialRotation;

        // CharacterControllerを再度有効化
        if (controller != null)
        {
            controller.enabled = true;
        }

        // ThirdPersonControllerの状態をリセット
        ResetThirdPersonControllerState();

        // 新しいゴーストを生成して再生開始
        currentGhost = Instantiate(ghostPrefab, initialPosition, initialRotation);
        GhostPlayer ghostPlayer = currentGhost.GetComponent<GhostPlayer>();

        ghostPlayer.StartReplay(recorder.GetRecordedStates());

        // 記録データをクリア
        recorder.ClearRecordedStates();
    }

    void ResetThirdPersonControllerState()
    {
        if (thirdPersonController != null)
        {
            // ThirdPersonControllerの状態をリセットするメソッドを呼び出す
            thirdPersonController.ResetState();
        }
    }
}
