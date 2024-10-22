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

    void HitSuccess() // 노드를 성공적으로 잘랐을때 처리 함수
    {
        combo++;
        score += 50 * bonusMultiplier;

        if (bonusMultiplier < 8) // 8배면 보너스 끝
        {
            if(combo <= needComboforMulti) // 콤보가 일정 수 이상 도달시 보너스 X2 X4 X8등을 해주는 코드
            {
                needComboforMulti += needComboforMulti * 2;
                bonusMultiplier *= 2;

                multiplierLoader.fillAmount = combo / needComboforMulti;


            }
        }


        scoreText.text = score.ToString();
    }

    void HitFaild() // 노드를 한번 놓쳤을때
    {
        combo = 0;

    }










    // 음악 관ㄹㅣ
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
