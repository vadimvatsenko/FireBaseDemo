namespace Auth
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

   
    public class LoginPageView : MonoBehaviour, IPageView
    {
        [SerializeField] private TextMeshProUGUI emailText;
        [SerializeField] private TextMeshProUGUI passwordText;

        [SerializeField] private Toggle rememberMeToggle;
        [SerializeField] private Button loginButton;
        [SerializeField] private Button registrationButton;
        [SerializeField] private Button forgotPassBtn;

        public string Email => emailText.text;
        public string Password => passwordText.text;

        public event Action OnLoginBtnClickedEvent;
        public event Action OnRegisterBtnClickedEvent;
        public event Action<bool> OnRememberMeToggleClickedEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnLoginBtnClickedEvent?.Invoke();
            }
        }

        public void Hide()
        {
            if(this.gameObject.activeSelf)
            gameObject.SetActive(false);
        }

        public void Show()
        {
            Init();
            gameObject.SetActive(true);
        }

        public void Init()
        {
            rememberMeToggle.isOn = PlayerPrefs.GetInt("RememberMeToogle") == 1 ? true : false; // добавлено

            registrationButton.onClick.AddListener(OnRegisterClickedUnity);
            loginButton.onClick.AddListener(OnLoginClickedUnity);
            rememberMeToggle.onValueChanged.AddListener(OnRememberMeClickedUnity);
        }

        private void OnDisable()
        {
            registrationButton.onClick.RemoveAllListeners();
            loginButton.onClick.RemoveAllListeners();
            rememberMeToggle.onValueChanged.RemoveAllListeners();
        }

        private void OnRememberMeClickedUnity(bool arg0)
        {
            OnRememberMeToggleClickedEvent?.Invoke(arg0);
        }

        private void OnRegisterClickedUnity()
        {
            OnRegisterBtnClickedEvent?.Invoke();
        }

        private void OnLoginClickedUnity()
        {
            OnLoginBtnClickedEvent?.Invoke();
        }
        
    }
}