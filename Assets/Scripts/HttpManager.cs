using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;   // http ����� ���� ���� �����̽�
using System.Text;      // json, csv���� ���� ������ ���ڵ�(UTF-8)�� ���� ���� �����̽�
using UnityEngine.UI;
using System;

public class HttpManager : MonoBehaviour
{
    public string url;
    public Text text_response;
    public RawImage img_response;
    public Button btn_get;
    public Button btn_getImage;
    public Button btn_getJson;
    //public RequestImage reqImageData;

    public void Get()
    {
        btn_get.interactable = false;
        StartCoroutine(GetRequest(url));
    }

    // Get ��� �ڷ�ƾ �Լ�
    IEnumerator GetRequest(string url)
    {
        // http Get ��� �غ� �Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // ������ Get ��û�� �ϰ�, �����κ��� ������ �� ������ ����Ѵ�.
        yield return request.SendWebRequest();

        // ����, �����κ��� �� ������ ����(200)�̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // ������� �����͸� ����Ѵ�.
            string response = request.downloadHandler.text;

            print(response);
            text_response.text = response;
        }
        // �׷��� �ʴٸ�(400, 404 etc)...
        else
        {
            // ���� ������ ����Ѵ�.
            print(request.error);
            text_response.text = request.error;
        }

        btn_get.interactable = true;
    }

    public void GetImage()
    {
        btn_getImage.interactable = false;
        StartCoroutine(GetImageRequest(url));
    }

    // �̹��� ������ Get���� �޴� �Լ�
    IEnumerator GetImageRequest(string url)
    {
        // get(Texture) ����� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        // ������ ��û�� �ϰ�, ������ ���� ������ ��ٸ���.
        yield return request.SendWebRequest();

        // ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // ���� �ؽ��� �����͸� Texture2D ������ �޾Ƴ��´�.
            Texture2D response = DownloadHandlerTexture.GetContent(request);

            // Texture2D �����͸� img_response�� texture ������ �־��.
            img_response.texture = response;

            // text_response�� ���� �ڵ� ��ȣ�� ����Ѵ�.
            text_response.text = "���� - " + request.responseCode.ToString();
        }
        // �׷��� �ʴٸ�...
        else
        {
            // ���� ������ text_response�� ����Ѵ�.
            print(request.error);
            text_response.text = request.error;
        }

        btn_getImage.interactable = true;
    }

    public void GetJson()
    {
        btn_getJson.interactable = false;
        StartCoroutine(GetJsonImageRequest(url));
    }

    IEnumerator GetJsonImageRequest(string url)
    {
        // url�κ��� Get���� ��û�� �غ��Ѵ�.
        UnityWebRequest request = UnityWebRequest.Get(url);

        // �غ�� ��û�� ������ �����ϰ� ������ �ö����� ��ٸ���.
        yield return request.SendWebRequest();

        // ����, ������ �����̶��...
        if (request.result == UnityWebRequest.Result.Success)
        {
            // �ؽ�Ʈ�� �޴´�.
            string result = request.downloadHandler.text;
            // ���� ���� json �����͸� RequestImage ����ü ���·� �Ľ��Ѵ�.
            RequestImage reqImageData = JsonUtility.FromJson<RequestImage>(result);

            //byte[] binaries = Encoding.UTF8.GetBytes(reqImageData.img);
            byte[] binaries = Convert.FromBase64String(reqImageData.img);
            print(binaries.Length);
            if (binaries.Length > 0)
            {
                Texture2D texture = new Texture2D(184, 273);

                // byte �迭�� �� raw �����͸� �ؽ��� ���·� ��ȯ�ؼ� texture2D �ν��Ͻ��� ��ȯ�Ѵ�.
                texture.LoadImage(binaries);
                img_response.texture = texture;

            }

        }
        // �׷��� �ʴٸ�...
        else
        {
            // ���� ������ text_response�� �����Ѵ�.
            text_response.text = request.responseCode + ": " + request.error;
            Debug.LogError(request.responseCode + ": " + request.error);
        }

        btn_getJson.interactable = true;
    }
}


[System.Serializable]
public struct RequestImage
{
    public string img;
}