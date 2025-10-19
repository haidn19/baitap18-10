namespace ngay18_10
{
    public class AccountBuss
    {
        private AccountDAO dao = new AccountDAO();

        public bool Login(string username, string password)
        {
            return dao.Login(username, password);
        }

        public bool Register(string username, string password)
        {
            return dao.Register(username, password);
        }

        public bool ChangePassword(string username, string newPassword)
        {
            return dao.ChangePassword(username, newPassword);
        }

        public int DeleteInactiveAccounts()
        {
            return dao.DeleteInactiveAccounts();
        }
    }
}
