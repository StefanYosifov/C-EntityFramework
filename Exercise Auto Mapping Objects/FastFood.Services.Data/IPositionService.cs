namespace FastFood.Services.Data
{
    using FastFood.Core.ViewModels.Positions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPositionService
    {

        Task CreateNewPositionAsync(CreatePositionInputModel model);


        Task<IEnumerable<PositionsAllViewModel>> GetAllAsync();

    }
}
