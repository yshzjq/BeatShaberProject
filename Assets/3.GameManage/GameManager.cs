using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float startDelayAudioTime = 2.8f;

    AudioSource audioPlayer;

    public Text multiplierText;
    public Text scoreText;
    public Text comboText;
    public Image multiplierLoader;

    
    private int bonusMultiplier = 1;
    private int needComboforMulti = 2;
    
    private int comboHelpCal = 0;

    public GameObject ResultCanvas;
    public GameObject LineLight;

    private int score = 0;
    private int combo = 0;
    public int maxNodes = 137;
    private int cutedNodes = 0;
    private int currenthighScore = 0;
    private int maxScore = 51300;


    public TextMeshProUGUI cutedNodeText;
    public TextMeshProUGUI maxNodeText;
    public TextMeshProUGUI scoreResultText;
    public TextMeshProUGUI RankText;
    public GameObject highNoticeText;

    public GameObject Loops;
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

        currenthighScore = PlayerPrefs.GetInt("Tutorial_Song_High_Score");
    }

    public void HitSuccess() // 노드를 성공적으로 잘랐을때 처리 함수
    {
        combo++;
        cutedNodes++;
        score += 50 * bonusMultiplier;

        if (bonusMultiplier < 8) // 8배면 보너스 끝
        {
            multiplierLoader.fillAmount = ((float)(combo - comboHelpCal) / (bonusMultiplier*2));

            if (combo >= needComboforMulti) // 콤보가 일정 수 이상 도달시 보너스 X2 X4 X8등을 해주는 코드
            {
                needComboforMulti += (needComboforMulti-comboHelpCal) * 2;
                bonusMultiplier *= 2;
                if (bonusMultiplier >= 8)
                {
                    multiplierLoader.fillAmount = 1;
                    
                }
                else multiplierLoader.fillAmount = 0;


                comboHelpCal = combo;

                         

                multiplierText.text = bonusMultiplier.ToString();
            }
        }

        comboText.text = combo.ToString();
        scoreText.text = score.ToString();
    }

    public void HitFaild() // 노드를 한번 놓쳤을때
    {
        combo = 0;
        needComboforMulti = 2;
        bonusMultiplier = 1;
        comboHelpCal = 0;
        multiplierLoader.fillAmount = 0;

        comboText.text = combo.ToString();
        multiplierText.text = bonusMultiplier.ToString();

    }



    // 음악 관리
    public void StartCoroutine_StartMusic()
    {
        StartCoroutine("StartMusic");
    }

    IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(startDelayAudioTime);

        GetComponent<AudioSource>().Play();
    }

    public void AppearResult()
    {
        if (score >= maxScore * 0.9) RankText.text = "SSS";
        else if (score >= maxScore * 0.8) RankText.text = "SS";
        else if (score >= maxScore * 0.7) RankText.text = "S";
        else if (score >= maxScore * 0.6) RankText.text = "A";
        else if (score >= maxScore * 0.5) RankText.text = "B";
        else if (score >= maxScore * 0.4) RankText.text = "C";
        else if (score >= maxScore * 0.3) RankText.text = "D";
        else RankText.text = "F";

        if(PlayerPrefs.GetInt("MaxScore") < score)
        {
            PlayerPrefs.SetInt("MaxScore", score);
            highNoticeText.SetActive(true);
        }

        scoreResultText.text = score / 1000 + " " + score % 1000;

        maxNodeText.text = "/ " + maxNodes.ToString();
        cutedNodeText.text = cutedNodes.ToString();


        


        ResultCanvas.SetActive(true);
    }

    public void DIsAppearResult()
    {
        StartCoroutine("DisAppearResultCoroutine");
    }

    IEnumerator DisAppearResultCoroutine()
    {
        StartCoroutine("LoopUpping");
        Animator ani = ResultCanvas.GetComponent<Animator>();
        ani.SetTrigger("WindowOut");

        LineLight.SetActive(false);


        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            RenderSettings.fogColor = RenderSettings.fogColor * new Color(0.97f, 0.97f, 0.97f);

            if(RenderSettings.fogColor.b <= 0.05f)
            {
                RenderSettings.fogColor = Color.black;
                break;
            }
        }

        GoToTitle();

        
    }

    void GoToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    IEnumerator LoopUpping()
    {
        yield return new WaitForSeconds(0.1f);
        Animator ani = Loops.GetComponent<Animator>();
        ani.SetTrigger("UP");

    }


}
