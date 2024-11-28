using Autopood.Domain;
using Autopood.Dto;

namespace Autopood.ServiceInterface
{
    public interface IPlanesServices
    {
        Task<Plane> Create(PlaneDto dto);
        Task<Plane> Update(PlaneDto dto);
        Task<Plane> Delete(Guid id);
        Task<Plane> GetAsync(Guid id);
    }
}
