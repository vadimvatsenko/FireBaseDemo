
using System;


namespace Auth
{
    public class RegistrationController
    {

        //Service
        private readonly FireBaseService fireBaseService;

        //View
        private readonly RegistrationPageView registrationPageView;

        // Controller
        private readonly PageRoutingController pageRoutingController;


        public RegistrationController(FireBaseService fireBaseService, 
                                        RegistrationPageView registrationPageView,
                                        PageRoutingController pageRoutingController)
        {
            this.registrationPageView = registrationPageView;
            this.fireBaseService = fireBaseService;
            this.pageRoutingController = pageRoutingController;
        }

        public void Init()
        {
            registrationPageView.OnRegistrationButtonClicked += OnRegistrationClicked;
            this.fireBaseService.OnRegistSuccess += OnRegistSuccess;
        }

        ~RegistrationController()
        {
            registrationPageView.OnRegistrationButtonClicked -= OnRegistrationClicked;
            this.fireBaseService.OnRegistSuccess -= OnRegistSuccess;
        }

        private void OnRegistSuccess()
        {
            pageRoutingController.ChangePage(CurrentPage.Login);
        }

        private void OnRegistrationClicked()
        {
            fireBaseService.RegisterNewUser(registrationPageView.Login, registrationPageView.Password, registrationPageView.ConfirmPassword);
        }
    }
}