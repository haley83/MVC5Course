using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{

    enum sex
	{
	        male,
        female
	}
	

    public class MyClass
    {
        public int MyProperty { get; set; }


        public MyClass()
        {
            sex e = sex.male;
            switch (e)
            {
                case sex.male:
                    break;
                case sex.female:
                    break;
                default:
                    break;
            }


            //var o = new ApplicationUser();


        }
    }
}