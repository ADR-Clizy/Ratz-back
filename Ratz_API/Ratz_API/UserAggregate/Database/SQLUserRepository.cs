namespace DatabaseConnection
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly RatzDbContext _context;

        public SQLUserRepository(RatzDbContext iContext)
        {
            _context = iContext;
        }

        public User GetUserByEmailAddress(string iEmail)
        {
            return _context.Users.FirstOrDefault(aUser => aUser.Email == iEmail);
        }

        public User GetUserById(int iUserId)
        {
            return _context.Users.Find(iUserId);
        }

        public User NewUser(User ioUser)
        {
            _context.Users.Add(ioUser);
            _context.SaveChanges();
            return ioUser;
        }

        public User UpdateUser(User ioUserChanges)
        {
            var aUser = _context.Users.Attach(ioUserChanges);
            aUser.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return ioUserChanges;
        }
        public User DeleteUser(int iUserId)
        {
            User aUser = _context.Users.Find(iUserId);
            if(aUser != null)
            {
                _context.Users.Remove(aUser);
                _context.SaveChanges();
            }
            return aUser;
        }

        public List<QrCode> GetQrCodes(int iUserId)
        {
            return _context.QrCodes.Where(aQrCode => iUserId == aQrCode.UserId).ToList();
        }

        
    }
}
