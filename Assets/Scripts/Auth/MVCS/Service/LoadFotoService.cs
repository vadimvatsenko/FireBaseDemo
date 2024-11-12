using UnityEngine;
using SFB;
using System;
using System.IO;
using UnityEngine.UI;

public class LoadFotoService 
{
    public Sprite OpenFile()
    {
        var extensions = new[] {
        new ExtensionFilter("Image Files", "png", "jpg", "jpeg" )
        };

        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", extensions, false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            Sprite sprite = LoadImage(paths[0]);

            return sprite;
        }
        return null;
    }

    private Sprite LoadImage(string path)
    {
        if (File.Exists(path)) // �������� ������������� �����
        {
            byte[] fileData = File.ReadAllBytes(path); // ������ ����� � ������ ����
            Texture2D texture = new Texture2D(300, 300); // ������� ��������
            if (texture.LoadImage(fileData)) // ��������� �������� �� ������
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                //displayImage.sprite = sprite; // ��������� ������ � Image
                //displayImage.SetNativeSize(); // ������������� ������������ ������ �����������
                Debug.Log("Image loaded successfully!");
                return sprite;
            }
            else
            {
                Debug.LogError("Failed to load image.");
                return null;
            }
        }
        else
        {
            Debug.LogError("File does not exist at path: " + path);
            return null;
        }
    }
}




