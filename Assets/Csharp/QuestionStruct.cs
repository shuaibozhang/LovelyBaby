using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class QuestionStruct{
    public int fileID;
    public SingleQuestion[] context;
}

[Serializable]
public class SingleQuestion
{
    public int uniqueID;
    public string ask;
    public string a;
    public string b;
    public string c;
    public string d;
    public int trueAnsweridx;
    public string answer;
    public string from;  
}
