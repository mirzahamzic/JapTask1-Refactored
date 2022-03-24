using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JapTask1.Core.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }


        //Navigation properties
        public List<Recipe> Recipes { get; set; }
    }
}
