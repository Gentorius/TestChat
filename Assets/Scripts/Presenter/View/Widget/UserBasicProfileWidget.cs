using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Presenter.View.Widget
{
    public class UserBasicProfileWidget : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _userProfilePictureObject;
        [SerializeField] 
        private GameObject _userNicknameObject;

        public bool isActiveUser;
        public int userID;

        private void OnEnable()
        {
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
            var users = GameObject.Find("ProjectContext(Clone)").GetComponent<ProjectContext>()
                .UserConfigController.UserStorage.Users;

            foreach (var user in users)
            {
                if (user.ID == userID)
                {
                    _userProfilePictureObject.GetComponent<Image>().sprite = user.Profile.ProfileImage;
                    _userNicknameObject.GetComponent<TextMeshProUGUI>().text = user.Profile.Nickname;
                    break;
                }
            }
        }

        private void SetActiveUserData()
        {
            var profile = GameObject.Find("ProjectContext(Clone)").GetComponent<ProjectContext>().UserConfigController.UserStorage.ActiveUser.Profile;

            _userProfilePictureObject.GetComponent<Image>().sprite = profile.ProfileImage;
            _userNicknameObject.GetComponent<TextMeshProUGUI>().text = profile.Nickname;
        }
    }
}