﻿using AutoMapper;
using BusinessLogicalLayer.API;
using BusinessLogicalLayer.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Extensions;
using System.Security.Claims;
using VisualLayer.Models.Funcionario;

namespace VisualLayer.Controllers.Funcionario
{
    public class FuncionarioController : Controller
    {
        private const string ENCRYPT = "ID";
        private const int NIVEL_PERMISSAO = 3;
        private readonly IEstadoService _estadoService;
        private readonly IFuncionarioService _FuncionarioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly ISF36Service _sf36Service;
        private readonly IWebHostEnvironment hostEnvironment;
        public FuncionarioController(IWebHostEnvironment hostEnvironment, IFuncionarioService funcionarioService, IEstadoService estadoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISF36Service sf36Service)
        {
            _FuncionarioService = funcionarioService;
            _estadoService = estadoService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            this.hostEnvironment = hostEnvironment;
            _sf36Service = sf36Service;
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(string senha)
        {
            Entities.Funcionario funcionario = _FuncionarioService.GetByID(Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value.Decrypt(ENCRYPT))).Result.Item;
            funcionario.Senha = senha.Hash();
            Response response = await _FuncionarioService.AlterarSenha(funcionario);
            JsonResult result = Json(response);
            return Json(response);
        }

        [HttpGet]
        public async Task<IActionResult> Buscar(string cep)
        {
            CepAPI a = CepAPI.Busca(cep);
            ViewBag.Endereco = a;
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Configuracao()
        {
            Entities.Funcionario verify = _FuncionarioService.GetInformationToVerify(Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value.Decrypt(ENCRYPT))).Result.Item;
            if (verify.Cargo.NivelPermissao != NIVEL_PERMISSAO || verify.IsFirstLogin || verify.HasRequiredTest)
            {
                return RedirectToAction(actionName: "Logarr", controllerName: "Home");
            }
            Entities.Funcionario funcionario = _FuncionarioService.GetByID(verify.ID).Result.Item;
            FuncionarioDetailViewModel funcionarioDetail = _mapper.Map<FuncionarioDetailViewModel>(funcionario);
            funcionarioDetail.Id = funcionarioDetail.Id.Encrypt(ENCRYPT);
            funcionarioDetail.Cep = funcionario.Endereco.CEP;
            funcionarioDetail.NumeroCasa = funcionario.Endereco.NumeroCasa;
            funcionarioDetail.Rua = funcionario.Endereco.Rua;
            funcionarioDetail.Complemento = funcionario.Endereco.Complemento;
            funcionarioDetail.Bairro = funcionario.Endereco.Bairro.NomeBairro;
            funcionarioDetail.Cidade = funcionario.Endereco.Bairro.Cidade.NomeCidade;
            funcionarioDetail.Estado = funcionario.Endereco.Bairro.Cidade.Estado.NomeEstado;
            return View(funcionarioDetail);
        }
        [Authorize]
        public async Task<IActionResult> Funcionarios()
        {
            Entities.Funcionario verify = _FuncionarioService.GetInformationToVerify(Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value.Decrypt(ENCRYPT))).Result.Item;
            if (verify.Cargo.NivelPermissao != NIVEL_PERMISSAO || verify.IsFirstLogin || verify.HasRequiredTest)
            {
                return RedirectToAction(actionName: "Logarr", controllerName: "Home");
            }
            DataResponse<Entities.Funcionario> dataResponse = await _FuncionarioService.GetAllAtivos();
            List<FuncionarioSelectViewModel> Funcionarios = new List<FuncionarioSelectViewModel>();
            for (int i = 0; i < dataResponse.Data.Count; i++)
            {
                Funcionarios.Add(_mapper.Map<FuncionarioSelectViewModel>(dataResponse.Data[i]));
            }
            for (int i = 0; i < Funcionarios.Count; i++)
            {
                Funcionarios[i].Id = Funcionarios[i].Id.Encrypt(ENCRYPT);
            }
            return View(Funcionarios);
        }

        [Authorize]
        public IActionResult Index()
        {
            Entities.Funcionario verify = _FuncionarioService.GetInformationToVerify(Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value.Decrypt(ENCRYPT))).Result.Item;
            if (verify.Cargo.NivelPermissao != NIVEL_PERMISSAO || verify.IsFirstLogin || verify.HasRequiredTest)
            {
                return RedirectToAction(actionName: "Logarr", controllerName: "Home");
            }
            return View();
        }

        [Route("api/Funcionario/JsonStringBody")]
        public string JsonStringBody([FromBody] string content)
        {
            return content;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SF36()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SF36(FuncionarioRespostasQuestionarioSf36 respostas)
        {
            Response response = await _sf36Service.CalcularScore(respostas);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Update()
        {
            int id = Convert.ToInt32(_httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.Sid).Value.Decrypt(ENCRYPT));
            SingleResponse<Entities.Funcionario> response = await _FuncionarioService.GetByID(id);
            if (response.Item.IsFirstLogin)
            {
                FuncionarioUpdateViewModel funcionario = _mapper.Map<FuncionarioUpdateViewModel>(response.Item);
                funcionario.EstadoUf = response.Item.Endereco.Bairro.Cidade.Estado.Sigla;
                funcionario.Cep = response.Item.Endereco.CEP;
                funcionario.NumeroCasa = response.Item.Endereco.NumeroCasa;
                funcionario.Rua = response.Item.Endereco.Rua;
                funcionario.Complemento = response.Item.Endereco.Complemento;
                funcionario.Bairro = response.Item.Endereco.Bairro.NomeBairro;
                funcionario.Cidade = response.Item.Endereco.Bairro.Cidade.NomeCidade;
                string caminho_WebRoot = hostEnvironment.WebRootPath;
                string path = Path.Combine(caminho_WebRoot, $"SystemImg\\Funcionarios\\{funcionario.Cpf.StringCleaner()}");
                funcionario.Foto = $"/SystemImg/Funcionarios/{funcionario.Cpf.StringCleaner()}.jpg";
                List<Estado> estados = _estadoService.GetAll().Result.Data;
                ViewBag.Estados = estados;
                return View(funcionario);
            }
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(FuncionarioUpdateViewModel funcionarioUpdate)
        {
            Entities.Funcionario funcionario2 = _FuncionarioService.GetByID(Convert.ToInt32(funcionarioUpdate.Id)).Result.Item;
            funcionario2.Nome = funcionarioUpdate.Nome;
            funcionario2.Cpf = funcionarioUpdate.Cpf;
            funcionario2.Endereco.Bairro.Cidade.Estado = _estadoService.GetByUF(funcionarioUpdate.EstadoUf).Result.Item;
            funcionario2.Endereco.Bairro.Cidade.EstadoId = funcionario2.Endereco.Bairro.Cidade.Estado.ID;
            funcionario2.Endereco.Bairro.Cidade.NomeCidade = funcionarioUpdate.Cidade;
            funcionario2.Endereco.Bairro.NomeBairro = funcionarioUpdate.Bairro;
            funcionario2.Endereco.Complemento = funcionarioUpdate.Complemento;
            funcionario2.Endereco.NumeroCasa = funcionarioUpdate.NumeroCasa;
            funcionario2.Endereco.CEP = funcionarioUpdate.Cep;
            funcionario2.Endereco.Rua = funcionarioUpdate.Rua;
            funcionario2.DataNascimento = funcionarioUpdate.DataNascimento;            
            Response response = await _FuncionarioService.UpdateFuncionario(funcionario2);
            if (response.HasSuccess)
            {
                if (funcionarioUpdate.Image.Length != 0)
                {
                    string ext = Path.GetExtension(funcionarioUpdate.Image.FileName);
                    if (ext != ".jpg" && ext != ".png")
                    {
                        ViewBag.Erros = "imagem deve ter as extensões .jpg, .png";
                        List<Estado> estados = _estadoService.GetAll().Result.Data;
                        ViewBag.Estados = estados;
                        return RedirectToAction(actionName: "Update");
                    }
                    string caminho_WebRoot = hostEnvironment.WebRootPath;
                    string path = Path.Combine(caminho_WebRoot, "SystemImg\\Funcionarios\\");
                    using (FileStream fs = new FileStream(path + funcionarioUpdate.Cpf.StringCleaner() + ".jpg", FileMode.Create))
                    {
                        await funcionarioUpdate.Image.CopyToAsync(fs);
                    }
                }
                if (funcionario2.Cargo.NivelPermissao == 3)
                    return RedirectToAction(actionName: "Index", controllerName: "Funcionario");
                if (funcionario2.Cargo.NivelPermissao == 1)
                    return RedirectToAction(actionName: "Index", controllerName: "RH");
                if (funcionario2.Cargo.NivelPermissao == 0)
                    return RedirectToAction(actionName: "Index", controllerName: "Adm");
            }
            return RedirectToAction(actionName: "Update");
        }
    }
}