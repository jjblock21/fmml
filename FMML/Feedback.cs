using System;

namespace Fireworks_Mania_Modloader
{
    public class Feedback
    {
        private ushort errorId;
        private Exception exception;
        private string errorDescription;
        private bool wasSuccessful;
        private int returned;

        public Feedback(ushort errorId, Exception exception, bool wasSuccessful, int returned, string errorDescription)
        {
            this.errorId = errorId;
            this.exception = exception;
            this.wasSuccessful = wasSuccessful;
            this.returned = returned;
            this.errorDescription = errorDescription;
        }

        public ushort ErrorID { get => errorId; }
        public Exception Exception { get => exception; }
        public bool WasSuccessful { get => wasSuccessful; }
        public int Returned { get => returned; }
        public string ErrorDescription { get => errorDescription; }

        public static Feedback GenerateErrorFeedback(ushort errorId, string errorDescription, Exception exception = null)
            => new Feedback(errorId, exception, false, -1, errorDescription);

        public static Feedback GenerateSuccessFeedback(int returned)
            => new Feedback(0, null, true, returned, null);

    }
}
