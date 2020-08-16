using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { Playing, GameOver, TESTING };

    public Text Level1Text, Level2Text, Level3Text, Level4Text;
    
    public GameState State { get; private set; }
    public RectTransform blackPanel;
    public AudioSource LightSwitchSound;
    public GameObject WallLevel1to2, Level4;

    public Animator LevelAnimator;

    public int LevelCounter = 0;

    void Update()
    {

    }

    public void NextLevel()
    {
        switch (LevelCounter)
        {
            case 1:
                Level2Text.gameObject.SetActive(true);
                blackPanel.sizeDelta = new Vector2(811f, 1000f);
                LightSwitchSound.Play();
                break;
            case 2:
                Level3Text.gameObject.SetActive(true);
                blackPanel.sizeDelta = new Vector2(0f, 0f);
                LightSwitchSound.Play();
                LevelAnimator.Play("Level2ToLevel3");
                break;
            case 3:
                Level4.gameObject.SetActive(true);
                LevelAnimator.Play("Level3ToLevel4", 5);
                break;
            case 4:
                Level4Text.gameObject.SetActive(true);
                break;

            default:
                Debug.Log("Default case");
                break;

        }
        LevelCounter++;

    }
}
