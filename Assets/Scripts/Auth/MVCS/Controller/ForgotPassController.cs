using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    public class ForgotPassController
    {
        private readonly FireBaseService fireBaseService;

        private readonly ForgotPassView forgotPassView;

        private readonly PageRoutingController pageRoutingController;

        public ForgotPassController(FireBaseService fireBaseService, ForgotPassView forgotPassView, PageRoutingController pageRoutingController)
        {
            this.fireBaseService = fireBaseService;
            this.pageRoutingController = pageRoutingController;
            this.forgotPassView = forgotPassView;
        }

        public void Init()
        {
            forgotPassView.OnSendMessageClicked += OnSendLetter;
        }

        private void OnSendLetter()
        {
            fireBaseService.SendForgotPassLetter(forgotPassView.Email);
        }
    }
}
