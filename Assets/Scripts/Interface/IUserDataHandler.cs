using Models;

namespace Interface
{
    public interface IUserDataHandler : IService
    {
        public void SaveUserData();
        public void LoadUserData();
        public User GetUserById(int id);
        public int GetActiveUserId();
    }
}