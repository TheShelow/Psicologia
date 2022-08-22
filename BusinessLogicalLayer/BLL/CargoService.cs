﻿using BusinessLogicalLayer.Interfaces;
using DataAcessLayer.Interfaces;
using Entities;
using Shared;

namespace BusinessLogicalLayer.BLL
{
    public class CargoService : ICargoService
    {
        private readonly ICargoDAL _cargoDAL;

        public CargoService(ICargoDAL cargoDAL)
        {
            _cargoDAL = cargoDAL;
        }

        public async Task<Response> Delete(Cargo cargo)
        {
            return await _cargoDAL.Delete(cargo);
        }

        public async Task<DataResponse<Cargo>> GetAll()
        {
            return await _cargoDAL.GetAll();
        }

        public async Task<SingleResponse<Cargo>> GetByID(int id)
        {
            return await _cargoDAL.GetByID(id);
        }

        public async Task<SingleResponse<bool>> Iniciar()
        {
            SingleResponse<int> response = await _cargoDAL.Iniciar();
            return ResponseFactory<bool>.CreateItemResponse(response.Message, response.HasSuccess, response.Item > 0);
        }

        public async Task<SingleResponse<int>> IniciarReturnId()
        {
            SingleResponse<int> response = await _cargoDAL.IniciarReturnId();
            return ResponseFactory<int>.CreateItemResponse(response.Message, response.HasSuccess, response.Item);
        }

        public async Task<Response> Insert(Cargo cargo)
        {
            return await _cargoDAL.Insert(cargo);
        }

        public async Task<SingleResponse<int>> InsertReturnId(Cargo cargo)
        {
            return await _cargoDAL.InsertReturnId(cargo);
        }

        public async Task<Response> Update(Cargo cargo)
        {
            return await _cargoDAL.Update(cargo);
        }
    }
}