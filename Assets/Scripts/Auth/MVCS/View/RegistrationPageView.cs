namespace Auth
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class RegistrationPageView : MonoBehaviour, IPageView
{
        [SerializeField] private TextMeshProUGUI emailText; // поле почты регистрации
        [SerializeField] private TextMeshProUGUI passwordText; // поле пароля
        [SerializeField] private TextMeshProUGUI confirmPasswordText;

        [SerializeField] private Button backButton; // кнопка обратно
        [SerializeField] private Button registrationButton; // кнопка регистрации
        public string Login => emailText.text; // свойство, возвращаем поле почты
        public string Password => passwordText.text; // свойство, возвращаем значение пароля
        public string ConfirmPassword => confirmPasswordText.text;

        public event Action OnBackButtonClicked; // событие кнопки обратно
        public event Action OnRegistrationButtonClicked; // событие кнопки регистрации 

        public void Hide()
        {
            gameObject.SetActive(false); // прячем
        }

        public void Show()
        {
            gameObject.SetActive(true); // активируем
            Init();
        }

        public void Init()
        {
            backButton.onClick.AddListener(OnBackButtonClickedUnity); // вешаем слушатель на метод OnBackButtonClickedUnity который вызывает событие
            registrationButton.onClick.AddListener(OnRegistrationButtonClickedUnity); // вешаем слушатель на метод OnRegistrationButtonClickedUnity который вызывает событие
        }

        private void OnDisable()
        {
            backButton.onClick.RemoveAllListeners();
            registrationButton.onClick.RemoveAllListeners();
            // или
            /*backButton.onClick.RemoveListener(OnBackButtonClickedUnity);
            registrationButton.onClick.RemoveListener(OnRegistrationButtonClickedUnity);*/
        }

        private void OnRegistrationButtonClickedUnity()
        {
            OnRegistrationButtonClicked?.Invoke();
        }

        private void OnBackButtonClickedUnity()
        {
            OnBackButtonClicked?.Invoke();
        }
    }
}