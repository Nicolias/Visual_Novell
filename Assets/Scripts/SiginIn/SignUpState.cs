using TMPro;
using UnityEngine.UI;

namespace Auntification.StateMachine
{
    public class SignUpState : AbstractState
    {
        public SignUpState(Button auntificationButton, TMP_Text auntificationButtonText, TMP_Text changeStateText, AuntificationServise auntificationServise) : base(auntificationButton, auntificationButtonText, changeStateText, auntificationServise)
        {
        }

        protected override void Auntificat()
        {
            AuntificationServise.SignUp();
        }

        protected override string GetAuntifiacationText()
        {
            return "Register";
        }

        protected override string GetChangeStateText()
        {
            return "Log in now";
        }
    }
}