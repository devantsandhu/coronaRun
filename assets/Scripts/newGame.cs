using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class newGame : MonoBehaviour
{
    private string menu = "menu";
    void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene(menu);
        gameStats.Deaths = 0;
        gameStats.Points = 0;
    }
}
