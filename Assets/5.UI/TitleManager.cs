using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TitleManager : MonoBehaviour
{

    public GameObject firstViewer;

    public GameObject selectedViewer;

    public TextMeshProUGUI MaxScore;

    IEnumerator Start()
    {
        MaxScore.text = "BestScore : " + PlayerPrefs.GetInt("MaxScore");
        yield return new WaitForSeconds(2f);
        viewerFirstCanvas();

    }

    public void viewerFirstCanvas()
    {
        firstViewer.SetActive(true);
    }

    public void selectedViewerView()
    {
        selectedViewer.SetActive(true);
    }

    public void selectedCancle()
    {
        Animator ani = selectedViewer.GetComponent<Animator>();
        ani.SetTrigger("WindowOut");
        Invoke("selcetedViewerEnableFalse", 0.5f);
        
    }

    public void selcetedViewerEnableFalse()
    {
        selectedViewer.SetActive(false);
        Animator ani = selectedViewer.GetComponent<Animator>();
    }

    public void GotoSongGame()
    {
        string SceneName;
        SceneManager.LoadScene("SampleScene");
    }
}
