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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(DataPool.jump))
        {
            DataPool.gameMgr.Shoot();
        }
        if (Input.GetKey(DataPool.forward))
        {
        }
        if (Input.GetKey(DataPool.backward))
        {
            DataPool.gameCamera.transform.up += -Vector3.forward / 2;
        }
        if (Input.GetKey(DataPool.left))
        {
            DataPool.gameCamera.transform.up += Vector3.left / 2;
        }
        if (Input.GetKey(DataPool.right))
        {
            DataPool.gameCamera.transform.position += Vector3.right / 2;
        }
        if (Input.GetKey(DataPool.leftturn))
        {
            DataPool.gameCamera.transform.Rotate(Vector3.down);
        }
        if (Input.GetKey(DataPool.rightturn))
        {
            DataPool.gameCamera.transform.Rotate(Vector3.up);
        }
    }
}
