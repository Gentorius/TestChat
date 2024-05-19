using Enums;

namespace Models
{
    public class UserInterfaceModel
    {
        private CurrentOpenWindow _currentCurrentOpenWindow = CurrentOpenWindow.GreetingWindow;
     
        public UserInterfaceModel()
        {
   
        }

        public void switchMainWindow(CurrentOpenWindow newCurrentOpenWindow)
        {
            _currentCurrentOpenWindow = newCurrentOpenWindow;
            
            if (newCurrentOpenWindow > CurrentOpenWindow.ProfileWindow || newCurrentOpenWindow < CurrentOpenWindow.GreetingWindow)
                _currentCurrentOpenWindow = CurrentOpenWindow.Unknown;
        }
        
    }
}