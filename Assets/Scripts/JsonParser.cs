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
        #region json �����͸� ����� �����ϱ�
        //// ����ü �ν��Ͻ��� �����.
        //UserData user1 = new UserData("�ڿ���", 44, "����", true);
        ////user1.name = "�ڿ���";
        ////user1.age = 44;
        ////user1.job = "����";
        ////user1.isMan = true;
        //UserData user2 = new UserData("�迵ȣ", 27, "����", true);

        //// ����ü �����͸� json ���·� ��ȯ�Ѵ�.
        //string jsonUser1 = JsonUtility.ToJson(user1, true);
        //string jsonUser2 = JsonUtility.ToJson(user2, true);

        //print(jsonUser1);
        //print(jsonUser2);
        //text_result.text = jsonUser1 + "\n" + jsonUser2;

        //SaveJsonData(jsonUser1, Application.dataPath, "�ڿ���.json");
        //SaveJsonData(jsonUser2, Application.dataPath, "����.json");
        #endregion

        #region json ������ �о ����ü ������ ��ȯ�ϱ�
        //string readString = ReadJsonData(Application.dataPath, "����.json");
        //print(readString);

        //if (readString != "")
        //{
        //    readUserData = JsonUtility.FromJson<UserData>(readString);
        //}
        #endregion

    }

    // text �����͸� ���Ϸ� �����ϱ�
    public void SaveJsonData(string json, string path, string fileName)
    {
        // 1. ���� ��Ʈ���� ���� ���·� ����.
        //string fullPath = path + "/" + fileName;
        string fullPath = Path.Combine(path, fileName);
        FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);

        // 2. ��Ʈ���� json �����͸� ����� �����Ѵ�.
        byte[] jsonBinary = Encoding.UTF8.GetBytes(json);
        fs.Write(jsonBinary);

        // 3. ��Ʈ���� �ݾ��ش�.
        fs.Close();
    }

    // text ������ �о����
    public string ReadJsonData(string path, string fileName)
    {
        string readText;
        string fullPath = Path.Combine(path, fileName);

        // ���� ó�� : �ش� ��ο� ������ �����ϴ����� ���� Ȯ���Ѵ�.
        bool isDirectoryExist = Directory.Exists(path);

        if(isDirectoryExist)
        {
            bool isFileExist = File.Exists(fullPath);

            if (isFileExist)
            {
                // 1. ���� ��Ʈ���� �б� ���� ����.
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);

                // 2. ��Ʈ�����κ��� ������(byte)�� �о�´�.
                StreamReader sr = new StreamReader(fs, Encoding.UTF8);
                readText = sr.ReadToEnd();
            }
            else
            {
                readText = "�׷� ���� �����!";
            }
        }
        else
        {
            readText = "�׷� ��δ� �����!";
        }

        // 3. ���� �����͸� string���� ��ȯ�ؼ� ��ȯ�Ѵ�.
        return readText;
    }
}
