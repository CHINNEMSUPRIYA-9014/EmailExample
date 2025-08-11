using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailSMTP
{
    public class App
    {
        EmailService s;

        public App(IEmailService n)
        {
            s = (EmailService?)n;
        }

        public void Run()
        {
            s.Greet();
        }
    }
}
