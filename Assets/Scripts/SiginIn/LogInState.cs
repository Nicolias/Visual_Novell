using TMPro;
using UnityEngine.UI;

namespace Auntification.StateMachine
{
    public class LogInState : AbstractState
    {
        public LogInState(Button auntificationButton, TMP_Text auntificationButtonText, TMP_Text changeStateText, AuntificationServise auntificationServise) 
            : base(auntificationButton, auntificationButtonText, changeStateText, auntificationServise)
        {
        }

        protected override void Auntificat()
        {
            AuntificationServise.SignIn();
        }

        protected override string GetAuntifiacationText()
        {
            return "Log in";
        }

        protected override string GetChangeStateText()
        {
            return "Register now";
        }
    }
}