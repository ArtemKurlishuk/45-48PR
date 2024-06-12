using API_Kurlishuk.Context;
using API_Kurlishuk.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace API_Kurlishuk.Controllers
{
    [Route("api/TasksController")] //указываем раздел
    public class TasksController : Controller
    {
        ///<summary>
        ///Получение списка задач
        ///</summary>
        ///<remarks> Данный метод получает список задач, находящиеся в БД</remarks>
        ///<response code="200"> При выполнении запроса выпали ошибки</response> 
        [Route("List")] // указываем какой метод вызывается
        [HttpGet] //указываем какой тип запроса используется
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]

        public ActionResult List()
        {
            try
            {
                // получаем список задач из БД
                IEnumerable<Tasks> Tasks = new TasksContext().Tasks;
                return Json(Tasks); // возвращаем ответ в виде JSON
            }
            catch (Exception exp)
            {
                return StatusCode(500); // если возникли неполадки, то вызываем 500ю ошибку(ошибка серевра)
            }
        }
        ///<summary>
        ///Получение списка задач
        ///</summary>
        ///<remarks> Данный метод получает список задач, находящиеся в БД</remarks>
        ///<response code="200"> При выполнении запроса выпали ошибки</response> 
        [Route("Item")] // указываем какой метод вызывается
        [HttpGet] //указываем какой тип запроса используется
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id) 
        {
            try
            {
                //получаем задачу по коду
                Tasks Task = new TasksContext().Tasks.Where(x => x.Id == Id).First();
                return Json(Task); // Возвращаем ответ в виде JSON
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

    }
}
