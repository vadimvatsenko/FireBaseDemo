using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Auth
{
    public class ProfileController
    {
        private readonly FireBaseService fireBaseService; 

        private readonly ProfilePageView profilePageView; 

        private readonly PageRoutingController pageRoutingController; 

        private readonly UserModel userModel;

        public ProfileController(FireBaseService fireBaseService, ProfilePageView profilePageView, PageRoutingController pageRoutingController, UserModel userModel)
        {
            this.fireBaseService = fireBaseService;
            this.profilePageView = profilePageView;
            this.pageRoutingController = pageRoutingController;
            this.userModel = userModel;            
        }

        public void Init()
        {
            fireBaseService.OnAuthStateChanged += UserInfoChange;
            profilePageView.OnQuitClick += QuitFromProfile;
            profilePageView.OnSaveUserInfoBtn += EditUserInfo;
        }

        ~ProfileController()
        {
            fireBaseService.OnAuthStateChanged -= UserInfoChange;
            profilePageView.OnQuitClick -= QuitFromProfile;
            profilePageView.OnSaveUserInfoBtn -= EditUserInfo;
        }

        private void UserInfoChange(FirebaseUser user)
        {
            if(user != null)

                profilePageView.ID = user.UserId;
                profilePageView.Name = user.DisplayName != string.Empty ? user.DisplayName : "Underfined";
                profilePageView.Email = user.Email;
            profilePageView.NameInput = profilePageView.Name;
        }

        private void EditUserInfo(string name, string avatarUri)
        {
            fireBaseService.UpdateUserFrofile(name, avatarUri);
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
       
    }
}

