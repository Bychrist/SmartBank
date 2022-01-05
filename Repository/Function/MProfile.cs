using Microsoft.EntityFrameworkCore;
using SmartBank.Models;
using SmartBank.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBank.Repository.Function
{
    public class MProfile : IProfile
    {
        private readonly AuthenticationContext _db;
        private readonly IAccount _account;

        public MProfile(AuthenticationContext db,IAccount account)
        {
            _db = db;
            _account = account;
        }


        public async Task<string> CreateProfile(ProfileModel profile)
        {

            using (var transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    var profileDb = new Profile
                    {
                        FirstName = profile.FirstName,
                        LastName = profile.LastName,
                        Email = profile.Email,
                        PhoneNumber = profile.PhoneNumber
                    };

                  await  _db.AddAsync(profileDb);
                  await  _db.SaveChangesAsync();

                    var accountDb = new Account
                    {
                        ProfileId = profileDb.Id
                    };

                    await   _account.CreateAccount(accountDb);


                    transaction.Commit();

                    return "success";
                    

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    return e.Message.ToString();
                }
            }
          
        }

        public async Task<Profile> GetProfile(int profileId)
        {
            try
            {
                var getProfile = await _db.Profiles.Include(a=>a.Accounts).Where(a => a.Id == profileId).FirstOrDefaultAsync();
                
                return getProfile;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<Profile>> GetProfiles()
        {
            try
            {
                var allProfiles = await _db.Profiles.Include(p=>p.Accounts).OrderByDescending(c=>c.Created_at).ToListAsync();
                return allProfiles;
            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<Profile> UpdateProfile(Profile profile)
        {
            try
            {
                var getProfile = await _db.Profiles.FindAsync(profile.Id);

                getProfile.LastName = profile.LastName;
                getProfile.FirstName = profile.FirstName;
                getProfile.PhoneNumber = profile.PhoneNumber;
                if (profile.Email != getProfile.Email)
                      getProfile.Email = profile.Email;

                await  _db.SaveChangesAsync();
                return profile;

            }
            catch (Exception e)
            {

                throw;
            }


        }







    }


}
