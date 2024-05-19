using Enums;
using UnityEngine;

namespace Windows.Models
{
    public class UserInterfaceModel
    {
        private FocusWindow _currentFocusWindow = FocusWindow.GreetingWindow;
     
        public UserInterfaceModel()
        {
   
        }

        public void switchMainWindow(FocusWindow newFocusWindow)
        {
            _currentFocusWindow = newFocusWindow;
            
            if (newFocusWindow > FocusWindow.ProfileWindow || newFocusWindow < FocusWindow.GreetingWindow)
                _currentFocusWindow = FocusWindow.Unknown;
        }
        
    }
}