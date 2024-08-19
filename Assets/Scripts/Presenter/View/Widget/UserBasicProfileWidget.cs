using System.Collections;
using Attributes;
using Interface;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presenter.View.Widget
{
    public class UserBasicProfileWidget : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _userProfilePictureObject;
        [SerializeField] 
        private GameObject _userNicknameObject;
        
        [Inject]
        private IUserDataHandler _userDataHandler;

        public bool isActiveUser;
        public int userID;

        private void OnEnable()
        {
            StartCoroutine(WaitForUserData());
        }
        
        private void OnDisable()
        {
            _userProfilePictureObject.GetComponent<Image>().sprite = null;
            _userNicknameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
        
        private IEnumerator WaitForUserData()
        {
            yield return new WaitUntil(() => _userDataHandler != null);
            SetUserData();
        }

        private void SetUserData()
        {
            if (isActiveUser)
            {
                SetActiveUserData();
                return;
            }

            SetUserDataByID();
        }

        private void SetUserDataByID()
        {
            var user = _userDataHandler.GetUserById(userID);
            
            if(user == null) return;
            
            _userProfilePictureObject.GetComponent<Image>().sprite = user.Profile.ProfileImage;
            _userNicknameObject.GetComponent<TextMeshProUGUI>().text = user.Profile.Nickname;
            
        }

        private void SetActiveUserData()
        {
            var activeUser = _userDataHandler.GetUserById(_userDataHandler.GetActiveUserId());
            var profile = activeUser.Profile;

            _userProfilePictureObject.GetComponent<Image>().sprite = profile.ProfileImage;
            _userNicknameObject.GetComponent<TextMeshProUGUI>().text = profile.Nickname;
        }
    }
}