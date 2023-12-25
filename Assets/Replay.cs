using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Destroy")]
    private GameObject playerPrefab;

    // 追加：プレイヤーが消える直前の位置
    private Vector3 playerPosition;

    // Update is called once per frame
    void Update()
    {
        // 設定したplayerPrefabと同じ名前(今回は"PlayerSphere")のGameObjectを探して取得
        GameObject playerObj = GameObject.Find(playerPrefab.name);

        // playerObjが存在していない場合
        if (playerObj == null)
        {
            // playerPrefabから新しくGameObjectを作成
            // 修正：作成時に作成する場所(playerPosition)を指定
            GameObject newPlayerObj = Instantiate(playerPrefab, playerPosition, Quaternion.identity);

            // 追加：リプレイされたことがわかりやすいように色を変えた
            newPlayerObj.GetComponent<Renderer>().material.color = Color.yellow;

            // 新しく作成したGameObjectの名前を再設定(今回は"PlayerSphere"となる)
            newPlayerObj.name = playerPrefab.name;
            // ※ここで名前を再設定しない場合、自動で決まる名前は、"PlayerSphere(Clone)"となるため
            //   13行目で探している"PlayerSphere"が永遠に見つからないことになり、playerが無限に生産される
            //   どういうことかは、23行目をコメントアウトしてゲームを実行すればわかります。
        }
        else
        {
            // 追加：プレイヤーの位置を都度更新
            playerPosition = playerObj.transform.localPosition;

        }
    }
}