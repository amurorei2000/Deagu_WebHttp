using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.IO;

[System.Serializable]
public struct UserData
{
    public string name;
    public int age;
    public string job;
    public bool isMan;
    
    public UserData(string name, int age, string job, bool isMan)
    {
        this.name = name;
        this.age = age;
        this.job = job;
        this.isMan = isMan;
    }
}


public class JsonParser : MonoBehaviour
{
    public Text text_result;
    public UserData readUserData;

    void Start()
    {
        #region json 데이터를 만들고 저장하기
        //// 구조체 인스턴스를 만든다.
        //UserData user1 = new UserData("박원석", 44, "강사", true);
        ////user1.name = "박원석";
        ////user1.age = 44;
        ////user1.job = "강사";
        ////user1.isMan = true;
        //UserData user2 = new UserData("김영호", 27, "반장", true);

        //// 구조체 데이터를 json 형태로 변환한다.
        //string jsonUser1 = JsonUtility.ToJson(user1, true);
        //string jsonUser2 = JsonUtility.ToJson(user2, true);

        //print(jsonUser1);
        //print(jsonUser2);
        //text_result.text = jsonUser1 + "\n" + jsonUser2;

        //SaveJsonData(jsonUser1, Application.dataPath, "박원석.json");
        //SaveJsonData(jsonUser2, Application.dataPath, "반장.json");
        #endregion

        #region json 파일을 읽어서 구조체 변수로 변환하기
        //string readString = ReadJsonData(Application.dataPath, "반장.json");
        //print(readString);

        //if (readString != "")
        //{
        //    readUserData = JsonUtility.FromJson<UserData>(readString);
        //}
        #endregion

    }

    // text 데이터를 파일로 저장하기
    public void SaveJsonData(string json, string path, string fileName)
    {
        // 1. 파일 스트림을 쓰기 형태로 연다.
        //string fullPath = path + "/" + fileName;
        string fullPath = Path.Combine(path, fileName);
        FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);

        // 2. 스트림에 json 데이터를 쓰기로 전달한다.
        byte[] jsonBinary = Encoding.UTF8.GetBytes(json);
        fs.Write(jsonBinary);

        // 3. 스트림을 닫아준다.
        fs.Close();
    }

    // text 파일을 읽어오기
    public string ReadJsonData(string path, string fileName)
    {
        string readText;
        string fullPath = Path.Combine(path, fileName);

        // 예외 처리 : 해당 경로에 파일이 존재하는지를 먼저 확인한다.
        bool isDirectoryExist = Directory.Exists(path);

        if(isDirectoryExist)
        {
            bool isFileExist = File.Exists(fullPath);

            if (isFileExist)
            {
                // 1. 파일 스트림을 읽기 모드로 연다.
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

                // 2. 스트림으로부터 데이터(byte)를 읽어온다.
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                readText = sr.ReadToEnd();
            }
            else
            {
                readText = "그런 파일 없어욧!";
            }
        }
        else
        {
            readText = "그런 경로는 없어욧!";
        }

        // 3. 읽은 데이터를 string으로 변환해서 반환한다.
        return readText;
    }
}
