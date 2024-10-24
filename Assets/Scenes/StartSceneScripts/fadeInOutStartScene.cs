using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class fadeInOutStartScene : MonoBehaviour
{
    CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        StartCoroutine("fadein");
    }

    IEnumerator fadein()
    {

        for (int i = 100; i > 0; i--)
        {
            canvasGroup.alpha = 1f / i;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(5f);

        for (int i = 0; i < 100; i++)
        {
            canvasGroup.alpha = 1f / i;
            yield return new WaitForSeconds(0.01f);
        }
        canvasGroup.alpha = 0;

        SceneManager.LoadScene("TitleScene");
    }

}
