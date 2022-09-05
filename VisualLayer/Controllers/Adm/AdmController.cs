﻿using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Extensions;
using System.Security.Claims;
using VisualLayer.Models.Funcionario;

namespace VisualLayer.Controllers.Adm
{
    public class AdmController : Controller
    {
        private readonly ICargoService _CargoService;
        private readonly IFuncionarioService _FuncionarioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public AdmController(IFuncionarioService funcionarioService, ICargoService cargoService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _FuncionarioService = funcionarioService;
            _CargoService = cargoService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Analises()
        {
            return View();
        }

        public async Task<IActionResult> Ativar(string id)
        {
            Entities.Funcionario funcionario = _FuncionarioService.GetByID(Convert.ToInt32(id.Decrypt("ID"))).Result.Item;
            funcionario.IsAtivo = false;
            Response response = await _FuncionarioService.Ativar(funcionario);
            return RedirectToAction(actionName: "Funcionarios", controllerName: "Adm");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Cadastrar()
        {
            ViewBag.Cargos = _CargoService.GetAll().Result.Data;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Cadastrar(FuncionarioInsertViewModel funcionarioInsert)
        {
            Entities.Funcionario funcionario = _mapper.Map<Entities.Funcionario>(funcionarioInsert);
            Response response = await _FuncionarioService.Insert(funcionario);
            ViewBag.Cargos = _CargoService.GetAll().Result.Data;
            ViewBag.Errors = response.Message;
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Calendario()
        {
            return View();
        }

        public async Task<IActionResult> Cargo()
        {
            return View();
        }

        public async Task<IActionResult> Deletar(string id)
        {
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
            if (Convert.ToInt32(id.Decrypt("ID")) == userId)
            {
                ViewBag.Erros = "Você não pode se deletar";
            }
            return View();
        }

        public async Task<IActionResult> Desativar(string id)
        {
            int identity = Convert.ToInt32(id.Decrypt("ID"));
            int userId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value);
            if (identity == userId)
            {
                ViewBag.Erros = "Você não pode se desativar";
            }
            Entities.Funcionario funcionario = _FuncionarioService.GetByID(identity).Result.Item;
            funcionario.IsAtivo = false;
            Response response = await _FuncionarioService.Desativar(funcionario);
            return RedirectToAction(actionName: "Funcionarios", controllerName: "Adm");
        }

        public async Task<IActionResult> Detalhar(string id)
        {
            Entities.Funcionario funcionario = _FuncionarioService.GetByID(Convert.ToInt32(id.Decrypt("ID"))).Result.Item;
            FuncionarioDetailViewModel funcionarioDetail = _mapper.Map<FuncionarioDetailViewModel>(funcionario);
            funcionarioDetail.Id = funcionarioDetail.Id.Encrypt("ID");
            funcionarioDetail.Cep = funcionario.Endereco.CEP;
            funcionarioDetail.NumeroCasa = funcionario.Endereco.NumeroCasa;
            funcionarioDetail.Rua = funcionario.Endereco.Rua;
            funcionarioDetail.Complemento = funcionario.Endereco.Complemento;
            funcionarioDetail.Bairro = funcionario.Endereco.Bairro.NomeBairro;
            funcionarioDetail.Cidade = funcionario.Endereco.Bairro.Cidade.NomeCidade;
            funcionarioDetail.Estado = funcionario.Endereco.Bairro.Cidade.Estado.NomeEstado;
            return View(funcionarioDetail);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Editar(string id)
        {
            FuncionarioUpdateAdmViewModel funcionario = _mapper.Map<FuncionarioUpdateAdmViewModel>(_FuncionarioService.GetByID(Convert.ToInt32(id.Decrypt("ID"))).Result.Item);
            funcionario.Id = funcionario.Id.Encrypt("ID");
            ViewBag.Funcionario = funcionario;
            ViewBag.Cargos = _CargoService.GetAll().Result.Data;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Editar(FuncionarioUpdateAdmViewModel model)
        {
            model.Id = model.Id.Decrypt("ID");
            Entities.Funcionario funcionario = _mapper.Map<Entities.Funcionario>(model);
            Response response = await _FuncionarioService.UpdateAdm(funcionario);
            if (response.HasSuccess)
            {
                return RedirectToAction(actionName: "Funcionarios", controllerName: "Adm");
            }
            else
            {
                ViewBag.Funcionario = funcionario;
                ViewBag.Cargos = _CargoService.GetAll().Result.Data;
                ViewBag.Erros = response.Message;
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> Funcionarios()
        {
            DataResponse<Entities.Funcionario> dataResponse = await _FuncionarioService.GetAll();
            List<FuncionarioSelectViewModel> Funcionarios = new List<FuncionarioSelectViewModel>();
            for (int i = 0; i < dataResponse.Data.Count; i++)
            {
                Funcionarios.Add(_mapper.Map<FuncionarioSelectViewModel>(dataResponse.Data[i]));
            }
            for (int i = 0; i < Funcionarios.Count; i++)
            {
                Funcionarios[i].Id = Funcionarios[i].Id.Encrypt("ID");
            }
            return View(Funcionarios);
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> RequisitarUpdate(string id)
        {
            Entities.Funcionario funcionario = _FuncionarioService.GetByID(Convert.ToInt32(id.Decrypt("ID"))).Result.Item;
            Response response = await _FuncionarioService.RequistarUpdate(funcionario);
            return RedirectToAction(actionName: "Funcionarios", controllerName: "Adm");
        }

        public async Task<IActionResult> ResetarSenha(string id)
        {
            _FuncionarioService.ResetarSenha(_FuncionarioService.GetByID(Convert.ToInt32(id.Decrypt("ID"))).Result.Item);
            ViewBag.Funcionario = _mapper.Map<FuncionarioUpdateAdmViewModel>(_FuncionarioService.GetByID(Convert.ToInt32(id.Decrypt("ID"))).Result.Item);
            ViewBag.Cargos = _CargoService.GetAll().Result.Data;
            return RedirectToAction(actionName: "Funcionarios", controllerName: "Adm");
        }
    }
}