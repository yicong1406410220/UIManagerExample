using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PanelManager.Init();
        PanelManager.Open<TipPanel>("这是个顶层窗口");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
