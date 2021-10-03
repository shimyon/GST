using models.DatabaseTable;
using models.ViewModels;
using MySql.Data.MySqlClient;
using services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class UserService : iCRUD<user>
    {
        services.Common.DatatableService datatableService = new Common.DatatableService();
        public DataTable<UserDatatable> GetList(UserSearch search, List<MySqlParameter> filters)
        {
            try
            {
                using (var ctx = new AppDb())
                {
                    var result = datatableService.GetDataTableResult<UserDatatable>("users_list_sp", search, filters);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public int Edit(user usr)
        {
            using (var db = new AppDb())
            {
                var checkuser = db.users.FirstOrDefault(f => f.Id == usr.Id);
                if (checkuser == null)
                {
                    throw new Exception("User not found!");
                }
                else
                {
                    checkuser.Lastname = usr.Lastname;
                    checkuser.Firstname = usr.Firstname;
                    checkuser.Role = usr.Role;
                    checkuser.Email = usr.Email;
                }
                db.Entry(checkuser).State = System.Data.Entity.EntityState.Modified;
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

        public user Get(int Id)
        {
            using (var db = new AppDb())
            {
                var data = db.users.FirstOrDefault(f => f.Id == Id);
                return data;
            }
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
