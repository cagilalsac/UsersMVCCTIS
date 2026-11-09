namespace CORE.Authentication.Services
{
    
    public interface ICookieAuthService
    {
        
        public int UserId { get; }

       
        public Task SignIn(int userId, string userName,
            IEnumerable<string> userRoleNames,
            DateTime? expiration = default,
            bool isPersistent = true);

        
        public Task SignOut();
    }
}
