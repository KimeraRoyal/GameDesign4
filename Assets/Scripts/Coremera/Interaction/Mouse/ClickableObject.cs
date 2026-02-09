using UnityEngine;
using UnityEngine.Events;

namespace Coremera.Interaction.Mouse
{
    public class ClickableObject : MonoBehaviour
    {
        public UnityEvent OnClicked;

        public void Click()
        {
            OnClicked?.Invoke();
        }
    }
}