using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task1 : MonoBehaviour
{
    [SerializeField] TaskManager tasks;
    public Text taskText;

    // Update is called once per frame
    void Update()
    {
        if (tasks.task1) {
            taskText.color = Color.green;
        } else {
            taskText.color = Color.white;
        }
    }
}
