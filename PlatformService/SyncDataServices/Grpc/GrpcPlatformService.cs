using AutoMapper;
using Grpc.Core;
using PlatformService.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.SyncDataServices.Grpc
{
    /// <summary>
    /// GrpcPlatform actually referenced from platforms.proto
    /// GrpcPlatformBase is auto generated , you must build to get it
    /// </summary>
    public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
    {
        private readonly IPlatformRepo _platformRepo;
        private readonly IMapper _mapper;

        public GrpcPlatformService(IPlatformRepo platformRepo, IMapper mapper)
        {
            _platformRepo = platformRepo;
            _mapper = mapper;
        }

        public override Task<PlatformResponse> GetAllPlatforms(GetAllRequest request,ServerCallContext context)
        {
            var response = new PlatformResponse();
            var platforms = _platformRepo.GetAllPlatforms();

            foreach(var platform in platforms)
            {
                response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform));
            }

            return Task.FromResult(response);
        }

    }
}
