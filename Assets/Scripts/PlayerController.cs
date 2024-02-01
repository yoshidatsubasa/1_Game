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

    private bool isDelayedDecreasing = false; // �x�����������ǂ����������t���O

    int coinCount;

    [SerializeField]
    GameObject boostUI;
    private Coroutine speedUpCoroutine;

    bool isInputActive = false; // ���͏�Ԃ�ێ�����ϐ�
    float verticalInput = 0f; // ���͂�ێ�����ϐ�

    [SerializeField] AudioSource source1;
    [SerializeField] AudioSource source2;
    [SerializeField] AudioSource source3;
    [SerializeField] AudioSource source4;
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;

    private int boostCount = 0; // �u�[�X�g�̃J�E���g
    private int boostsUsed = 0; // �g�p���ꂽ�u�[�X�g�̐�
    public Text boostCountText; // UI Text�v�f
    bool isFlickering = false; // �_�Œ����ǂ����������t���O
    Color originalColor; // �e�L�X�g�̌��̐F��ێ�����ϐ�

    
    private bool isInvincible = false;  // ���G��Ԃ�ǐՂ��邽�߂̃t���O��ǉ�

    public Image leftImage; // �������̃C���[�W
    public Image rightImage; // �E�����̃C���[�W

    private bool isLeftPressed = false;
    private bool isRightPressed = false;

    private bool isInputActive2 = false; // ���͂̏�Ԃ��Ď�

    public Image playerImage; // Image�R���|�[�l���g�����I�u�W�F�N�g



    private Coroutine uiFlickerCoroutine; // UI�_�ŃR���[�`���̎Q��

    void Start()
    {
        // CharacterController���擾
        controller = GetComponent<CharacterController>();
        // AudioSource�R���|�[�l���g���擾����
        //damageSound = GetComponent<AudioSource>();

        Screen.SetResolution(1920, 1080, false);
        Application.targetFrameRate = 60;

        slider.value = 5;
        slider2.value = 5;

        originalColor = boostCountText.color; // �e�L�X�g�̌��̐F���擾
        //�}�E�X�ŃN���b�N�ł��Ȃ��悤�ɂ���
        slider.interactable = false;
        slider2.interactable = false;

        // HP��1�ȉ��ɂȂ����瑼��UI��_�ł�����
        if (slider.value <= 1 && uiFlickerCoroutine == null)
        {
           

            if (playerImage != null)
            {
                StartCoroutine(FlickerPlayerImage()); // Image��_�ł�����R���[�`�����J�n
            }
        }
    }

   
    // Update is called once per frame
    void Update()
    {
        
        float horizontalMovement = Input.GetAxis("Horizontal2"); // ���E�̃X�e�B�b�N��\���L�[�̐��������̓��͂��󂯎��
        float verticalMovement = Input.GetAxis("Vertical"); // �㉺�̃X�e�B�b�N��\���L�[�̐��������̓��͂��󂯎��


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
        //// Xbox�R���g���[���[�̃{�^���ɑΉ�������
        verticalInput = Input.GetAxis("Horizontal2");
        if (verticalInput > 0 && !isInputActive && !isLeftPressed) // ���͂������Ă��O����͂��Ȃ������ꍇ
        {
            isInputActive = true; // ���͏�Ԃ��X�V
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

        // ���E�̓��͂��󂯎��
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

            // �Ԃ��F���N���A�ɖ߂�
            if (leftImage != null && rightImage != null)
            {
                leftImage.color = Color.white;
                rightImage.color = Color.white;
            }
        }
        
       
        

        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);

        rigidbody.AddForce(movement * power * Time.deltaTime);

       
        coinCount = Coincounter.getscore();

        if (coinCount > 0 && coinCount % 10 == 0 && boostCount < coinCount / 10) // 10�̔{�����X�s�[�h�A�b�v���łȂ��ꍇ
        {
            // 10�̔{�����X�s�[�h�A�b�v���łȂ��ꍇ
            // �u�[�X�g�J�E���g�𑝂₷
            boostCount = coinCount / 10;
            UpdateBoostCountText(); // �u�[�X�g�J�E���g�̍X�V���UI�e�L�X�g���X�V
            BoostCountedEffect(); // �u�[�X�g���J�E���g���ꂽ�Ƃ��̌���
        }

        if (Input.GetButtonDown("LBButton2") && boostCount>0&&boostsUsed<boostCount)
        {
            // �u�[�X�g���J�E���g����Ă����ԂŃ{�^���������ꂽ�ꍇ
            // �X�s�[�h�A�b�v���J�n����
            StartCoroutine(SpeedUpRoutine());
            boostUI.SetActive(true);
            boostsUsed++;
            UpdateBoostCountText(); // �u�[�X�g�g�p���UI�e�L�X�g���X�V
        }
        if (slider.value<=0)
        {
            SceneManager.LoadScene("Over");
        }


        // �͂������A�C���[�W�̐F��ύX����֐�
        void ApplyForceAndChangeImage(Vector3 direction, Image activeImage, Image inactiveImage)
        {
            isInputActive2 = true;
            rigidbody.AddForce(direction * power * Time.deltaTime);

            // ���E�̃C���[�W��Ԃ�����
            if (activeImage != null && inactiveImage != null)
            {
                activeImage.color = Color.red;
                inactiveImage.color = Color.white;
            }
        }
        if (boostCount - boostsUsed > 0 && !isFlickering)
        {
            StartCoroutine(FlickerBoostCountText()); // �u�[�X�g�J�E���g���c���Ă���ꍇ�A�_�ł��J�n
        }
    }

    IEnumerator SpeedUpRoutine()
    {
        power = 650;
        GameObject trailObject = GameObject.Find("Player");
        TrailRenderer trailRenderer = trailObject.GetComponent<TrailRenderer>();

        // TrailRenderer��null�łȂ����Ƃ��m�F���ĐF��ύX����
        if (trailRenderer != null)
        {
            // �V�����F��ݒ肷��
            trailRenderer.startColor = Color.red;
            trailRenderer.endColor = Color.red;
        }
        // �u�[�X�g���̌��ʉ����Đ����� (source3��clip3���g�p)
        if (source3 != null && clip3 != null)
        {
            source3.PlayOneShot(clip3);
        }

        // �X�s�[�h�A�b�v���ɖ��G��Ԃɂ���
        isInvincible = true;
        yield return new WaitForSeconds(5.0f);

        power = 300; // �X�s�[�h�A�b�v�I�����ɑ��x�����ɖ߂�
        boostUI.SetActive(false);
        // ���̐F�ɖ߂�
        if (trailRenderer != null)
        {
            trailRenderer.startColor = Color.white;
            trailRenderer.endColor = Color.white;
        }
        isInvincible = false; // ���G��Ԃ̏I��
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

    // ���G��Ԃ̊��Ԃ��Ǘ�����R���[�`��
    IEnumerator InvincibilityDuration(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    IEnumerator FlickerBoostCountText()
    {
        isFlickering = true;
        float duration = 0.5f; // �_�ł̑���
        float elapsedTime = 0f;

        while (boostCount - boostsUsed > 0)
        {
            elapsedTime += Time.deltaTime;

            // �e�L�X�g�̐F��ύX���ē_�ł�����
            boostCountText.color = Color.Lerp(originalColor, Color.yellow, Mathf.PingPong(elapsedTime / duration, 1));

            yield return null;
        }

        // �u�[�X�g�J�E���g�� 0 �̏ꍇ�͌��̐F�ɖ߂�
        boostCountText.color = originalColor;
        isFlickering = false; // �_�ŏI��

    }
    // �u�[�X�g���J�E���g���ꂽ���̏���
    void BoostCountedEffect()
    {
        if (source4 != null && clip4 != null)
        {
            source4.PlayOneShot(clip4); // ���ʉ��Đ�
        }
    }

    // ����UI�v�f��_�ł�����R���[�`��
    IEnumerator FlickerPlayerImage()
    {
        while (slider.value <= 1)
        {
            // Image���\���ɂ���
            playerImage.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            // Image���ĕ\������
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
            int damage = 1;                           //���̃_���[�W
            slider.value-=damage;�@�@�@�@�@�@�@�@�@�@ //�����Ɍ���
            StartCoroutine(DelayedSliderDecrease(damage));  //�x��Č��� 

            PlayerEffect flickerScript = GetComponent<PlayerEffect>();
            if (flickerScript != null)
            {
                flickerScript.StartFlicker();
            }

            source1.PlayOneShot(clip1);

            // HP��1�ȉ��ɂȂ����瑼��UI��_�ł�����
            if (slider.value <= 1 && uiFlickerCoroutine == null)
            {
                if (playerImage != null)
                {
                    StartCoroutine(FlickerPlayerImage()); // Image��_�ł�����R���[�`�����J�n
                }
            }
        }
    }

    private System.Collections.IEnumerator DelayedSliderDecrease(int damage)
    {
        float decreaseDuration = 1.0f; // �������ԁi�b�j
        float startTime = Time.time;
        float startValue = slider2.value;

        isDelayedDecreasing = true; // �x���������ɐݒ�

        while (Time.time - startTime < decreaseDuration && damage > 0)
        {
            float currentTime = Time.time - startTime;
            float decreaseAmount = Mathf.Lerp(startValue, startValue - damage, currentTime / decreaseDuration);
            slider2.value = Mathf.Max(0, decreaseAmount); // �X���C�_�[�̒l��0�����ɂȂ�Ȃ��悤�ɒ���
            yield return null;
        }

        slider2.value = slider.value; // slider2�̒l�𑦍��Ɍ��������X���C�_�[�̒l�ɍ��킹��
        isDelayedDecreasing = false; // �x�������I��
    }


}
