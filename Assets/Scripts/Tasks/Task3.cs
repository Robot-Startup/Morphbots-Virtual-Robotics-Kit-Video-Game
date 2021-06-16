using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Task3 : MonoBehaviour
{
    [SerializeField] TaskManager tasks;
    public Text taskText;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (tasks.task3) {
            taskText.color = Color.green;
        } else {
            taskText.color = Color.white;
        }
    }
}
