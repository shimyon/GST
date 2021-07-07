using models.DatabaseTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class UserService
    {
        public int Add(user usr)
        {
            using (var db = new AppDb())
            {
                var checkuser = db.users.FirstOrDefault(f => f.Email == usr.Email);
                if (checkuser != null)
                {
                    throw new Exception("User already exists, Click on forgot password for regenerate your password!");
                }
                db.users.Add(usr);
                var rec = db.SaveChanges();
                return rec;
            }
        }

        public int UpdatePass(user usr)
        {
            using (var db = new AppDb())
            {
                var checkuser = db.users.FirstOrDefault(f => f.Email == usr.Email);
                if (checkuser != null)
                {
                    checkuser.Password = usr.Password;
                    var rec = db.SaveChanges();
                    return rec;
                }
                throw new Exception("User not register yet, Please register user first!");
            }
        }

        public user Auth(string userName, string password)
        {
            using (var db = new AppDb())
            {
                services.Common.PasswordCryptoService crypto = new Common.PasswordCryptoService();
                string encpass = crypto.EncryptText(password);
                var user = db.users.FirstOrDefault(f => f.Username == userName && f.Password == encpass);
                return user;
            }
        }
    }
}
