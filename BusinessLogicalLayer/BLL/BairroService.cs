﻿using BusinessLogicalLayer.Interfaces;
using Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.BLL
{
    internal class BairroService : IBairroService
    {
        public Task<Response> Delete(Bairro bairro)
        {
            throw new NotImplementedException();
        }

        public Task<DataResponse<Bairro>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<Bairro>> GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SingleResponse<bool>> Iniciar()
        {
            throw new NotImplementedException();
        }

        public Task<Response> Insert(Bairro bairro)
        {
            throw new NotImplementedException();
        }
        public Task<SingleResponse<int>> InsertReturnId(Bairro bairro)
        {
            throw new NotImplementedException();
        }

        public Task<Response> Update(Bairro bairro)
        {
            throw new NotImplementedException();
        }
    }
}
