using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Auth
{
    public class ForgotPassView : MonoBehaviour, IPageView
    {
        [SerializeField] private TextMeshProUGUI emailText;
        [SerializeField] private Button backBtn;
        [SerializeField] private Button sendMessageBtn;

        public event Action OnBackBtnClicked;
        public event Action OnSendMessageClicked;

        public string Email => emailText.text;
        public void Hide()
        {
            this.gameObject.SetActive(false);

            backBtn.onClick.RemoveAllListeners();
            sendMessageBtn.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            this.gameObject.SetActive(true);
            Init();
        }

        public void Init()
        {
            backBtn.onClick.AddListener(OnBackBtnClickedUnity);
            sendMessageBtn.onClick.AddListener(OnSendMessageBtnClickedUnity);
        }

        private void OnBackBtnClickedUnity()
        {
            OnBackBtnClicked?.Invoke();
        }

        private void OnSendMessageBtnClickedUnity()
        {
            OnSendMessageClicked?.Invoke();
        }


    }
}
