using Business.Validation.SendEmailValidation;
using Core.Helpers.Email;
using Core.Utilities.Results.Concrete.ErrorResult;
using Entities.DTOs.EmailDTOs;
using Entities.DTOs.PhoneNumberDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;
        public EmailController(IEmailService emailService, IConfiguration config)
        {
            _emailService = emailService;
            _config = config;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendCredit([FromBody]CreditDTO creditDTO)
        {
            var validation = new CreditValidation();
            var validationResult = await validation.ValidateAsync(creditDTO);
            if (!validationResult.IsValid)
                return BadRequest(new ErrorResult(message: validationResult.Errors.ToString(), statusCode: HttpStatusCode.BadRequest));
            var result = await _emailService.SendEmailAsync(_config["EmailSettings:Email"], _config["EmailSettings:Name"], "Kredit şərtlərini görmək istəyirəm", $"Ad Soyad: {creditDTO.FullName ?? "Daxil edilməyib"}\nƏlaqə Nömrəsi: {creditDTO.PhoneNumber}");
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendContact([FromBody]ContactDTO contactDTO)
        {
            var validation = new ContactValidation();
            var validationResult = await validation.ValidateAsync(contactDTO);
            if(!validationResult.IsValid) 
                return BadRequest(new ErrorResult(message: validationResult.Errors.ToString(),statusCode: HttpStatusCode.BadRequest));
            var result = await _emailService.SendEmailAsync(_config["EmailSettings:Email"], _config["EmailSettings:Name"], contactDTO.Subject, CreateContactBody(contactDTO));
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendPhoneNumber([FromBody] PhoneNumberDTO phoneNumberDTO)
        {
            var validation = new PhoneNumberValidation();
            var validationResult = await validation.ValidateAsync(phoneNumberDTO);
            if (!validationResult.IsValid)
                return BadRequest(new ErrorResult(message: validationResult.Errors.ToString(), statusCode: HttpStatusCode.BadRequest));
            var result = await _emailService.SendEmailAsync(_config["EmailSettings:Email"], _config["EmailSettings:Name"], "Zəng Sifarişi", $"Əlaqə Nömrəsi: {phoneNumberDTO.PhoneNumber}");
            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        private static string CreateContactBody(ContactDTO contactDTO)
        {
            return $"{contactDTO.Message}\n" +
                $"Ad Soyad: {contactDTO.FullName}\n" +
                $"Tel: {contactDTO.PhoneNumber}\n" +
                $"Elektron Poçt: {contactDTO.EmailAddress}";
        }
    }
}
