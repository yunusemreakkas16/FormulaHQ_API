using FormulaHQ.API.Models;

namespace FormulaHQ.API.Repositories
{
    public interface ISponsorRepository
    {
        Task<SponsorResponseModel> GetSponsorByID(Guid sponsorID);
        Task<SponsorListResponseModel> GetAllSponsors();
        Task<SponsorResponseModel> AddSponsor(Sponsor sponsor);
        Task<SponsorResponseModel> UpdateSponsor(Sponsor sponsor);
    }
}
