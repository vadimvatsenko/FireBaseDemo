using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Auth
{
    public class ProfileController
    {
        private readonly FireBaseService fireBaseService;

        private readonly ProfilePageView profilePageView;

        private readonly PageRoutingController pageRoutingController;

        private readonly LoadFotoService loadFotoService;

        public ProfileController(FireBaseService fireBaseService, ProfilePageView profilePageView, PageRoutingController pageRoutingController, UserModel userModel, LoadFotoService loadFotoService)
        {
            this.fireBaseService = fireBaseService;
            this.profilePageView = profilePageView;
            this.pageRoutingController = pageRoutingController;
            this.loadFotoService = loadFotoService;
        }

        public void Init()
        {
            fireBaseService.OnAuthStateChanged += UserInfoChange;
            profilePageView.OnQuitClick += QuitFromProfile;
            profilePageView.OnSaveUserInfoBtn += EditUserInfo;
            profilePageView.OnEditUserEmailEvent += UpdateUserEmailController;

            profilePageView.OnLoadFotoBtnClickedEvent += OnLoadFoto;
        }

        ~ProfileController()
        {
            fireBaseService.OnAuthStateChanged -= UserInfoChange;
            profilePageView.OnQuitClick -= QuitFromProfile;
            profilePageView.OnSaveUserInfoBtn -= EditUserInfo;
            profilePageView.OnEditUserEmailEvent -= UpdateUserEmailController;

            profilePageView.OnLoadFotoBtnClickedEvent -= OnLoadFoto;
        }

        private async void UserInfoChange(FirebaseUser user)
        {
            if (user != null)
            {
                profilePageView.ID = user.UserId;
                profilePageView.Name = user.DisplayName != string.Empty ? user.DisplayName : "Underfined";
                profilePageView.Email = user.Email;
                profilePageView.NameInput = profilePageView.Name;

                await LoadImage();

            }
        }

        private void EditUserInfo(string name, string avatarUri)
        {
            fireBaseService.UpdateUserFrofile(name, avatarUri);
        }

        private void UpdateUserEmailController(string email)
        {
            fireBaseService.UpdateUserEmail(email);
        }

        private void QuitFromProfile()
        {
            profilePageView.ID = " ";
            profilePageView.Name = " ";
            profilePageView.Email = " ";
            profilePageView.NameInput = " ";

            fireBaseService.QuitFromProfile();

            pageRoutingController.ChangePage(CurrentPage.Login);
        }

        public async Task LoadImage()
        {
            if(fireBaseService._user.PhotoUrl != null)
            {
                using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(fireBaseService._user.PhotoUrl))
                {
                    var operation = request.SendWebRequest();

                    // Ждем завершения загрузки
                    while (!operation.isDone)
                    {
                        await Task.Yield();
                    }

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        // Загружаем текстуру из запроса
                        Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;

                        // Преобразуем Texture2D в Sprite
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                        // Устанавливаем Sprite в UI Image
                        profilePageView.AvatarImg = sprite;
                        //profilePageView.AvatarImg.gameobject.SetNativeSize(); // Устанавливаем оригинальный размер изображения
                        Debug.Log("Image loaded and displayed successfully!");
                    }
                    else
                    {
                        Debug.LogError($"Failed to load image from URL: {request.error}");
                        Debug.LogError("Response Code: " + request.responseCode); // Показывает код ошибки
                        Debug.LogError("URL: " + fireBaseService._user.PhotoUrl); // Показывает URL, который вызывает проблему
                    }
                }
            }
            

        }

        private void OnLoadFoto()
        {
            Sprite newSprite = loadFotoService.OpenFile();
            if(newSprite != null)
            {
                profilePageView.AvatarImg = newSprite;
            }

        }
    }
}

