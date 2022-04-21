namespace DatabaseConnection
{
    public interface IUserRepository
    {
        User GetUserById(int iUserId);
        User GetUserByEmailAddress(string iEmail);
        User NewUser(User ioUser);
        User UpdateUser(User ioUserChanges);    
        User DeleteUser(int iUserId);
        List<QrCode> GetQrCodes(int iUserId);
    }
}
