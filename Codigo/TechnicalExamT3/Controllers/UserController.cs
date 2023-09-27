using Business.BusinessLogic;
using Business.DataAccess.Factory;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using TechnicalExamT3.Mappers;
using TechnicalExamT3.Models;
using UserServiceWCF;
using X.PagedList;

namespace TechnicalExamT3.Controllers
{
    public class UserController : Controller
    {
        private readonly UserBL _userBL;
        private readonly IUserService _userServiceWCF;

        public UserController(IContextDBFactory contextDBFactory, IUserService userServiceWCF) 
        { 
            _userBL = new UserBL(contextDBFactory);
            _userServiceWCF = userServiceWCF;
        }


        // GET: UserController
        public async Task<ActionResult> Index(int? currentPage, int? pageSize=10)
        {
            try
            {
                var users = await _userServiceWCF.GetAllAsync();
                //var users = await _userBL.GetAll();

                //aplicamos paginacion
                int pageNumber = (currentPage ?? 1);

                //convertir a modelos vista
                List<UserViewModel> usersView = UserMapper.FromUsersDTOToUsersViewModel(users)?.ToList();
                //List<UserViewModel> usersView = UserMapper.FromUsersToViewModels(users)?.ToList();

                var pagedList = usersView.ToPagedList(pageNumber, pageSize ?? default(int));

                return View(pagedList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }      
        }


        // GET: UserController/Transvesal/
        public async Task<ActionResult> Transversal(string actionName, string? id)
        {
            
            UserCompositeViewModel userActionViewModel = new UserCompositeViewModel()
            {
                Action = new UserActionViewModel() { ActionName = actionName, Id = id},
                User = string.IsNullOrEmpty(id) ? null : UserMapper.FromUserToUserViewModel(await _userBL.Get(id))
            };

            return View(userActionViewModel);
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserCompositeViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resp = await _userServiceWCF.RegisterAsync(UserMapper.FromUserViewModelToUserDTO(userModel.User));
                    //var resp = await _userBL.Register(UserMapper.FromUserViewModelToUser(userModel.User));

                    ViewBag.SuccessMessage = ProjectResources.Resource.UserAddedSuccess;

                    return RedirectToAction("Index");
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                string errorMessage = string.Join(" ", errors);

                // Establece el mensaje de error en ViewBag o ViewData
                ViewBag.ErrorMessage = errorMessage;

                return View("Transversal", userModel);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Transversal", userModel);
            }
        }


        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserCompositeViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resp = await _userServiceWCF.UpdateAsync(UserMapper.FromUserViewModelToUserDTO(userModel.User));
                    //var resp = await _userBL.Update(UserMapper.FromUserViewModelToUser(userModel.User));

                    return RedirectToAction(nameof(Index));
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                string errorMessage = string.Join(" ", errors);

                // Establece el mensaje de error en ViewBag o ViewData
                ViewBag.ErrorMessage = errorMessage;

                return View("Transversal", userModel);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Transversal", userModel);
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(UserCompositeViewModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var resp = await _userServiceWCF.DeleteAsync(userModel.User.Id);
                    //var resp = await _userBL.Delete(userModel.User.Id);

                    return RedirectToAction(nameof(Index));
                }

                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                string errorMessage = string.Join(" ", errors);

                // Establece el mensaje de error en ViewBag o ViewData
                ViewBag.ErrorMessage = errorMessage;

                return View("Transversal", userModel);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Transversal", userModel);
            }
        }

        public async Task<ActionResult> DownloadExcel()
        {
            try
            {
                var users = await _userServiceWCF.GetAllAsync();
                //var users = await _userBL.GetAll();

                #region generacion excel
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Usuarios");

                    worksheet.Cell(1, 1).Value = "Nombre";
                    worksheet.Cell(1, 2).Value = "Fecha de Nacimiento";
                    worksheet.Cell(1, 3).Value = "Sexo";

                    int row = 2;
                    foreach (var user in users)
                    {
                        worksheet.Cell(row, 1).Value = user.Name;
                        worksheet.Cell(row, 2).Value = user.BirthDate.ToString("dd/MM/yyyy");
                        worksheet.Cell(row, 3).Value = user.Sex;
                        row++;               
                    }

                    // Escribe el libro de trabajo en el flujo de respuesta
                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        var content = memoryStream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            $"Usuarios{DateTime.Now.ToShortDateString()}.xlsx");
                    }
                }
                #endregion

            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
