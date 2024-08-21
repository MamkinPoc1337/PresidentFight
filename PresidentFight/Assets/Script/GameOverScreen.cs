using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text winnerText;

    public void Setup(string winner)
    {
        gameObject.SetActive(true);
        winnerText.text = winner + " Winnier";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMeny");
    }
}
