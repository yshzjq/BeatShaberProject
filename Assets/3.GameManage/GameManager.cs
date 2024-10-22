using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float startDelayAudioTime = 2.8f;

    AudioSource audioPlayer;

    public Text multiplierText;
    public Text scoreText;
    public Image multiplierLoader;
    public CanvasGroup canvasGroup;

    private int score = 0;
    private int bonusMultiplier = 1;
    private int needComboforMulti = 2;
    private int combo = 0;
    private int comboHelpCal = 0;

    private void OnEnable()
    {
        multiplierText.text = score.ToString();
    }
    void Start()
    {

        if (instance == null)
        {
            instance = this;
            audioPlayer = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void HitSuccess() // ��带 ���������� �߶����� ó�� �Լ�
    {
        combo++;
        score += 50 * bonusMultiplier;

        if (bonusMultiplier < 8) // 8��� ���ʽ� ��
        {
            if(combo <= needComboforMulti) // �޺��� ���� �� �̻� ���޽� ���ʽ� X2 X4 X8���� ���ִ� �ڵ�
            {
                needComboforMulti += needComboforMulti * 2;
                bonusMultiplier *= 2;

                multiplierLoader.fillAmount = combo / needComboforMulti;


            }
        }


        scoreText.text = score.ToString();
    }

    void HitFaild() // ��带 �ѹ� ��������
    {
        combo = 0;

    }










    // ���� ������
    public void StartCoroutine_StartMusic()
    {
        StartCoroutine("StartMusic");
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(startDelayAudioTime);

        GetComponent<AudioSource>().Play();
    }
}
