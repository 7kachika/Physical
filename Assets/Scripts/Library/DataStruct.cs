using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameServerLibrary
{
    #region -- DataStruct --
    /// <summary>
    /// GamePlay
    /// </summary>
    public class GamePlay
    {
        public int ballPoolCount;
        public int gravity;
        public int resistive;

        public GamePlay()
        {
            ballPoolCount = 1;
            gravity = 98;
            resistive = 0;
        }
    } 

    /// <summary>
    /// Ball
    /// </summary>
    public class Ball
    {
        public int mass;
        public int angle;
        public int force;

        public Ball()
        {
            mass = 10;
            angle = 45;
            force = 10;
        }
    }
    #endregion
}
