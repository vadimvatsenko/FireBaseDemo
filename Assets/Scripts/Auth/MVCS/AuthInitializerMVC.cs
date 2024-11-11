using UnityEngine;

namespace Auth
{
    public class AuthInitializerMVC : MonoBehaviour
    {
        [SerializeField] private LoginPageView loginPageView;
        [SerializeField] private RegistrationPageView registrationPageView;
        [SerializeField] private ProfilePageView profilePageView;
        [SerializeField] private NotifyPageView notifyPageView;

        private PageRoutingModel _pageRoutingModel;
        private UserModel _userModel;

        private LoginController _loginController;
        private RegistrationController _registrationController;
        private ProfileController _profileController;
        private PageRoutingController _pageRoutingController;

        private FireBaseService _fireBaseService;
        private ValidationService _validationService;

        void Start()
        {
            Init();
        }

        private void Init()
        {
            notifyPageView.Init();
            _userModel = new UserModel();
            _validationService = new ValidationService(notifyPageView);
            _fireBaseService = new(notifyPageView, _validationService);

            loginPageView.Init();
            registrationPageView.Init();
            profilePageView.Init();
            

           
            _pageRoutingModel = new PageRoutingModel();

            _pageRoutingController = new(_pageRoutingModel, loginPageView, registrationPageView, profilePageView);
            _pageRoutingController.Init();
            
            _loginController = new(_fireBaseService, loginPageView, _pageRoutingController);
            _loginController.Init();

            _profileController = new ProfileController(_fireBaseService, profilePageView, _pageRoutingController, _userModel);
            _profileController.Init();

            

            _registrationController = new(registrationPageView, _fireBaseService, _pageRoutingController);
            _registrationController.Init();

            _fireBaseService.Init(); // после всего, чтобы успеть подписатьс€ на событи€
        }

        /*private void Init()
        {
            var container = ContainerFactory.Create();

            container.RegisterImplementation(typeof(PageRoutingModel), typeof(PageRoutingModel), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(UserModel), typeof(UserModel), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(LoginController), typeof(LoginController), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(RegistrationController), typeof(RegistrationController), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(PageRoutingController), typeof(PageRoutingController), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(FireBaseService), typeof(FireBaseService), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(ValidationService), typeof(ValidationService), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(LoginPageView), typeof(LoginPageView), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(RegistrationPageView), typeof(RegistrationPageView), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(ProfilePageView), typeof(ProfilePageView), Lifetime.PerContainer);
            container.RegisterImplementation(typeof(NotifyPageView), typeof(NotifyPageView), Lifetime.PerContainer);

        }*/
    }
}
