using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TextFlickerScript : MonoBehaviour
{
    public GameObject textobject;
    public int FlickerPeriod_frame = 240; // 点滅周期（フレーム数）
    private int current_frame; // 点滅が始まってからのフレーム数

    public bool IsDelete; // 一定時間たったら消すか否か
    public int flickernum_del; // 何点滅したら消すか
    int current_flickernum; // 何点滅目か
    
    // Start is called before the first frame update
    void Start()
    {
        current_frame = 0;
        current_flickernum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Text text = textobject.GetComponent<Text>();
        int alpharate = FlickerPeriod_frame/2;
        
        if(current_frame >= FlickerPeriod_frame/2) {alpharate = -alpharate;}
        if(current_frame >= FlickerPeriod_frame) {current_frame = 0; ++current_flickernum;}
        if(IsDelete && current_flickernum >= flickernum_del) {current_flickernum = 0; this.gameObject.SetActive(false);}

        var TextColor = text.color - new Color(0.0f, 0.0f, 0.0f, (1.0f/(float)alpharate));
        
        text.color = TextColor;

        ++current_frame;
    }
}
