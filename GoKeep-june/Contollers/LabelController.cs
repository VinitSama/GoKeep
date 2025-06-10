using GoKeep.Business;
using GoKeep.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoKeep_june.Contollers
{
    [Authorize]
    [Route("GoKeep/label")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL _labelBL;
        public LabelController(ILabelBL labelBL)
        {
            _labelBL = labelBL;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLabel()
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var result = await _labelBL.GetAllLabel(email);
                var response = new GenericResponseModel<List<GetAllLabelResponseModel>>(result);
                if (result.Any())
                {
                    response.ResponseCode = 200;
                    response.Description = $"All Labels of Email: {email}";
                }
                else
                {
                    response.ResponseCode = 500;
                    response.Description = "Unsuccessful Attempt";
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }

        [HttpDelete("{labelId}")]
        public async Task<IActionResult> DeleteLabel(int labelId)
        {
            try
            {
                var result = await _labelBL.DeleteLabel(labelId);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Label Deleted";
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

        [HttpPost]
        public async Task<IActionResult> CreateNewLabel(CreateLabelRequestModel labelModel)
        {
            try
            {
                var email = User.FindFirstValue(ClaimTypes.Email);
                var result = await _labelBL.CreateNewLabel(email, labelModel);
                var response = new GenericResponseModel<int>(result);
                if (result > -1)
                {
                    response.ResponseCode = 201;
                    response.Description = "Label Created Successfully";
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
        [HttpPut]
        public async Task<IActionResult> UpdateLabel(UpdateLabelRequestModel labelModel)
        {
            Console.WriteLine($"{labelModel.Id.ToString()}, {labelModel.Name}");
            try
            {
                var result = await _labelBL.UpdateLabel(labelModel);
                var response = new GenericResponseModel();
                if (result)
                {
                    response.ResponseCode = 204;
                    response.Description = "Label Updated Successfully";
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
    }
}
