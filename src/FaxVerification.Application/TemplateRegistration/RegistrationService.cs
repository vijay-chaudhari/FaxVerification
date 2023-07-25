using FaxVerification.Configuration;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FaxVerification.TemplateRegistration
{
    public class RegistrationService : CrudAppService<
        Registration,
        RegistrationDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateRegistrationDto>,
        IRegistrationService
    {
        private readonly IConfiguration _configuration;

        public RegistrationService(IRepository<Registration, Guid> repository, IConfiguration configuration) : base(repository)
        {
            _configuration = configuration;
        }

        public async Task MoveFiletoFolder(string FileName)
        {
            try
            {
                string Destination = _configuration.GetValue<string>("PendingRegistrationFile:Destination");
                var Souce = _configuration.GetValue<string>("PendingRegistrationFile:Source");

                string sourceFile = Souce+ "\\" + FileName;
                string destinationFile = Destination + "\\"+ FileName;
                File.Copy(sourceFile, destinationFile);
            }
            catch (IOException iox)
            {
                Console.WriteLine(iox.Message);
            }
        }

        public async Task<Registration> GetConfigByVendorNo(string VendorNo)
        {
            var query = await Repository.GetQueryableAsync();
            try
            {
                List<Registration> configurationList = query.Where(x => x.VendorNo == VendorNo).ToList();
                return configurationList.FirstOrDefault();
            }
            catch(Exception ex)
            {

            }
            Registration E = new Registration();

            return E;

        }


    }
}
