using CrossTech.Application.Interfaces.UseCases.EmployeesUseCases;
using CrossTech.Application.UseCases;
using CrossTech.Application.UseCases.EmployeesUseCases.AddEmployee;
using CrossTech.Application.UseCases.EmployeesUseCases.RemoveEmployee;
using CrossTech.Application.UseCases.EmployeesUseCases.UpdateEmployee;
using CrossTech.Web.DTO;
using CrossTech.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrossTech.Web.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IRequestEmployeeListUseCase _listUC;
        private readonly IUpdateEmployeeUseCase _updateUC;
        private readonly IAddEmployeeUseCase _addUC;
        private readonly IRemoveEmployeeUseCase _removeUC;

        public EmployeesController(IRequestEmployeeListUseCase listUC,
                                   IUpdateEmployeeUseCase updateUC,
                                   IAddEmployeeUseCase addUC,
                                   IRemoveEmployeeUseCase removeUC)
        {
            _listUC = listUC;
            _updateUC = updateUC;
            _addUC = addUC;
            _removeUC = removeUC;
        }

        /// <summary>
        /// Просмотр всех сотрудников системы
        /// </summary>
        /// <returns></returns>
        [UserCan("see_employees_list")]
        public IActionResult Index()
        {
            var userId = Content(User.Identity.Name);
            var result = _listUC.Handle(new BasicUserUseCaseRequest { Requester = new Application.DTO.Requester { Id = int.Parse(userId.Content) } });
            return View(result.Result);
        }

        public IActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// Обновление сотрудника и связанных с ним сущностей
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("employees/{id}/edit")]
        [UserCan("update_employee")]
        public IActionResult Update(UpdateEmployeeUseCaseRequest request)
        {
            var validationResult = _updateUC.Validate(request);
            if (validationResult.Success && validationResult.Result.IsValid)
            {
                var updateStatus = _updateUC.Handle(request);
                if (updateStatus.Success)
                {
                    return Ok(new Response(true, updateStatus.Result.Employee));
                }
                else
                {
                    return StatusCode(500, new Response(updateStatus.Reason));
                }
            }
            else
            {
                this.HttpContext.Response.StatusCode = 422;
                return new JsonResult(new Response(validationResult.Result));
            }
        }

        /// <summary>
        /// Удаление сотрудника и связанных с ним сущностей
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("employees/{id}")]
        [UserCan("delete_employee")]
        public IActionResult Delete(int id)
        {
            RemoveEmployeeUseCaseRequest request = new RemoveEmployeeUseCaseRequest { Id = id };
            var validationResult = _removeUC.Validate(request);
            if (validationResult.Success && validationResult.Result.IsValid)
            {
                var updateStatus = _removeUC.Handle(request);
                if (updateStatus.Success)
                {
                    return Ok(new Response(true));
                }
                else
                {
                    return StatusCode(500, new Response(updateStatus.Reason));
                }
            }
            else
            {
                this.HttpContext.Response.StatusCode = 422;
                return new JsonResult(new Response(validationResult.Result));
            }
        }

        /// <summary>
        /// Создание записи сотрудника и всех связанных с ним сущностей
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("employees")]
        [UserCan("create_employee")]
        public IActionResult Store(AddEmployeeUseCaseRequest request)
        {
            var validationResult = _addUC.Validate(request);
            if (validationResult.Success && validationResult.Result.IsValid)
            {
                var addingResult = _addUC.Handle(request);
                if (addingResult.Success)
                {
                    return Ok(new Response(true, addingResult.Result.Employee));
                }
                else
                {
                    return StatusCode(500, new Response(addingResult.Reason));
                }
            }
            else
            {
                this.HttpContext.Response.StatusCode = 422;
                return new JsonResult(new Response(validationResult.Result));
            }
        }
    }
}