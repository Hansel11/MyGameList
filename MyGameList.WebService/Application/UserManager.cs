using Microsoft.Data.Sqlite;
using MyGameList.Entities;
using MyGameList.Domain.PsdDB;
using MyGameList.Domain.Request;
using MyGameList.Domain.Response;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using psdtest.Domain.Response;

namespace MyGameList.WebService.Application
{
    public class UserManager
    {
        public UserManager()
        {
        }

        public static UserResponseDTO LoginUser(UserRequestDTO req)
        {
            try
            {
                using (var context = new EntityContext<MsUser>())
                {
                    var user = (from u in context.Data
                                  where u.Username == req.Username
                                  select u).FirstOrDefault();

                    if (user == null) return null;

                    if (user.Password == req.Password)
                    {
                        var res = new UserResponseDTO
                        {
                            UserId = user.Id
                        };
                        return res;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public static UserResponseDTO RegisterUser(UserRequestDTO req)
        {
            try
            {
                using (var context = new EntityContext<MsUser>())
                {
                    var uname = (from u in context.Data
                                 where u.Username == req.Username
                                 select u).FirstOrDefault();
                    if (uname != null) return null;

                    var user = new MsUser
                    {
                        Id = Guid.NewGuid(),
                        Username = req.Username,
                        Password = req.Password
                    };
                    context.Add(user);
                    context.SaveChanges();

                    var res = new UserResponseDTO
                    {
                        UserId = user.Id
                    };
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}