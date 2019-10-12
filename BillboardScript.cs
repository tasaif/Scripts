using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BillboardScript : MonoBehaviour
{
    List<GameObject> boards;
    float last_switch;
    int current_board = 0;
    public float advance_time = 3;
    public string next_scene = "";

    // Start is called before the first frame update
    void Start()
    {
        boards = new List<GameObject>();
        for(var i=0; i<transform.childCount; i++)
        {
            var child = transform.GetChild(i).gameObject;
            boards.Add(child);
            child.SetActive(false);
        }
        boards[0].SetActive(true);
        last_switch = Time.fixedTime;
    }

    void AdvanceBoard()
    {
        boards[current_board].SetActive(false);
        current_board++;
        if (current_board >= boards.Count)
        {
            if (next_scene != "")
            {
                SceneManager.LoadScene(next_scene);
            }
            current_board = 0;
        }
        boards[current_board].SetActive(true);
        last_switch = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > last_switch + advance_time)
        {
            AdvanceBoard();
        }
    }
}
