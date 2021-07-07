using models.DatabaseTable;
using services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services
{
    public class QuoteService : iCRUD<models.DatabaseTable.template>
    {
        //public int Add(user usr)
        //{
        //    using (var db = new AppDb())
        //    {
        //        var checkuser = db.users.FirstOrDefault(f => f.Email == usr.Email);
        //        if (checkuser != null)
        //        {
        //            throw new Exception("User already exists, Click on forgot password for regenerate your password!");
        //        }
        //        db.users.Add(usr);
        //        var rec = db.SaveChanges();
        //        return rec;
        //    }
        //}

        //public int UpdatePass(user usr)
        //{
        //    using (var db = new AppDb())
        //    {
        //        var checkuser = db.users.FirstOrDefault(f => f.Email == usr.Email);
        //        if (checkuser != null)
        //        {
        //            checkuser.Password = usr.Password;
        //            var rec = db.SaveChanges();
        //            return rec;
        //        }
        //        throw new Exception("User not register yet, Please register user first!");
        //    }
        //}
        public int Add(template obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(int Id)
        {
            throw new NotImplementedException();
        }
        

        public template Get(int Id)
        {
            throw new NotImplementedException();
        }

        public int Update(template obj)
        {
            throw new NotImplementedException();
        }
    }
}
