using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterControls : MonoBehaviour
{

    public int DifficultyLevel;
    public int max_lvl = 5;
    public bool enemies_exist = false;
    public List<GameObject> active_GOs;


    public bool round_in_progress = false;
    public bool spawning_in_progress = false;
    protected bool waiting_on_player_input = true;


    public int points = 0;
    public int lives = 5;

    public Text point_value;
    public Text life_value;

    protected bool is_paused = false;
    protected bool force_pause = false;

    MainMenuControls MMC;

    public GameObject PauseText;

    public bool is_placing_tower = false;

    private void Start()
    {
        MMC = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MainMenuControls>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !force_pause && !waiting_on_player_input)
        {
            TogglePauseGame();
        }
    }


    public void TogglePauseGame()
    {
        is_paused = !is_paused;
        PauseText.SetActive(!PauseText.activeSelf);
        if(is_paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void ForcePause()
    {
        force_pause = true;
        Time.timeScale = 0;
    }


    public bool GetWaitingOnInputFlag()
    {
        return waiting_on_player_input;
    }

    public void SetWaitingOnPlayerFlag(bool to_set)
    {
        waiting_on_player_input = to_set;
    }


    public void OnRoundEnd()
    {
        MMC.ShowEndRoundScreenDelay();
    }
    public void OnGameEnd()
    {
        MMC.ShowGameEndScreenDelay();
    }


    public void ChangePoints(int pointchange)
    {
        points += pointchange;
        if(points < 0 )
        {
            points = 0;
        }
        point_value.text = points.ToString();
    }
    public void DeductLifePoint()
    {
        lives--;
        if(lives <= 0)
        {
            //Game Over!!!
            MMC.ShowGameLossScreen();
            ForcePause();
        }
        life_value.text = lives.ToString();

    }


}
