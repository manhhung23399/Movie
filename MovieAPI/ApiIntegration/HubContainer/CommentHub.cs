using Microsoft.AspNetCore.SignalR;
using Movie.Core.Interfaces;

namespace Movie.ApiIntegration.HubContainer
{
    public class CommentHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
