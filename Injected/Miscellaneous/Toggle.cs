namespace Injected
{
    public class ToggleClass
    {
        private bool toggle = false;
        public bool Toggle(bool input)
        {
            if (input)
                if (toggle) toggle = false;
                else toggle = true;
            return toggle;
        }

        public void SetState(bool state) => toggle = state;
    }
}
