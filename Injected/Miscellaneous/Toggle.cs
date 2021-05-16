using System;

namespace Injected
{
    public class ToggleClass
    {
        private bool toggle = false;

        public event EventHandler<bool> StateChanged;

        public bool Toggle(bool input)
        {
            if (input)
            {
                if (toggle) toggle = false;
                else toggle = true;
                RaiseStateChangedEvent();
            }
            return toggle;
        }

        private void RaiseStateChangedEvent()
        {
            EventHandler<bool> handler = StateChanged;
            handler?.Invoke(this, toggle);
        }

        public void SetState(bool state)
        {
            toggle = state;
            RaiseStateChangedEvent();
        }
    }
}
