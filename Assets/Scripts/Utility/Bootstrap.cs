using UnityEngine;

namespace Utility
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _projectContext; //Used for behind the scenes processes
        [SerializeField] 
        private GameObject _userInterface; //Used for user input

        private void Awake()
        {
            _userInterface = Instantiate(_userInterface, new Vector3(0, 0, 0), Quaternion.identity);
            
            _projectContext = Instantiate(_projectContext, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        

        
    }
}