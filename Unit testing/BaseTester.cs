using Domain.Managers;
using Infrastructure;
using Logic.Models;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit_testing
{
    [TestClass]
    public class BaseTester
    {
        protected Manager Manager = new();

        protected void SetUserRole(UserRole role) => Manager.SetUserRole(role);
    }
}
