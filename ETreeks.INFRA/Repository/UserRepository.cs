﻿using Dapper;
using ETreeks.CORE.Common;
using ETreeks.CORE.Data;
using ETreeks.CORE.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ETreeks.INFRA.Repository
{
    public class UserRepository:IRepository<User>
    {
        private readonly IDbContext _dbContext;
        public UserRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(User user)
        {
            int result;
            var p = new DynamicParameters();
            p.Add("FIRSTNAME", user.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LASTNAME", user.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("BIRTHDATE", user.BirthDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("PHONENUMBER", user.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("IMAGEUSER", user.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("res", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbContext.Connection.Execute("USERS_PACKAGE.CREATEUSERS", p, commandType: CommandType.StoredProcedure);
            result = p.Get<int>("res");
            return result;
        }

        public int Delete(int id)
        {
            int result;
            var p = new DynamicParameters();
            p.Add("USERID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("res", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbContext.Connection.Execute("USERS_PACKAGE.DELETEUSERS", p, commandType: CommandType.StoredProcedure);
            result = p.Get<int>("res");
            return result;
        }

        public List<User> GetAll()
        {
            IEnumerable<User> result = _dbContext.Connection.Query<User>("USERS_PACKAGE.GETALLUSERS", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public User GetById(int id)
        {
            var p = new DynamicParameters();
            p.Add("USERID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            IEnumerable<User> result = _dbContext.Connection.Query<User>("USERS_PACKAGE.GETUSERSBYID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public int Update(User user)
        {
            int result;
            var p = new DynamicParameters();
            p.Add("USERID", user.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("FIRSTNAME", user.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("LASTNAME", user.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("BIRTHDATE", user.BirthDate, dbType: DbType.DateTime, direction: ParameterDirection.Input);
            p.Add("PHONENUMBER", user.PhoneNumber, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("IMAGEUSER", user.Image, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("res", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbContext.Connection.Execute("USERS_PACKAGE.UPDATEUSERS", p, commandType: CommandType.StoredProcedure);
            result = p.Get<int>("res");
            return result;
        }
    }
}