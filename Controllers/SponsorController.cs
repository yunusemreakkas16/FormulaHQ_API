using FormulaHQ.API.Models;
using FormulaHQ.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormulaHQ.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorRepository sponsorRepository;

        public SponsorController(ISponsorRepository sponsorRepository)
        {
            this.sponsorRepository = sponsorRepository;
        }

        [HttpPost]
        [Route("AddSponsor")]
        public async Task<ActionResult<SponsorResponseModel>> CreatePost([FromBody] Sponsor sponsor)
        {
            if (sponsor == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });

            var SponsorResponseModel = await sponsorRepository.AddSponsor(sponsor);

            if (SponsorResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = SponsorResponseModel.Message });

            if (SponsorResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = SponsorResponseModel.Message });

            return Ok(new { MessageId = SponsorResponseModel.MessageID, MessageDescription = SponsorResponseModel.Message, User = SponsorResponseModel.Sponsor });
        }

        [HttpPost]
        [Route("SponsorList")]
        public async Task<ActionResult<SponsorListResponseModel>> GetAllSponsors()
        {
            var sponsorListResponseModel = await sponsorRepository.GetAllSponsors();
            if (sponsorListResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = sponsorListResponseModel.Message });
            if (sponsorListResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = sponsorListResponseModel.Message });
            return Ok(new { MessageId = sponsorListResponseModel.MessageID, MessageDescription = sponsorListResponseModel.Message, Users = sponsorListResponseModel.Sponsors });
        }

        [HttpPost]
        [Route("SponsorDetail")]
        public async Task<ActionResult<SponsorResponseModel>> GetSponsorByID([FromBody] SponsorParamModel sponsorParamModel)
        {
            if (sponsorParamModel == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var sponsorResponseModel = await sponsorRepository.GetSponsorByID(sponsorParamModel.SponsorID);
            if (sponsorResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = sponsorResponseModel.Message });
            if (sponsorResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = sponsorResponseModel.Message });
            return Ok(new { MessageId = sponsorResponseModel.MessageID, MessageDescription = sponsorResponseModel.Message, User = sponsorResponseModel.Sponsor });
        }

        [HttpPost]
        [Route("UpdateSponsor")]
        public async Task<ActionResult<SponsorResponseModel>> UpdateSponsor([FromBody] Sponsor sponsor)
        {
            if (sponsor == null)
                return BadRequest(new { MessageId = -2, MessageDescription = "Post data is required." });
            var sponsorResponseModel = await sponsorRepository.UpdateSponsor(sponsor);
            if (sponsorResponseModel.MessageID == -99)
                return StatusCode(500, new { MessageId = -99, MessageDescription = sponsorResponseModel.Message });
            if (sponsorResponseModel.MessageID == -100)
                return StatusCode(500, new { MessageId = -100, MessageDescription = sponsorResponseModel.Message });
            return Ok(new { MessageId = sponsorResponseModel.MessageID, MessageDescription = sponsorResponseModel.Message, User = sponsorResponseModel.Sponsor });
        }

    }
}
