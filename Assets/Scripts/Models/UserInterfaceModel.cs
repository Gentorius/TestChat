using Enums;

namespace Models
{
    public class UserInterfaceModel
    {
        public Window CurrentWindow = Window.WelcomeWindow;

        public void SetMainWindow(Window newWindow)
        {
            if (newWindow != Window.WelcomeWindow)
            {
                CurrentWindow = Window.Unknown;
                return;
            }
            
            CurrentWindow = newWindow;
        }
        
    }
}