namespace Auth
{
    using Firebase;
    using Firebase.Auth;
    using System;
    using System.Net.Mail;
    using System.Threading.Tasks;
    using UnityEditor.VersionControl;
    using UnityEngine;

    public class FireBaseService
    {

        public FirebaseAuth _auth { get; private set; }
        public FirebaseUser _user { get; private set; }
        private NotifyPageView _notifyPageView;
        private ValidationService _validationService;
        private Color SuccessColor => Color.green;
        private Color FailureColor => Color.red;

        public event Action OnLoginSuccess;
        public event Action OnRegistSuccess;
        public event Action<FirebaseUser> OnAuthStateChanged;
        public event Action OnAutoLogin;


        public FireBaseService(NotifyPageView notifyPageView, ValidationService validationService)
        {
            _auth = FirebaseAuth.DefaultInstance;
            _notifyPageView = notifyPageView;
            _validationService = validationService;
        }

        ~FireBaseService()
        {
            _auth.StateChanged -= AuthStateChanged;
            _auth = null;
        }

        public void Init()
        {
            if (PlayerPrefs.GetInt("RememberMeToogle") == 1) // добавлено
            {
                _auth.StateChanged += AuthStateChanged;
                //_user = _auth.CurrentUser;
                AuthStateChanged(this, null);
            }
            else
            {
                QuitFromProfile(); // добавлено
            }
        }

        public async void RegisterNewUser(string email, string password, string confirmPassword)
        {

            if (_validationService.EmailValidation(email) && _validationService.PassWordValidation(password) && _validationService.PasswordEquals(password, confirmPassword))
            {
                Task<AuthResult> task = _auth.CreateUserWithEmailAndPasswordAsync(email, password);

                await task;

                try
                {
                    if (task.IsCanceled)
                    {
                        _notifyPageView.ShowMessage("Error", "CreateUserWithEmailAndPasswordAsync was canceled.", FailureColor);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        _notifyPageView.ShowMessage("Error", "CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception, FailureColor);
                        return;
                    }

                    Firebase.Auth.AuthResult result = task.Result;

                    _notifyPageView.ShowMessage("Success", result.User.DisplayName + " " + result.User.UserId + " " + "regist success", SuccessColor);
                    OnRegistSuccess?.Invoke();
                    EmailVerification();
                }
                catch (FirebaseException ex)
                {
                    _notifyPageView.ShowMessage("Error", ex.Message, FailureColor);
                }
            }
            else
            {
                return;
            }
        }

        public async void LoginInSystem(string email, string password)
        {
            try
            {
                Task<AuthResult> task = _auth.SignInWithEmailAndPasswordAsync(email, password);
                await task;
                if (task.IsCanceled)
                {
                    _notifyPageView.ShowMessage("Error", "SignInWithEmailAndPasswordAsync was canceled.", FailureColor);
                    return;
                }

                if (task.IsFaulted)
                {
                    _notifyPageView.ShowMessage("Error", "SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception, FailureColor);
                    return;
                }

                AuthResult result = task.Result;

                _notifyPageView.ShowMessage("Success", result.User.DisplayName + " " + result.User.UserId + " " + "login success", SuccessColor);
                OnLoginSuccess?.Invoke();
                OnAuthStateChanged?.Invoke(result.User);
            }
            catch (FirebaseException ex)
            {
                _notifyPageView.ShowMessage("Error", ex.Message, FailureColor);
            }
        }

        public void AuthStateChanged(object sender, EventArgs eventArgs)
        {
            if (_auth.CurrentUser != _user)
            {
                bool signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null
                    && _auth.CurrentUser.IsValid();
                if (!signedIn && _user != null)
                {
                    Debug.Log("Signed out " + _user.UserId);
                    _notifyPageView.ShowMessage("Success", _user.UserId + "Quit Success", SuccessColor);
                }

                _user = _auth.CurrentUser;
                if (signedIn)
                {
                    _notifyPageView.ShowMessage("Success", _user.UserId + "Enter Success", Color.green);
                    
                    OnLoginSuccess?.Invoke();
                    OnAuthStateChanged?.Invoke(_user);
                }
            }
        }

        public async void UpdateUserFrofile(string userName, string uri = " ")
        {
            try
            {
                UserProfile userProfile = new UserProfile()
                {
                    DisplayName = userName != string.Empty ? userName : _user.DisplayName,
                    PhotoUrl = new Uri("https://cdn.icon-icons.com/icons2/1371/PNG/512/batman_90804.png")
                };

                var task = _user.UpdateUserProfileAsync(userProfile);
                await task;

                if (task.IsCanceled)
                {
                    Debug.LogError("UpdateUserProfileAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("UpdateUserProfileAsync encountered an error: " + task.Exception);
                    return;
                }

                _notifyPageView.ShowMessage("Success", "User profile updated successfully.", SuccessColor);
                OnAuthStateChanged?.Invoke(_user);


            }
            catch (FirebaseException ex)
            {
                _notifyPageView.ShowMessage("Error", ex.Message, FailureColor);
            }
        }

        private async void EmailVerification()
        {
            Debug.Log(_user.Email);
            try
            {
                var task = _user.SendEmailVerificationAsync();
                await task;
                if (task.IsCanceled)
                {
                    _notifyPageView.ShowMessage("Error", "SendEmailVerificationAsync was canceled.", FailureColor);
                    return;
                }
                if (task.IsFaulted)
                {
                    _notifyPageView.ShowMessage("Error", "SendEmailVerificationAsync encountered an error: " + task.Exception, FailureColor);
                    return;
                }
                _notifyPageView.ShowMessage("Success", "Email sent successfully." + task.Exception, SuccessColor);
            }
            catch (FirebaseException ex)
            {
                _notifyPageView.ShowMessage("Error", ex.Message, FailureColor);
            }
        
        }

        public async void SendForgotPassLetter(string emailAddress)
        {
            emailAddress = "vadim_vatsenko@ukr.net";
            if (_validationService.EmailValidation(emailAddress))
            {
                try
                {
                    var task = _auth.SendPasswordResetEmailAsync(emailAddress);
                    await task;

                    if (task.IsCanceled)
                    {
                        _notifyPageView.ShowMessage("Error", "SendPasswordResetEmailAsync was canceled.", FailureColor);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        _notifyPageView.ShowMessage("Error", "SendPasswordResetEmailAsync encountered an error: " + task.Exception, FailureColor);
                        return;
                    }
                    _notifyPageView.ShowMessage("Success", "Password reset email sent successfully.", SuccessColor);
                }
                catch (FirebaseException ex)
                {
                    _notifyPageView.ShowMessage("Error", ex.Message, FailureColor);
                }
            }
            
        }

        public async void UpdateUserEmail(string emailAddress)
        {
            if (_validationService.EmailValidation(emailAddress))
            {
                try
                {
                    var task = _user.UpdateEmailAsync(emailAddress);
                    await task;
                    if (task.IsCanceled)
                    {
                        _notifyPageView.ShowMessage("Error", "UpdateEmailAsync was canceled.", FailureColor);
                        return;
                    }
                    if (task.IsFaulted)
                    {
                        _notifyPageView.ShowMessage("Error", "UpdateEmailAsync encountered an error: " + task.Exception, FailureColor);
                        return;
                    }

                    _notifyPageView.ShowMessage("Success", "User email updated successfully.", SuccessColor);


                }
                catch (FirebaseException ex)
                {
                    _notifyPageView.ShowMessage("Error", ex.Message, FailureColor);
                }
            }
            
        }

        public void QuitFromProfile()
        {
            _auth.SignOut();
        }

    }
}