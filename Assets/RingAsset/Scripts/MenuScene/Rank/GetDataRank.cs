using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDataRank : MonoBehaviour
{
    public List<Text> _listTime;

    public List<Text> _listKill;

    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log(GameManager.Instance._dataGame.timePlay.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
