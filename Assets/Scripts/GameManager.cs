using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState { Playing, GameOver, TESTING };
    
    public GameState State { get; private set; }
    
    
    void Update()
    {
        switch (State)
        {
            case GameState.Playing:
                CheckForVictory();
                break;

            case GameState.GameOver:
                print("You Won!");
                State = GameState.TESTING;
                break;
            
            case GameState.TESTING:
                break;
        }
    }

    void CheckForVictory()
    {
        GameObject[] goals = GameObject.FindGameObjectsWithTag("Goal");
        if (goals.All(g => g.GetComponent<Goal>().IsPressed))
            State = GameState.GameOver;
    }
}
