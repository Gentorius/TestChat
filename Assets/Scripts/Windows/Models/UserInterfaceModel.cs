using Enums;
using UnityEngine;

namespace Windows.Models
{
    public class UserInterfaceModel
    {
        private MainWindow _currentMainWindow = MainWindow.HelloWindow;
     
        public UserInterfaceModel()
        {
   
        }

        public void switchMainWindow(MainWindow newMainWindow)
        {
            _currentMainWindow = newMainWindow;
            
            if (newMainWindow > MainWindow.ProfileWindow || newMainWindow < MainWindow.HelloWindow)
                _currentMainWindow = MainWindow.Unknown;
        }
        
    }
}