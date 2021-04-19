using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Pluto.Rover.Commands
{
    public class ChangeDirectionCommandHadler: IRequestHandler<ChangeDirectionCommandQueue>
    {
        public ChangeDirectionCommandHadler()
        {
            
        }
        public Task<Unit> Handle(ChangeDirectionCommand request, CancellationToken cancellationToken)
        {
            
            //TODO change this
            return Task.FromResult(Unit.Value);
        }
    }
}