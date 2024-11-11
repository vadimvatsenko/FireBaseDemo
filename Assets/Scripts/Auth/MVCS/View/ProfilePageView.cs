using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Auth
{
    public class ProfilePageView : MonoBehaviour, IPageView
    {
        [SerializeField] private TextMeshProUGUI idText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI emailText;
        [SerializeField] private TextMeshProUGUI passwordText;

        [SerializeField] private GameObject inputNameField;
        
        [SerializeField] private GameObject displayName;

        [SerializeField] private Button quitButton;
        [SerializeField] private Button enterGameButton;

        [SerializeField] private Button editUserProfileNameButton;
        [SerializeField] private Button saveUserProfileNameButton;

        public string ID
        {
            get { return idText.text; }
            set { idText.text = value; }
        }
        public string Name
        {
            get { return nameText.text; }
            set { nameText.text = value; }
        }

        public string NameInput
        {
            get { return inputNameField.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text; }
            set { inputNameField.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = value; }
        }

        public string Email
        {
            get { return emailText.text; }
            set { emailText.text = value; }
        }
        public string Password
        {
            get { return passwordText.text; }
            set { passwordText.text = value; }
        }

        public event Action OnQuitClick;
        public event Action OnEnterGameClick;

        public event Action OnEditUserInfoBtn;
        public event Action<string, string> OnSaveUserInfoBtn;

        public void Hide()
        {
           gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Init();
        }

        public void Init()
        {
            quitButton.onClick.AddListener(QuitClick);
            enterGameButton.onClick.AddListener(EnterGameClick);
            editUserProfileNameButton.onClick.AddListener(ProfileEditNameClick);
            saveUserProfileNameButton.onClick.AddListener(ProfileSaveNameClick);
        }

        private void OnDisable()
        {
            quitButton.onClick.RemoveAllListeners();
            enterGameButton.onClick.RemoveAllListeners();
            editUserProfileNameButton.onClick.RemoveAllListeners();
            saveUserProfileNameButton.onClick.RemoveAllListeners();

        }

        private void ProfileEditNameClick()
        {
            displayName.gameObject.SetActive(false);
            inputNameField.SetActive(true);
        }

        private void ProfileSaveNameClick()
        {
            inputNameField.SetActive(false);
            displayName.SetActive(true);

            OnSaveUserInfoBtn?.Invoke(NameInput, string.Empty);  
        }

        private void QuitClick()
        {
            OnQuitClick?.Invoke();
        }

        private void EnterGameClick()
        {
            OnEnterGameClick?.Invoke();
        }

        private void ProfileEditClick()
        {
            //OnEditUserProfileFotoButton?.Invoke();
        }

    }
}
