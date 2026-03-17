using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject newObj = new GameObject("GameManager");
                instance = newObj.AddComponent<GameManager>();
            }

            return instance;
        }
    }

    [Header("Stutter Settings")]
    [SerializeField] private float stutterTime = .3f;
    [SerializeField] private float stutterTimeScale = .5f;


    private IEnumerator stutterCoroutine;

    public bool playerIsDead;

    public ServingSystem servingSystem;

    private void Awake()
    {
        instance = this;
        stutterCoroutine = StutterCoroutine();
    }

    public void Stutter()
    {
        StopCoroutine(stutterCoroutine);
        StartCoroutine(StutterCoroutine());
    }

    private IEnumerator StutterCoroutine()
    {
        Time.timeScale = stutterTimeScale;
        yield return new WaitForSeconds(stutterTime);
        Time.timeScale = 1;
    }

    public void RestartRun()
    {
        if (!playerIsDead)
        {
            StartCoroutine(RestartRunCoroutine());
        }
    }

    private IEnumerator RestartRunCoroutine()
    {
        playerIsDead = true;
        CameraFader.Instance.FadeOut();
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("SampleScene");
    }

    public void ServingUI()
    {
        servingSystem.gameObject.SetActive(true);
        servingSystem?.StartServe();
    }

    public void EndGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Ending");
    }
}
