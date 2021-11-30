using TMPro;
using UnityEngine;


namespace MaksK_SpaceGB
{
    public sealed class PopUpMenu : MonoBehaviour
    {
        private TMP_InputField _inputField;

        public void Initialize()
        {
            _inputField = GetComponentInChildren<TMP_InputField>(true);
        }

        public string TypeName()
        {
            return _inputField.text;            
        }

        public void ShowMenu()
        {
            gameObject.SetActive(true);
            _inputField.ActivateInputField();
        }

        public void CloseMenu()
        {
            _inputField.text = "";
            gameObject.SetActive(false);
        }
    }
}