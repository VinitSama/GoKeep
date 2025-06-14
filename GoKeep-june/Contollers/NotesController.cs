using GoKeep.Business;
using GoKeep.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;

namespace GoKeep_june.Contollers
{
    [Authorize]
    [Route("GoKeep/notes")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL _noteBL;

        public NotesController(INotesBL noteBL)
        {
            _noteBL = noteBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(new GenericResponseModel { ResponseCode = 400, Description = "Email not found in claims." });
                }
                var result = await _noteBL.GetAllNotes(email);
                var response = new GenericResponseModel<List<GetAllNotesResponseModel>>(result);
                if (result.Any())
                {
                    response.ResponseCode = 200;
                    response.Description = $"All notes of email: {email}";
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Description = "No notes found!!";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewNote(AddNoteRequestModel noteModel)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(new GenericResponseModel { ResponseCode = 400, Description = "Email not found in claims." });
                }
                var result = await _noteBL.AddNewNote(email, noteModel);
                var response = new GenericResponseModel<int>(result);
                if (result > -1)
                {
                    response.ResponseCode = 201;
                    response.Description = "Note Created Successfully";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessful";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote(int noteId)
        {
            try
            {
                var result = await _noteBL.DeleteNote(noteId);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Note Deleted";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessfull";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
        //[Route("/Deleteforever")]
        [HttpDelete("forever/{noteId}")]
        public async Task<IActionResult> DeleteForever(int noteId)
        {
            try
            {
                var result = await _noteBL.DeleteForever(noteId);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Note Deleted";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessfull";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, UpdateNoteRequestModel noteModel)
        {
            try
            {
                var result = await _noteBL.UpdateNote(id, noteModel);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Note Updated Successfully";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessfull";

                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet("toggleArchive/{id}")]
        public async Task<IActionResult> ToggleArchive(int id)
        {
            try
            {
                var result = await _noteBL.ToggleArchiveNote(id);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Successfull";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessfull";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet("togglePin/{id}")]
        public async Task<IActionResult> TogglePin(int id)
        {
            try
            {
                var result = await _noteBL.TogglePinNote(id);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Successfull";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessfull";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
        [HttpGet("toggleTrash/{id}")]
        public async Task<IActionResult> ToggleTrash(int id)
        {
            try
            {
                var result = await _noteBL.ToggleTrashNote(id);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Successfull";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessfull";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }
    }
}
