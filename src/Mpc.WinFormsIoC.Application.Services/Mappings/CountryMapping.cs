namespace Mpc.WinFormsIoC.Application.Services.Mappings
{
    using Mpc.WinFormsIoC.Application.Dto;
    using Mpc.WinFormsIoC.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;

    public static class CountryMapping
    {
        /// <summary>
        /// Convert IEnumerable of CountryModel to IEnumerable of CountryDto 
        /// </summary>
        public static IEnumerable<CountryDto> ToDto(this IEnumerable<CountryModel> coutries)
        {
            return coutries?.Select(ToDto);
        }

        /// <summary>
        /// Convert Model to Dto
        /// </summary>
        public static CountryDto ToDto(this CountryModel country)
        {
            return country == null ? null : new CountryDto
            {
                Id = country.Id,
                Name = country.Name
            };
        }

        /// <summary>
        /// Convert IEnumerable of CountryDto to IEnumerable of CountryModel 
        /// </summary>
        public static IEnumerable<CountryModel> ToModel(this IEnumerable<CountryDto> countries)
        {
            return countries?.Select(ToModel);
        }

        /// <summary>
        /// Convert Dto to Model
        /// </summary>
        public static CountryModel ToModel(this CountryDto country)
        {
            return country == null ? null : new CountryModel
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}
