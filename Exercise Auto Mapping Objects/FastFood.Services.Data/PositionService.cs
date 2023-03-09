namespace FastFood.Services.Data
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using FastFood.Core.ViewModels.Positions;
    using FastFood.Data;
    using FastFood.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class PositionService : IPositionService
    {
        private readonly IPositionService positionService;

        public PositionService(IPositionService positionService)
        {
            this.positionService = positionService;
        }

        public async Task CreateNewPositionAsync(CreatePositionInputModel model)
        {
            Position positions=this.mapper.Map<Position>(model);

            await context.Positions.AddAsync(positions);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PositionsAllViewModel>> GetAllAsync()
        {
            return await this.context.Positions
                .ProjectTo<PositionsAllViewModel>(this.mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}
