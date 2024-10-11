using Autopood.Domain;
using Autopood.Dto;
using System.Numerics;

namespace Autopood.ServiceInterface
{
    public interface ICarsServices
    {
        Task<Car> Create(CarDto dto);
        Task<Car> Update(CarDto dto);
        Task<Car> Delete(Guid id);
        Task<Car> GetAsync(Guid id);
    }
}
