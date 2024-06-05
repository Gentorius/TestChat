using Enums;

namespace Models
{
    public class UserInterfaceModel
    {
        private CurrentOpenWindow _currentCurrentOpenWindow = CurrentOpenWindow.WelcomeWindow;

        public void SetMainWindow(CurrentOpenWindow newCurrentOpenWindow)
        {
            if (newCurrentOpenWindow > CurrentOpenWindow.ProfileWindow ||
                newCurrentOpenWindow < CurrentOpenWindow.WelcomeWindow)
            {
                _currentCurrentOpenWindow = CurrentOpenWindow.Unknown;
                return;
            }
            
            _currentCurrentOpenWindow = newCurrentOpenWindow;
        }
        
    }
}