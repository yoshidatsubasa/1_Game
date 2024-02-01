using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BottunSelecter2 : MonoBehaviour
{
    public Button[] buttons; // 選択可能なボタンの配列
    public GameObject selectionFrame; // 枠オブジェクト

    private int currentButtonIndex = 0; // 現在の選択されているボタンのインデックス

    bool isInputActive = false; // 入力状態を保持する変数
    float verticalInput = 0f; // 入力を保持する変数

    bool isBlinking = false; // 点滅中かどうかを示すフラグ
    Coroutine blinkCoroutine; // 点滅コルーチンの参照
    bool firstButtonPress = true; // 最初のボタン押下フラグ
    bool isDecisionPressed = false; // 決定ボタンが押されたかどうかを示すフラグ

    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    IEnumerator Start()
    {
        // 最初のボタンを選択した状態にする
        SelectButton(currentButtonIndex);
        yield return null;
        StartBlinking();
    }

    void Update()
    {
        if (isDecisionPressed)
        {
            // 決定ボタンが押された後は十字キーの入力を無視
            return;
        }
        //-------------------------------------------------------------------------------------------------
        //矢印キーVer

        //float verticalInput2 = Input.GetAxis("Horizontal");

        //if (verticalInput2 != 0 && !isInputActive)
        //{
        //    isInputActive = true; // 入力状態を更新
        //    if (verticalInput2 > 0)
        //    {
        //        SelectPreviousButton();
        //    }
        //    else if (verticalInput2 < 0)
        //    {
        //        SelectNextButton();
        //    }
        //}
        //else if (verticalInput2 == 0)
        //{
        //    isInputActive = false; // 入力状態をリセット
        //}

        //if (Input.GetKey(KeyCode.Space)) // XboxコントローラーのAボタンが押されたらボタンを実行する
        //{
        //    if (firstButtonPress)
        //    {
        //        firstButtonPress = false; // 最初のボタン押下フラグを解除
        //    }
        //    PressSelectedButton();
        //    if (!isBlinking) // 点滅中でなく、かつ最初のボタンが押されていない場合点滅開始
        //    {
        //        StartBlinking();
        //    }
        //}
        //-------------------------------------------------------------------------------------------------
        //コントローラーVer

        verticalInput = Input.GetAxis("Vertical2");

        if (verticalInput > 0 && !isInputActive) // 入力があってかつ前回入力がなかった場合
        {
            SelectNextButton();
            isInputActive = true; // 入力状態を更新
        }
        else if (verticalInput < 0 && !isInputActive)
        {
            SelectPreviousButton();
            isInputActive = true;
        }
        else if (verticalInput == 0) // 入力がない場合
        {
            isInputActive = false; // 入力状態をリセット
        }
        if (Input.GetButtonDown("AButton")) // XboxコントローラーのAボタンが押されたらボタンを実行する
        {
            if (firstButtonPress)
            {
                firstButtonPress = false; // 最初のボタン押下フラグを解除
            }
            PressSelectedButton();
            if (!isBlinking) // 点滅中でなく、かつ最初のボタンが押されていない場合点滅開始
            {
                StartBlinking();
            }
        }
    }

    void SelectButton(int index)
    {
        // 選択されたボタンの位置に枠を移動する
        Vector3 selectedPosition = buttons[index].transform.position;
        selectionFrame.transform.position = selectedPosition;
    }

    void SelectNextButton()
    {
        source1.PlayOneShot(clip1);
        // 次のボタンを選択
        currentButtonIndex++;
        if (currentButtonIndex >= buttons.Length)
        {
            currentButtonIndex = 0;
        }
        SelectButton(currentButtonIndex);
        StopBlinking(); // 点滅を停止
    }

    void SelectPreviousButton()
    {
        source1.PlayOneShot(clip1);
        // 前のボタンを選択
        currentButtonIndex--;
        if (currentButtonIndex < 0)
        {
            currentButtonIndex = buttons.Length - 1;
        }
        SelectButton(currentButtonIndex);
        StopBlinking(); // 点滅を停止
    }

    void PressSelectedButton()
    {
        source2.PlayOneShot(clip2);
        // 選択されているボタンを実行する
        buttons[currentButtonIndex].onClick.Invoke();
        isDecisionPressed = true; // 決定ボタンが押されたことを示すフラグを設定

    }


    IEnumerator BlinkCoroutine()
    {
        isBlinking = true; // 点滅中フラグを設定

        while (firstButtonPress) // 最初のボタンが押されるまで点滅しない
        {
            yield return null;
        }

        while (true)
        {
            selectionFrame.GetComponent<Image>().color = Color.red; // 赤に変更
            yield return new WaitForSeconds(0.1f); // 0.5秒待つ

            selectionFrame.GetComponent<Image>().color = Color.clear; // 白に変更
            yield return new WaitForSeconds(0.1f); // 0.5秒待つ
        }
    }

    void StartBlinking()
    {
        blinkCoroutine = StartCoroutine(BlinkCoroutine());
    }

    void StopBlinking()
    {
        if (isBlinking && blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine); // 点滅を停止
            isBlinking = false; // 点滅中フラグを解除
            selectionFrame.GetComponent<Image>().color = Color.white; // 白に戻す
        }
    }
}