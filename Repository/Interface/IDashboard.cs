using SmartBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repository.Interface
{
    public interface IDashboard
    {
        public Task<Dashboard> GetToDashboard();
    }
}
