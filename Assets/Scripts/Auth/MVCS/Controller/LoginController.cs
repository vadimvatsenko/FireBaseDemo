
using UnityEngine;

namespace Auth
{
    public class LoginController
    {
        
        private readonly FireBaseService fireBaseService;
        private readonly LoginPageView loginPageView;
        private readonly PageRoutingController pageRoutingController;

        public LoginController(FireBaseService fireBaseService, LoginPageView loginPageView, PageRoutingController pageRoutingController)
        {
            this.fireBaseService = fireBaseService;
            this.loginPageView = loginPageView;
            this.pageRoutingController = pageRoutingController;

            if(!PlayerPrefs.HasKey("RememberMeToogle"))
            {
                PlayerPrefs.SetInt("RememberMeToogle", 0);
            }
        }

        public void Init()
        {
            loginPageView.OnLoginBtnClickedEvent += OnLoginClick; // подписка на вызов сервиса
            fireBaseService.OnLoginSuccess += Service_OnLoginSuccess;
            loginPageView.OnRememberMeToggleClickedEvent += View_OnToogleClicked;

            if (PlayerPrefs.HasKey("RememberMeToogle"))
            {
                if (PlayerPrefs.GetInt("RememberMeToogle") == 1)
                {
                    fireBaseService.OnAutoLogin += Service_OnLoginSuccess;
                    
                }
            }
        }

        private void View_OnToogleClicked(bool value)
        {
            Debug.Log(value);
            PlayerPrefs.SetInt("RememberMeToogle", value ? 1 : 0);
        }

        ~LoginController()
        {
            loginPageView.OnLoginBtnClickedEvent -= OnLoginClick;
            fireBaseService.OnLoginSuccess -= Service_OnLoginSuccess;
        }

        private void Service_OnLoginSuccess()
        {
            pageRoutingController.ChangePage(CurrentPage.Profile);
        }

        private void OnLoginClick()
        {
            fireBaseService.LoginInSystem(loginPageView.Email, loginPageView.Password);
        }

    }
}
