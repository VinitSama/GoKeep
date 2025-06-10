using GoKeep.Business;
using GoKeep.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoKeep_june.Contollers
{
    [Authorize]
    [Route("GoKeep/noteLabel")]
    [ApiController]
    public class NoteLabelController : ControllerBase
    {
        private readonly INoteLableBL _notelabelBL;

        public NoteLabelController(INoteLableBL notelabelBL)
        {
            _notelabelBL = notelabelBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNoteLabel()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var result = await _notelabelBL.GetNoteLable(email);
                var response = new GenericResponseModel<List<GetNotesLabelResponseModel>>(result);
                if (result.Any())
                {
                    response.ResponseCode = 200;
                    response.Description = $"All notes-label of email: {email}";
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Description = "No notes-label found!!";
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
        public async Task<IActionResult> AddNoteLabel(NoteLabelRequestModel notelabelModel)
        {
            try
            {
                var result = await _notelabelBL.AddNoteLabel(notelabelModel);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 201;
                    response.Description = "Note-label Created Successfully";
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

        [HttpDelete("{noteId}/{labelId}")]
        public async Task<IActionResult> DeleteNoteLabel(int noteId, int labelId)
        {
            try
            {
                var notelabelModel = new NoteLabelRequestModel()
                {
                    NoteId = noteId,
                    LabelId = labelId
                };
                var result = await _notelabelBL.DeleteNoteLabel(notelabelModel);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Note-label Deleted";
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
        [HttpGet("noteid/{noteId}")]
        public async Task<IActionResult> GetLabelIdByNoteId(int noteId)
        {
            try
            {
                var result = await _notelabelBL.GetLabelIdByNoteIds(noteId);
                var response = new GenericResponseModel<List<int>>(new List<int>());
                if (result.Any())
                {
                    response.ResponseCode = 200;
                    response.Description = "All notes-label of note";
                    foreach (var label in result)
                    {
                        response.Data.Add(label.LabelId);
                    }
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Description = "No notes-label found!!";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpGet("labelId/{labelId}")]
        public async Task<IActionResult> GetNoteIdByLabelId(int labelId)
        {
            try
            {
                var result = await _notelabelBL.GetNoteIdByLabelId(labelId);
                var response = new GenericResponseModel<List<int>>(new List<int>());
                if (result.Any())
                {
                    response.ResponseCode = 200;
                    response.Description = "All notes-label of note";
                    foreach (var note in result)
                    {
                        response.Data.Add(note.NoteId);
                    }
                }
                else
                {
                    response.ResponseCode = 404;
                    response.Description = "No notes-label found!!";
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
