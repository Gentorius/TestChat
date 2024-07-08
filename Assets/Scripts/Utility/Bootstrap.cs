using UnityEngine;

namespace Utility
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _projectContext; //Used for behind the scenes processes
        [SerializeField] 
        private GameObject _userInterface; //Used for user input
        [SerializeField]
        private GameObject _dataStream; //Used for data simulation of data stream between server and client

        private void Awake()
        {
            _dataStream = Instantiate(_dataStream, new Vector3(0, 0, 0), Quaternion.identity);
            _userInterface = Instantiate(_userInterface, new Vector3(0, 0, 0), Quaternion.identity);
            _projectContext = Instantiate(_projectContext, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        

        
    }
}