using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetDataRank : MonoBehaviour
{
    public List<Text> _listTime;

    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < GameManager.Instance._dataGame.timePlay.Count; i++)
        {
            if (i < 4)
            {
                _listTime[i].text = GameManager.Instance._dataGame.timePlay[i];
            }
        }

        Debug.Log(GameManager.Instance._dataGame.timePlay.Count);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
