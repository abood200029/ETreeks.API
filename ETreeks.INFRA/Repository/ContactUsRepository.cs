﻿using Dapper;
using ETreeks.CORE.Common;
using ETreeks.CORE.Data;
using ETreeks.CORE.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace ETreeks.INFRA.Repository
{
    public class ContactUsRepository : IRepository<ContactU>
    {
        private readonly IDbContext _dbContext;
        public ContactUsRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ContactU> GetAll()
        {
            IEnumerable<ContactU> result = _dbContext.Connection.Query<ContactU>("CONTACTUS_PACKAGE.GETALLCONTACTUS", commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public ContactU GetById(int id)
        {
            var p = new DynamicParameters();
            p.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            IEnumerable<ContactU> result = _dbContext.Connection.Query<ContactU>("CONTACTUS_PACKAGE.GETCONTACTUSBYID", p, commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }

        public int Create(ContactU contactU)
        {
            int result;
            var p = new DynamicParameters();
            p.Add("NAME", contactU.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("EMAIL", contactU.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("MESSAGE", contactU.Message, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("res", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbContext.Connection.Execute("CONTACTUS_PACKAGE.CREATECONTACTUS", p, commandType: CommandType.StoredProcedure);
            result = p.Get<int>("res");
            return result;
        }

        public int Update(ContactU contactU)
        {
            int result;
            var p = new DynamicParameters();
            p.Add("ID", contactU.Id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("NAME", contactU.Name, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("EMAIL", contactU.Email, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("MESSAGE", contactU.Message, dbType: DbType.String, direction: ParameterDirection.Input);
            p.Add("res", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbContext.Connection.Execute("CONTACTUS_PACKAGE.UPDATECONTACTUS", p, commandType: CommandType.StoredProcedure);
            result = p.Get<int>("res");
            return result;
        }

        public int Delete(int id)
        {
            int result;
            var p = new DynamicParameters();
            p.Add("ID", id, dbType: DbType.Int32, direction: ParameterDirection.Input);
            p.Add("res", dbType: DbType.Int32, direction: ParameterDirection.Output);
            _dbContext.Connection.Execute("CONTACTUS_PACKAGE.DELETECONTACTUS", p, commandType: CommandType.StoredProcedure);
            result = p.Get<int>("res");
            return result;
        }
    }
}
