using NH.JewelryErpMini.Models.Initial;
using NH.JewelryErpMini.Models.viewDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.Repository
{
    public class AccountRepository
    {
        private JewelryDbContext db;
        public AccountRepository()
        {
            this.db = new JewelryDbContext();
        }

        public Message<UserDto> Login(string code, string password)
        {
            Message<UserDto> _message = new Message<UserDto>();
            UserDto userDto = this.db.Users.Where(u => u.Code == code && u.Password == password)
                              .Select(u => new UserDto { Id = u.Id, usr_Code = u.Code, usr_Name = u.Name, usr_Password = u.Password, usr_Dept_Id = u.DeptId, usr_Dept = u.Dept.DeptName })
                              .SingleOrDefault();
            if (userDto == null)
            {
                _message.isSuccess = false;
                _message.message = "登录失败，工号或密码不正确！";
            }
            else
            {
                _message.isSuccess = true;
                _message.message = "登录成功！";
            }
            _message.model = userDto;
            return _message;
        }
    }
}