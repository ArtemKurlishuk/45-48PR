using API_Kurlishuk.Context;
using API_Kurlishuk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API_Kurlishuk.Controllers
{
    [Route("api/TasksController")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class TasksController : Controller
    {
        /// <summary>
        /// Получение списка задач
        /// </summary>
        /// <remarks>Данный метод получает список задач, находящийся в базе данных</remarks>
        /// <response code="200">Список успешно получен</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("List")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]

        public ActionResult List()
        {
            try
            {
                IEnumerable<Tasks> Tasks = new TasksContext().Tasks;
                return Json(Tasks);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Получение задачи
        /// </summary>
        /// <remarks>Данный метод получает задачу, находящуюся в базе данных</remarks>
        /// <response code="200">Задача успешно получена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        [Route("Item")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Tasks>), 200)]
        [ProducesResponseType(500)]
        public ActionResult Item(int Id)
        {
            try
            {
                Tasks Task = new TasksContext().Tasks.Where(x => x.Id == Id).First();
                return Json(Task);
            }
            catch (Exception exp)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод добавления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <response code="200">Задача успешно добавлена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод добавляет задачу в базу данных</remarks>
        [Route("Add")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Add([FromForm] Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                tasksContext.Tasks.Add(task);
                tasksContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод обновления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <response code="200">Задача успешно изменена</response>
        /// <response code="404">Задача не найдена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод обновляет информацию о задаче в базе данных</remarks>
        [Route("Update")]
        [HttpPut]
        [ApiExplorerSettings(GroupName = "v3")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult Update([FromForm] Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                var findTask = tasksContext.Tasks.FirstOrDefault(x => x.Id == task.Id);
                if (findTask != null)
                {
                    findTask.Name = task.Name;
                    findTask.Priority = task.Priority;
                    findTask.DateExecute = task.DateExecute;
                    findTask.Comment = task.Comment;
                    findTask.Done = task.Done;
                    tasksContext.SaveChanges();
                    return StatusCode(200);
                }
                else return StatusCode(404);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод удаления задачи
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <response code="200">Задача успешно удалена</response>
        /// <response code="404">Задача не найдена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод удаляет информацию о задаче в базе данных</remarks>
        [Route("Delete")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public ActionResult Delete([FromForm] Tasks task)
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                var findTasks = tasksContext.Tasks.FirstOrDefault(x => x.Id == task.Id);
                if (findTasks != null)
                {
                    tasksContext.Remove(findTasks);
                    tasksContext.SaveChanges();
                    return StatusCode(200);
                }
                else return StatusCode(404, "Задача не найдена!");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Метод полной отчистки таблицы
        /// </summary>
        /// <param name="task">Данные о задаче</param>
        /// <response code="200">Таблица успешно очищена</response>
        /// <response code="500">При выполнении запроса возникли ошибки</response>
        /// <returns>Статус выполнения запроса</returns>
        /// <remarks>Данный метод очищает таблицу Задачи</remarks>
        [Route("Delete/All")]
        [HttpDelete]
        [ApiExplorerSettings(GroupName = "v4")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public ActionResult DeleteAll()
        {
            try
            {
                TasksContext tasksContext = new TasksContext();
                tasksContext.RemoveRange(tasksContext.Tasks);
                tasksContext.SaveChanges();
                return StatusCode(200);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
