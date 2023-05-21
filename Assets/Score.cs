using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public Text times;
    public int score;

    public int timeLeft;
    public AudioSource deathSfx;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tick());
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(1f);
        timeLeft--;
        timeLeft = Mathf.Clamp(timeLeft, 0, 45);
        UpdateTime();

        if (timeLeft <= 0)
        {
            deathSfx.Play();
            anim.SetBool("Dead", true);
            GameObject.Find("Player").GetComponent<Movement>().enabled = false;
            yield return new WaitForSeconds(6f);
            GameObject.Find("Player").SetActive(false);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }

        StartCoroutine(Tick());
    }


    public void UpdateScore()
    {
        score++;
        scoreText.text = "Acculumated: " + score;
        UpdateTime();
    }

    public void UpdateTime()
    {
        times.text = timeLeft.ToString();
    }

}
