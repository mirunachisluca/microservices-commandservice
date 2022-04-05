using AutoMapper;
using CommandService.Data;
using CommandService.DTOs;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepository _commandRepository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepository commandRepository, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> [HttpGet] Getting commands for platform with id {platformId}");

            if (!_commandRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var commands = _commandRepository.GetAllCommandsForPlatform(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{commandId}", Name = nameof(GetCommandForPlatform))]
        public ActionResult<CommandReadDTO> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> [HttpGet] Getting command with id {commandId} for platform with id {platformId}");

            if (!_commandRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _commandRepository.GetCommandForPlatform(platformId, commandId);

            if (command != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(command));
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> InsertCommandForPlatform(int platformId, CommandCreateDTO commandDTO)
        {
            Console.WriteLine($"--> [HttpPost] Creating command for platform with id {platformId}");

            if (!_commandRepository.PlatformExists(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(commandDTO);

            _commandRepository.CreateCommand(platformId, command);
            _commandRepository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(command);

            return CreatedAtAction(nameof(GetCommandForPlatform), 
                new { platformId, commandId = commandReadDTO.Id }, 
                commandReadDTO);
        }

    }
}
