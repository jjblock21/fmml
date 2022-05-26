using System;

namespace Main
{
    public class Toggle
    {
        public Toggle(bool startState)
        {
            toggle = startState;
        }

        public Toggle()
        {
            toggle = false;
        }

        private bool toggle;

        public event EventHandler<bool> StateChanged;

        public bool SwitchUI(bool input)
        {
            if (input)
            {
                if (toggle) toggle = false;
                else toggle = true;
                RaiseStateChangedEvent();
            }
            return toggle;
        }

        public bool Switch()
        {
            if (toggle) toggle = false;
            else toggle = true;
            RaiseStateChangedEvent();
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
