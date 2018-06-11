using GameServerLibrary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPool {

    #region -- Manager --
    public static GameManager gameMgr { get; set; }
    public static Camera gameCamera { get; set; }
    #endregion

    #region -- Data --
    public static GamePlay gamePlay { get; set; }
    public static Queue<GameObject> balls { get; set; }
    public static Queue<GameObject> ballPool { get; set; }
    public static Ball ball { get; set; }
    #endregion

    #region -- KeyCode --
    public static KeyCode jump { get; set; }
    public static KeyCode forward { get; set; }
    public static KeyCode backward { get; set; }
    public static KeyCode left { get; set; }
    public static KeyCode right { get; set; }
    public static KeyCode leftturn { get; set; }
    public static KeyCode rightturn { get; set; }
    #endregion

    /// <summary>
    /// Static constructor
    /// </summary>
    static DataPool()
    {
        gamePlay = new GamePlay();
        balls = new Queue<GameObject>();
        ballPool = new Queue<GameObject>();
        ball = new Ball();
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpkey", "Space"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardkey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardkey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftkey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightkey", "D"));
        leftturn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftturnkey", "Q"));
        rightturn = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightturnkey", "E"));
    }
}
