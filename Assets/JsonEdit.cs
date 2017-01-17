using UnityEngine;
using System.Collections;
using System.IO;

[ExecuteInEditMode]
public class JsonEdit : MonoBehaviour {
    public int FileNameIdx = 0;

    public QuestionStruct curQuestionStruct;

    private string filename;
    // Use this for initialization
    void Start () {
        var textString = Resources.Load<TextAsset>("json/question_" + FileNameIdx);
        curQuestionStruct = JsonUtility.FromJson<QuestionStruct>(textString.text);
    }

    [ContextMenu("ReFreshen")]
    public void ReFreshen()
    {
        var textString = Resources.Load<TextAsset>("json/question_" + FileNameIdx);
        curQuestionStruct = JsonUtility.FromJson<QuestionStruct>(textString.text);
    }

    [ContextMenu("SaveToFile")]
    public void SaveToFile()
    {
        filename = Application.dataPath + "/Resources/json/question_" + curQuestionStruct.fileID + ".txt";
        StreamWriter sw;
        FileInfo t = new FileInfo(filename);
        if (!t.Exists)
        {
            sw = t.CreateText();
        }
        else
        {
            File.Delete(filename);
            sw = t.CreateText();
        }
        sw.Write(JsonUtility.ToJson(curQuestionStruct)); 
        sw.Close();        
        sw.Dispose();
    }
	// Update is called once per frame
	void Update () {
	
	}
}
