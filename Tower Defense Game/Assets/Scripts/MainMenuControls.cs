using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{

    GameMasterControls GMC;
    Canvas the_Canvas;

    GameObject Canvas_MainMenu;
    GameObject Canvas_RoundEnd;
    GameObject Canvas_GameWin;
    GameObject Canvas_GameLoss;

    //Upgrade Menu
    GameObject DefaultRoundEndList;
    GameObject ShopList;


    Text RoundEnd_Text;
    Text GameWin_Text;
    Text GameLoss_Text;
    string RoundEnd_Default_string;
    string GameWon_string;
    string GameLoss_string;
    public void Start()
    {
        GMC = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMasterControls>();

        the_Canvas = GetComponent<Canvas>();
        Canvas_MainMenu = the_Canvas.transform.GetChild(1).gameObject;
        Canvas_RoundEnd = the_Canvas.transform.GetChild(2).gameObject;
        Canvas_GameWin = the_Canvas.transform.GetChild(3).gameObject;
        Canvas_GameLoss = the_Canvas.transform.GetChild(4).gameObject;

        DefaultRoundEndList = Canvas_RoundEnd.transform.GetChild(0).gameObject;
        ShopList = Canvas_RoundEnd.transform.GetChild(1).gameObject;

        //Text is varying and chanegs depending on points, so all text and their canvas holders are defined here

        RoundEnd_Text = Canvas_RoundEnd.transform.GetChild(0).GetChild(1).GetComponent<Text>();
        GameWin_Text = Canvas_GameWin.transform.GetChild(1).GetComponent<Text>();
        GameLoss_Text = Canvas_GameLoss.transform.GetChild(1).GetComponent<Text>();


        RoundEnd_Default_string = "You've beaten round {0} ! Good job!";
        GameWon_string = "You beat the game!! Nice! Your score is <color=red> {0} </color>";
        GameLoss_string = "It was a good try - your score was <color=red> {0} </color>";
    }

    public void OnStartGameButtonPress()
    {
        Canvas_MainMenu.SetActive(false);
        GMC.SetWaitingOnPlayerFlag(false);
    }


    public void OnNextRoundButtonPress()
    {
        Canvas_RoundEnd.SetActive(false);
        GMC.SetWaitingOnPlayerFlag(false);
    }

    public void OnExitPress()
    {
        Application.Quit();
    }



    //Using invokes to allow player some time to breathe with no activity (no enemies on screen)
    //Flashing the victory screen immediatly can be overwhelming after a few rounds.
    public void ShowEndRoundScreenDelay()
    {
        Invoke("ShowRoundEndScreen", 2f);
    }

    public void ShowGameEndScreenDelay()
    {
        Invoke("ShowGameEndScreen", 1f);

    }
//Game loss screen should be immediately shown to show preciselyu the moment the loss occured. Doesn't use Invoke.
    public void ShowGameLossScreen()
    {
        Canvas_GameLoss.SetActive(true);
        GameLoss_Text.text = string.Format(GameLoss_string, GMC.points);

    }


    void ShowRoundEndScreen()
    {
        UpdateText();
        Canvas_RoundEnd.SetActive(true);
    }

    void ShowGameEndScreen()
    {
        GameWin_Text.text = string.Format(GameWon_string, GMC.points);
        Canvas_GameWin.SetActive(true);
        if (PlayerPrefs.GetInt("Highscore", 0) < GMC.points)
        {
            PlayerPrefs.SetInt("Highscore", GMC.points);

        }

    }



    public void UpdateText()
    {
        RoundEnd_Text.text = string.Format(RoundEnd_Default_string, GMC.DifficultyLevel);
    }



    public void OnBackToMenuFromShopButtonPressed()
    {
        ShopList.SetActive(false);
        DefaultRoundEndList.SetActive(true);
    }


    public void OnShopButtonPress()
    {
        ShopList.GetComponent<EnablePurchasableOptions>().UpdatePurchasables();
        ShopList.SetActive(true);
        DefaultRoundEndList.SetActive(false);
    }


    public void ReturnToBuyMenuAfterTowerPlace()
    {
        foreach (Transform menu_item in ShopList.transform)
        {
            if (menu_item.GetComponent<Text>() != null)
            {
                menu_item.gameObject.SetActive(false);
            }
            else
            {
                menu_item.gameObject.SetActive(true);

            }
        }
    }


}
