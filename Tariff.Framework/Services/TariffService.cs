using Tariff.Framework.Services.Interface;
using System.Collections.Generic;
using Tariff.Framework.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;
using Tariff.Framework.Exceptions;
using Tariff.Framework.Validation;
using AutoMapper;

namespace Tariff.Framework.Services
{
    public class TariffService : ITariffService
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public TariffService(IMapper mapper, ILogger<TariffService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<TariffResult>> GetProducts(string consumption)
        {
            _logger.LogInformation("GetProducts", $"Consumption: {consumption}");

            int usage = await consumption.Validate();

            var products = new List<Product>
            {
                new ProductA(usage),
                new ProductB(usage)
            };

            _logger.LogInformation(nameof(GetProducts), "The result:{0}", products);

            var tariffResult = _mapper.Map<IEnumerable<TariffResult>>(products);

            return await Task.FromResult(tariffResult.OrderBy(c => c.AnnualCost));
            // Or products.Sort((x, y) => x.AnnualCost.CompareTo(y.AnnualCost));
        }
    }
}
