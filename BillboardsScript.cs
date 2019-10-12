using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BillboardsScript : MonoBehaviour
{
    List<GameObject> boards;
    float last_switch;
    int current_board_index = 0;
    public float advance_time = 3;
    public string next_scene = "";
    public bool loop = false;
    BillboardScript current_billboard_script;

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
        current_billboard_script = boards[0].GetComponent<BillboardScript>();
    }

    void AdvanceBoard()
    {
        boards[current_board_index].SetActive(false);
        current_board_index++;
        if (current_board_index >= boards.Count)
        {
            if (loop)
            {
                current_board_index = 0;
            }
            else
            {
                if (next_scene != "")
                {
                    SceneManager.LoadScene(next_scene);
                }
            }
        }
        current_billboard_script = boards[current_board_index].GetComponent<BillboardScript>();
        boards[current_board_index].SetActive(true);
        last_switch = Time.fixedTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_billboard_script != null)
        {
            if (!current_billboard_script.time_based)
            {
                return;
            } else if (Time.fixedTime > last_switch + current_billboard_script.advance_time)
            {
                AdvanceBoard();
            }
        } else if (Time.fixedTime > last_switch + advance_time)
        {
            AdvanceBoard();
        }
    }
}
