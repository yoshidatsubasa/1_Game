using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    public float power = 0;
    new public Rigidbody rigidbody;
   

    private bool jampFlag = false;

    public float jumppower = 5;

    private CharacterController controller;
    private Vector3 moveDirection;

    public Slider slider;
    public Slider slider2;

    private bool isDelayedDecreasing = false; // 遅延減少中かどうかを示すフラグ

    int coinCount;

    [SerializeField]
    GameObject boostUI;
    private Coroutine speedUpCoroutine;

    bool isInputActive = false; // 入力状態を保持する変数
    float verticalInput = 0f; // 入力を保持する変数

    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioSource source3;
    [SerializeField] AudioSource source4;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;

    private int boostCount = 0; // ブーストのカウント
    private int boostsUsed = 0; // 使用されたブーストの数
    public Text boostCountText; // UI Text要素
    bool isFlickering = false; // 点滅中かどうかを示すフラグ
    Color originalColor; // テキストの元の色を保持する変数

    
    private bool isInvincible = false;  // 無敵状態を追跡するためのフラグを追加

    public Image leftImage; // 左方向のイメージ
    public Image rightImage; // 右方向のイメージ

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    private bool isInputActive2 = false; // 入力の状態を監視

    public Image playerImage; // Imageコンポーネントを持つオブジェクト



    private Coroutine uiFlickerCoroutine; // UI点滅コルーチンの参照

    void Start()
    {
        // CharacterControllerを取得
        controller = GetComponent<CharacterController>();
        // AudioSourceコンポーネントを取得する
        //damageSound = GetComponent<AudioSource>();

        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        slider.value = 5;
        slider2.value = 5;

        originalColor = boostCountText.color; // テキストの元の色を取得
        //マウスでクリックできないようにする
        slider.interactable = false;
        slider2.interactable = false;

        // HPが1以下になったら他のUIを点滅させる
        if (slider.value <= 1 && uiFlickerCoroutine == null)
        {
           

            if (playerImage != null)
            {
                StartCoroutine(FlickerPlayerImage()); // Imageを点滅させるコルーチンを開始
            }
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        
        float horizontalMovement = Input.GetAxis("Horizontal2"); // 左右のスティックや十字キーの水平方向の入力を受け取る
        float verticalMovement = Input.GetAxis("Vertical"); // 上下のスティックや十字キーの垂直方向の入力を受け取る


        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    rigidbody.AddForce(new Vector3(-1, 0, 0) * power);
        //}

        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    rigidbody.AddForce(new Vector3(1, 0, 0) * power);
        //}

        //if (Input.GetKey(KeyCode.UpArrow) && !jampFlag)
        //{
        //    rigidbody.velocity = Vector3.up * jumppower;
        //    jampFlag = true;
        //}


        //-------------------------------------------------------------------
        //// Xboxコントローラーのボタンに対応させる
        verticalInput = Input.GetAxis("Horizontal2");
        if (verticalInput > 0 && !isInputActive && !isLeftPressed) // 入力があってかつ前回入力がなかった場合
        {
            isInputActive = true; // 入力状態を更新
            rigidbody.AddForce(new Vector3(1, 0, 0) * power);
          
        }
        else if (verticalInput < 0 && !isInputActive && !isRightPressed)
        {
            isInputActive = true;
            rigidbody.AddForce(new Vector3(-1, 0, 0) * power);
        }

        if (Input.GetButton("YButton") && !jampFlag)
        {
            rigidbody.velocity = Vector3.up * jumppower;
            jampFlag = true;

            source2.PlayOneShot(clip2);
        }

        //----------------------------------------------------------------------------------
        float horizontalMovement2 = Input.GetAxis("Horizontal2");

        // 左右の入力を受け取る
        if (horizontalMovement2 > 0 && !isInputActive2 && !isRightPressed)
        {
           
            ApplyForceAndChangeImage(Vector3.left, rightImage, leftImage);

            isLeftPressed = false;
            isRightPressed = true;
        }
        else if (horizontalMovement2 < 0 && !isInputActive2 && !isLeftPressed)
        {
            ApplyForceAndChangeImage(Vector3.right, leftImage, rightImage);
            isLeftPressed = true;
            isRightPressed = false;
        }

        if (horizontalMovement2 == 0)
        {
            isLeftPressed = false;
            isRightPressed = false;
            isInputActive2 = false;

            // 赤い色をクリアに戻す
            if (leftImage != null && rightImage != null)
            {
                leftImage.color = Color.white;
                rightImage.color = Color.white;
            }
        }
        
       
        

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);

        rigidbody.AddForce(movement * power * Time.deltaTime);

       
        coinCount = Coincounter.getscore();

        if (coinCount > 0 && coinCount % 10 == 0 && boostCount < coinCount / 10) // 10の倍数かつスピードアップ中でない場合
        {
            // 10の倍数かつスピードアップ中でない場合
            // ブーストカウントを増やす
            boostCount = coinCount / 10;
            UpdateBoostCountText(); // ブーストカウントの更新後にUIテキストを更新
            BoostCountedEffect(); // ブーストがカウントされたときの効果
        }

        if (Input.GetButtonDown("LBButton2") && boostCount>0&&boostsUsed<boostCount)
        {
            // ブーストがカウントされている状態でボタンが押された場合
            // スピードアップを開始する
            StartCoroutine(SpeedUpRoutine());
            boostUI.SetActive(true);
            boostsUsed++;
            UpdateBoostCountText(); // ブースト使用後にUIテキストを更新
        }
        if (slider.value<=0)
        {
            SceneManager.LoadScene("Over");
        }


        // 力を加え、イメージの色を変更する関数
        void ApplyForceAndChangeImage(Vector3 direction, Image activeImage, Image inactiveImage)
        {
            isInputActive2 = true;
            rigidbody.AddForce(direction * power * Time.deltaTime);

            // 左右のイメージを赤くする
            if (activeImage != null && inactiveImage != null)
            {
                activeImage.color = Color.red;
                inactiveImage.color = Color.white;
            }
        }
        if (boostCount - boostsUsed > 0 && !isFlickering)
        {
            StartCoroutine(FlickerBoostCountText()); // ブーストカウントが残っている場合、点滅を開始
        }
    }

    IEnumerator SpeedUpRoutine()
    {
        power = 650;
        GameObject trailObject = GameObject.Find("Player");
        TrailRenderer trailRenderer = trailObject.GetComponent<TrailRenderer>();

        // TrailRendererがnullでないことを確認して色を変更する
        if (trailRenderer != null)
        {
            // 新しい色を設定する
            trailRenderer.startColor = Color.red;
            trailRenderer.endColor = Color.red;
        }
        // ブースト時の効果音を再生する (source3とclip3を使用)
        if (source3 != null && clip3 != null)
        {
            source3.PlayOneShot(clip3);
        }

        // スピードアップ時に無敵状態にする
        isInvincible = true;
        yield return new WaitForSeconds(5.0f);

        power = 300; // スピードアップ終了時に速度を元に戻す
        boostUI.SetActive(false);
        // 元の色に戻す
        if (trailRenderer != null)
        {
            trailRenderer.startColor = Color.white;
            trailRenderer.endColor = Color.white;
        }
        isInvincible = false; // 無敵状態の終了
        yield return null;
    }

    private void UpdateBoostCountText()
    {
        if (boostCountText != null)
        {
            int remainingBoosts = boostCount - boostsUsed;
            boostCountText.text = "x" + remainingBoosts.ToString();
        }
    }

    // 無敵状態の期間を管理するコルーチン
    IEnumerator InvincibilityDuration(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    IEnumerator FlickerBoostCountText()
    {
        isFlickering = true;
        float duration = 0.5f; // 点滅の速さ
        float elapsedTime = 0f;

        while (boostCount - boostsUsed > 0)
        {
            elapsedTime += Time.deltaTime;

            // テキストの色を変更して点滅させる
            boostCountText.color = Color.Lerp(originalColor, Color.yellow, Mathf.PingPong(elapsedTime / duration, 1));

            yield return null;
        }

        // ブーストカウントが 0 の場合は元の色に戻す
        boostCountText.color = originalColor;
        isFlickering = false; // 点滅終了

    }
    // ブーストがカウントされた時の処理
    void BoostCountedEffect()
    {
        if (source4 != null && clip4 != null)
        {
            source4.PlayOneShot(clip4); // 効果音再生
        }
    }

    // 他のUI要素を点滅させるコルーチン
    IEnumerator FlickerPlayerImage()
    {
        while (slider.value <= 1)
        {
            // Imageを非表示にする
            playerImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            // Imageを再表示する
            playerImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stage Load 1"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Stage Load 2"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Stage Load 3"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube 1"))
        {
            jampFlag = false;
        }

        if (collision.gameObject.CompareTag("Cube 2"))
        {
            jampFlag = false;
        }




        if (collision.gameObject.CompareTag("Cube 3"))
        {
            jampFlag = false;
        }
        if (collision.gameObject.name == "Red" && !isDelayedDecreasing && !isInvincible)
        {
            int damage = 1;                           //一回のダメージ
            slider.value-=damage;　　　　　　　　　　 //即座に減少
            StartCoroutine(DelayedSliderDecrease(damage));  //遅れて減少 

            PlayerEffect flickerScript = GetComponent<PlayerEffect>();
            if (flickerScript != null)
            {
                flickerScript.StartFlicker();
            }

            source1.PlayOneShot(clip1);

            // HPが1以下になったら他のUIを点滅させる
            if (slider.value <= 1 && uiFlickerCoroutine == null)
            {
                if (playerImage != null)
                {
                    StartCoroutine(FlickerPlayerImage()); // Imageを点滅させるコルーチンを開始
                }
            }
        }
    }

    private System.Collections.IEnumerator DelayedSliderDecrease(int damage)
    {
        float decreaseDuration = 1.0f; // 減少時間（秒）
        float startTime = Time.time;
        float startValue = slider2.value;

        isDelayedDecreasing = true; // 遅延減少中に設定

        while (Time.time - startTime < decreaseDuration && damage > 0)
        {
            float currentTime = Time.time - startTime;
            float decreaseAmount = Mathf.Lerp(startValue, startValue - damage, currentTime / decreaseDuration);
            slider2.value = Mathf.Max(0, decreaseAmount); // スライダーの値を0未満にならないように調整
            yield return null;
        }

        slider2.value = slider.value; // slider2の値を即座に減少したスライダーの値に合わせる
        isDelayedDecreasing = false; // 遅延減少終了
    }


}
