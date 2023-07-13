using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiceButton : MonoBehaviour
{
    public GameObject ricePrefab;
    public Transform board;

    public void RiceBtn()
    {
        GameObject rice = Instantiate(ricePrefab, board.position, board.rotation, board);
    }
}
