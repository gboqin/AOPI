using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NH.JewelryErpMini.Models.viewDto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string usr_Name { get; set; }
        public string usr_Code { get; set; }
        public string usr_Password { get; set; }
        public int usr_Dept_Id { get; set; }
        public string usr_Dept { get; set; }
    }
}