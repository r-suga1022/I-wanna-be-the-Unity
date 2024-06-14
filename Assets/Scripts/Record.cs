using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System.Text;

public class Record : MonoBehaviour
{
    bool Pulserecording = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
    }

    public void AddData(List<string> list, string data)
    {
        list.Add(data);
    }

    //csv書き出し
    public void LogSave(List<string> x, string fileName, bool AppendToFile)
    {
        FileInfo fi;
        StreamWriter sw;

        string filepath = Application.dataPath + "/" + fileName + ".csv";
        
        // fi = new FileInfo(Application.dataPath + "/" + fileName + ".csv");
        // sw = fi.AppendText();
        sw = new StreamWriter(filepath, AppendToFile);

        for (int i = 0; i < x.Count; ++i)
        {
            sw.Write(x[i].ToString()+"\n");
        }

        sw.Flush();
        sw.Close();

        x.Clear();
    }
}
