using SmartBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repository.Interface
{
     public interface IProfile
    {
        public Task<string> CreateProfile(ProfileModel profile);

        public Task<Profile> UpdateProfile(Profile profile);

        public Task<Profile> GetProfile(int profileId);

        public Task<List<Profile>> GetProfiles();





    }
}
