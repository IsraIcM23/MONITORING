using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WACO
{
    public class WacoController
    {
        public List<User> _users = new List<User>();
        public WacoController(List<User> users)
        {
            _users = users;
        }

        public void Add(User myUser)
        {
            if (!ExistsUserWithCI(myUser.CI))
            {
                _users.Add(myUser);
            }
        }

        public bool ExistsUserWithCI(int ci)
        {
            return _users.Any(item => item.CI == ci);
        }

        public User FindUser(int ci)
        {
            var result = _users.FirstOrDefault(r => r.CI.Equals(ci));
            if(result == null)
            {
                throw new UserException();
            }
            return result;
        }

        public void ShowUsers()
        {
            foreach (var item in _users)
            {
                Console.WriteLine(item.Name+"/"+item.SurName+"/"+item.CI);
            }
        }
    }
}
