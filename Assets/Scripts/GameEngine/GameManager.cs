// Copyright 2018 7kachika
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using GameServerLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public GameObject ballPrefab;

    private Transform rootTrans;
    private Transform ballTrans;

    public void InstantiatePrefab(GameObject prefab, Transform transform, string name, Queue<GameObject> queue)
    {
        Vector3 position = Vector3.zero;
        GameObject newPrefab = Instantiate(prefab, position, Quaternion.identity, transform) as GameObject;
        newPrefab.name = name;
        newPrefab.SetActive(false);
        queue.Enqueue(newPrefab);
    }

    public void NewBall()
    {
        InstantiatePrefab(ballPrefab, ballTrans, "Ball Prefab", DataPool.ballPool);
    }

    public void Shoot()
    {
        GameObject newBall = DataPool.ballPool.Dequeue();
        newBall.transform.position = transform.position + new Vector3(0, 0, 8);
        Vector3 vector = new Vector3(0, Mathf.Sin((DataPool.ball.angle * Mathf.PI) / 180), Mathf.Cos((DataPool.ball.angle * Mathf.PI) / 180));
        newBall.SetActive(true);
        newBall.GetComponent<Rigidbody>().mass = DataPool.ball.mass / 10f;
        newBall.GetComponent<Rigidbody>().AddForce(1000 * vector * DataPool.ball.force / 10f);
        DataPool.balls.Enqueue(newBall);
    }

    public void Resetup()
    {
        DataPool.ball = new Ball();
        DataPool.gamePlay = new GamePlay();
    }

    public void SetMass(int value)
    {
        if ((DataPool.ball.mass + value) > 0)
        {
            DataPool.ball.mass += value;
        }
    }

    public void SetAngle(int value)
    {
        if (((DataPool.ball.angle + value) >= 0) && ((DataPool.ball.angle + value) <= 90))
        {
            DataPool.ball.angle += value;
        }
    }

    public void SetForce(int value)
    {
        if ((DataPool.ball.force + value) >= 0)
        {
            DataPool.ball.force += value;
        }
    }

    public void SetBallPool(int value)
    {
        if (DataPool.gamePlay.ballPoolCount + value >= 1)
        {
            DataPool.gamePlay.ballPoolCount += value;
        }
    }

    public void SetGravity(int value)
    {
        if (DataPool.gamePlay.gravity + value >= 0)
        {
            if (DataPool.gamePlay.gravity + value <= 0)
            {
                DataPool.gamePlay.gravity = 0;
            }
            else
            {
                DataPool.gamePlay.gravity += value;
            }
        }
        Physics.gravity = new Vector3(0, -(DataPool.gamePlay.gravity / 10f), 0);
    }

    public void SetResistive(int value)
    {
        DataPool.gamePlay.resistive += value;
    }

    private void Awake()
    {
        if (DataPool.gameMgr == null)
        {
            DontDestroyOnLoad(gameObject);
            DataPool.gameMgr = this;
        }
        else if (DataPool.gameMgr != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rootTrans = transform;

        ballTrans = new GameObject("Ball").transform;
        ballTrans.SetParent(rootTrans);

        for (int i = 0; i < 20; i++)
        {
            InstantiatePrefab(ballPrefab, ballTrans, "Ball Prefab", DataPool.ballPool);
        }

        Resetup();
    }

    private void Update()
    {
        if (DataPool.balls.Count > DataPool.gamePlay.ballPoolCount)
        {
            GameObject removeBall = DataPool.balls.Dequeue();
            removeBall.SetActive(false);
            DataPool.ballPool.Enqueue(removeBall);
        }
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 70 + 10, 20, 50, 30), "Reset")) { Resetup(); }

        GUI.Box(new Rect(10, Screen.height - 115, 140, 105), "Ball");

        GUI.Label(new Rect(20, Screen.height - 90, 50, 30), "Mass");
        if (GUI.Button(new Rect(70, Screen.height - 90, 20, 20), "-")) { SetMass(-10); }
        GUI.Label(new Rect(100, Screen.height - 90, 20, 20), (DataPool.ball.mass / 10f).ToString());
        if (GUI.Button(new Rect(120, Screen.height - 90, 20, 20), "+")) { SetMass(10); }

        GUI.Label(new Rect(20, Screen.height - 65, 50, 30), "Angle");
        if (GUI.Button(new Rect(70, Screen.height - 65, 20, 20), "-")) { SetAngle(-5); }
        GUI.Label(new Rect(100, Screen.height - 65, 20, 20), DataPool.ball.angle.ToString());
        if (GUI.Button(new Rect(120, Screen.height - 65, 20, 20), "+")) { SetAngle(5); }

        GUI.Label(new Rect(20, Screen.height - 40, 50, 30), "Force");
        if (GUI.Button(new Rect(70, Screen.height - 40, 20, 20), "-")) { SetForce(-10); }
        GUI.Label(new Rect(100, Screen.height - 40, 20, 20), (DataPool.ball.force / 10f).ToString());
        if (GUI.Button(new Rect(120, Screen.height - 40, 20, 20), "+")) { SetForce(10); }

        GUI.Box(new Rect(Screen.width - 160 + 10, Screen.height - 115, 140, 105), "Gameplay");

        GUI.Label(new Rect(Screen.width - 160 + 20, Screen.height - 90, 50, 30), "Balls");
        if (GUI.Button(new Rect(Screen.width - 160 + 70, Screen.height - 90, 20, 20), "-")) { SetBallPool(-1); }
        GUI.Label(new Rect(Screen.width - 160 + 95, Screen.height - 90, 20, 20), DataPool.gamePlay.ballPoolCount.ToString());
        if (GUI.Button(new Rect(Screen.width - 160 + 120, Screen.height - 90, 20, 20), "+")) { SetBallPool(1); }

        GUI.Label(new Rect(Screen.width - 160 + 20, Screen.height - 65, 50, 30), "Gravity");
        if (GUI.Button(new Rect(Screen.width - 160 + 70, Screen.height - 65, 20, 20), "-")) { SetGravity(-10); }
        GUI.Label(new Rect(Screen.width - 160 + 95, Screen.height - 65, 25, 20), (DataPool.gamePlay.gravity / 10f).ToString());
        if (GUI.Button(new Rect(Screen.width - 160 + 120, Screen.height - 65, 20, 20), "+")) { SetGravity(10); }

        GUI.Label(new Rect(Screen.width - 160 + 20, Screen.height - 40, 50, 30), "Air");
        if (GUI.Button(new Rect(Screen.width - 160 + 70, Screen.height - 40, 20, 20), "-")) { SetResistive(-10); }
        GUI.Label(new Rect(Screen.width - 160 + 95, Screen.height - 40, 25, 20), (DataPool.gamePlay.resistive / 10f).ToString());
        if (GUI.Button(new Rect(Screen.width - 160 + 120, Screen.height - 40, 20, 20), "+")) { SetResistive(10); }
    }
}
