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

public class Camera : MonoBehaviour
{
    public Transform target;
    public float x;
    public float y;
    public float xSpeed = .1f;
    public float ySpeed = .1f;
    public float distance;
    public float disSpeed = .1f;
    public float minDistance = .1f;
    public float maxDistance = .5f;

    private Quaternion rotationEuler;
    private Vector3 cameraPosition;

    private void Awake()
    {
        if (DataPool.gameCamera == null)
        {
            DontDestroyOnLoad(gameObject);
            DataPool.gameCamera = this;
        }
        else if (DataPool.gameCamera != this)
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
        y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

        if (x >360)
        {
            x -= 360;
        }
        else if (x < 0)
        {
            x += 360;
        }

        distance -= Input.GetAxis("Mouse ScrollWheel") * disSpeed * Time.deltaTime;

        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        rotationEuler = Quaternion.Euler(y, x, 0);
        cameraPosition = rotationEuler * new Vector3(0, 0, -distance) + target.position;

        transform.rotation = rotationEuler;
        transform.position = cameraPosition;
    }
}
