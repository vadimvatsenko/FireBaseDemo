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
        if (File.Exists(path)) // Проверка существования файла
        {
            byte[] fileData = File.ReadAllBytes(path); // Чтение файла в массив байт
            Texture2D texture = new Texture2D(300, 300); // Создаем текстуру
            if (texture.LoadImage(fileData)) // Загружаем текстуру из байтов
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                //displayImage.sprite = sprite; // Применяем спрайт к Image
                //displayImage.SetNativeSize(); // Устанавливаем оригинальный размер изображения
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




