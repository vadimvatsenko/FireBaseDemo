
using System;
using System.Collections.Generic;

namespace Auth
{
    public class PageRoutingController
    {
        private readonly PageRoutingModel pageRoutingModel;

        private readonly LoginPageView loginPageView;
        private readonly RegistrationPageView registrationPageView;
        private readonly ProfilePageView profilePageView;

        private readonly Dictionary<CurrentPage, IPageView> pagesHash = new();
        public PageRoutingController(PageRoutingModel pageRoutingModel,
                                        LoginPageView loginPageView,
                                        RegistrationPageView registrationPageView,
                                        ProfilePageView profilePageView)
        {
            this.pageRoutingModel = pageRoutingModel;
            this.loginPageView = loginPageView;
            this.registrationPageView = registrationPageView;
            this.profilePageView = profilePageView;

            pagesHash.Add(CurrentPage.Login, loginPageView);
            pagesHash.Add(CurrentPage.Registration, registrationPageView);
            pagesHash.Add(CurrentPage.Profile, profilePageView);
        }

        ~PageRoutingController()
        {
            pageRoutingModel.CurrentPageValue.OnValueChanged -= OnPageChanged;
            loginPageView.OnRegisterBtnClickedEvent -= OnRegisterClicked;
            registrationPageView.OnBackButtonClicked -= OnBackClicked;
        }

        public void Init()
        {
            pageRoutingModel.CurrentPageValue.OnValueChanged += OnPageChanged;
            loginPageView.OnRegisterBtnClickedEvent += OnRegisterClicked;
            registrationPageView.OnBackButtonClicked += OnBackClicked;
        }

        private void OnBackClicked()
        {
            pageRoutingModel.CurrentPageValue.Value = CurrentPage.Login;
        }

        private void OnRegisterClicked()
        {
            pageRoutingModel.CurrentPageValue.Value = CurrentPage.Registration;
        }

        public void ChangePage(CurrentPage currentPage)
        {
            UnityEngine.Debug.Log("Change page invoked");
            pageRoutingModel.CurrentPageValue.Value = currentPage;
        }

        private void OnPageChanged(CurrentPage page1, CurrentPage page2)
        {
            UnityEngine.Debug.Log("Start Change Pages");
            IPageView pageView1, pageView2;
            bool isContainsKey1 = pagesHash.TryGetValue(page1, out pageView1);
            bool isContainsKey2 = pagesHash.TryGetValue(page2, out pageView2);

            if (isContainsKey1 && isContainsKey2)
            {
                pageView1.Hide();
                pageView2.Show();
                UnityEngine.Debug.Log($"OnPageChange page invoked from {page1} to {page2}");
            }
            else
            {
                UnityEngine.Debug.Log("Page isnt in dictionry");
            }
        }
    }
}