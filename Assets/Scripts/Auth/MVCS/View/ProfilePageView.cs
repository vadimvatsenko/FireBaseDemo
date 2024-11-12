using System;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Auth
{
    public class ProfilePageView : MonoBehaviour, IPageView
    {
        [Header("--Display Text--")]
        [SerializeField] private TextMeshProUGUI idText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI emailText;
        [SerializeField] private TextMeshProUGUI passwordText;

        [Header("--Input Name--")]
        [SerializeField] private GameObject inputNameWrap;
        [SerializeField] private TextMeshProUGUI inputNameField;

        [Header("--Input Email--")]
        [SerializeField] private GameObject inputEmailWrap;
        [SerializeField] private TextMeshProUGUI inputEmailField;

        [Header("--Display Name--")]
        [SerializeField] private GameObject displayNameWrap;
        [Header("--Display Email--")]
        [SerializeField] private GameObject displayPasswordWrap;

        [Header("--Avatar--")]
        [SerializeField] private Image avatarImg;

        [Header("--Buttons--")]
        [Header("--Main Buttons--")]
        [SerializeField] private Button quitButton;
        [SerializeField] private Button enterGameButton;
        [Header("--Edit User Name Buttons--")]
        [SerializeField] private Button editUserProfileNameButton;
        [SerializeField] private Button saveUserProfileNameButton;
        [Header("--Edit User Email Buttons--")]
        [SerializeField] private Button editUserProfileEmailButton;
        [SerializeField] private Button saveUserProfileEmailBtn;

        [Header("--Load Foto Button--")]
        [SerializeField] private Button loadFotoBtn;

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
            get { return inputNameField.text; }
            set { inputNameField.text = value; }
        }

        public string Email
        {
            get { return emailText.text; }
            set { emailText.text = value; }
        }

        public string EmailInput
        {
            get { return inputEmailField.text; }
            set { inputEmailField.text = value; }
        }
        public string Password
        {
            get { return passwordText.text; }
            set { passwordText.text = value; }
        }

        public Sprite AvatarImg
        {
            get { return avatarImg.sprite; }
            set { avatarImg.sprite = value;
                avatarImg.SetNativeSize();
            }
        }

        public event Action OnQuitClick;
        public event Action OnEnterGameClick;

        public event Action OnEditUserInfoBtn;
        public event Action<string, string> OnSaveUserInfoBtn;
        public event Action<string> OnEditUserEmailEvent;

        public event Action OnLoadFotoBtnClickedEvent;

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

            editUserProfileEmailButton.onClick.AddListener(ProfileEditEmailClick);
            saveUserProfileEmailBtn.onClick.AddListener(ProfileSaveEmailClick);

            loadFotoBtn.onClick.AddListener(OnLoadFotoBtnClickedUnity);
        }

        private void OnDisable()
        {
            quitButton.onClick.RemoveAllListeners();
            enterGameButton.onClick.RemoveAllListeners();
            editUserProfileNameButton.onClick.RemoveAllListeners();
            saveUserProfileNameButton.onClick.RemoveAllListeners();

            editUserProfileEmailButton.onClick.RemoveAllListeners();
            saveUserProfileEmailBtn.onClick.RemoveAllListeners();

            loadFotoBtn.onClick.RemoveAllListeners();

        }

        private void ProfileEditNameClick()
        {
            displayNameWrap.gameObject.SetActive(false);
            inputNameWrap.SetActive(true);
        }

        private void ProfileSaveNameClick()
        {
            inputNameWrap.SetActive(false);
            displayNameWrap.SetActive(true);

            OnSaveUserInfoBtn?.Invoke(NameInput, string.Empty);  
        }

        private void ProfileEditEmailClick()
        {
            displayPasswordWrap.gameObject.SetActive(false);
            inputEmailWrap.SetActive(true);
        }

        private void ProfileSaveEmailClick()
        {
            displayPasswordWrap.gameObject.SetActive(true);
            inputEmailWrap.SetActive(false);

            OnEditUserEmailEvent?.Invoke(EmailInput);
        }

        private void QuitClick()
        {
            OnQuitClick?.Invoke();
        }

        private void EnterGameClick()
        {
            OnEnterGameClick?.Invoke();
        }

        private void OnLoadFotoBtnClickedUnity()
        {
            OnLoadFotoBtnClickedEvent?.Invoke();
        }
    }
}
