using AutoMapper;
using CommandService.Models;
using CommandService.Protos;
using Grpc.Net.Client;

namespace CommandService.SyncDataServices.Grpc
{
    public class PlatformDataClient : IPlatformDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlatformDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Platform> ReturnAllPlatforms()
        {
            var grpcPlatormUrl = _configuration["GrpcPlatform"];
            Console.WriteLine($"--> Calling GRPC Service {grpcPlatormUrl}");

            var channel = GrpcChannel.ForAddress(grpcPlatormUrl);
            var client = new GrpcPlatform.GrpcPlatformClient(channel);
            var request = new GetAllRequest();

            try
            {
                var reply = client.GetAllPlatforms(request);
                
                return _mapper.Map<IEnumerable<Platform>>(reply.Platform);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not call GRPC Server {ex.Message}");

                return null;
            }
        }
    }
}
