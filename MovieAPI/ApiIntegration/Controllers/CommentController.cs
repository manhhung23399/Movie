using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie.Core.Entities;
using Movie.Core.Interfaces;
using Movie.Core.Resources.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movie.ApiIntegration.Controllers
{
    [Route("api/v1/comment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize()]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> Get([FromRoute]string movieId)
        {
            var comments = await _unitOfWork.Comment.GetCommentAsync(movieId);
            var accounts = await _unitOfWork.Account.GetAccountsAsync(movieId);
            var response = comments.ToList().Join(
                accounts, 
                p => p.Uid, 
                c => c.Id, 
                (p, c) => new CommentResponse
            {
                key = p.Key,
                likes = p.Likes,
                message = p.Message,
                photo_url = c.PhotoUrl,
                reply_comment = p.ReplyComment,
                user_name = c.DisplayName,
                when = p.When
            }).ToList();
            return Ok(ResponseBase.Success(response));
        }
        [HttpPost]
        public async Task<ActionResult<Comment>> Post([FromBody]Comment comment, [FromRoute]string movieId)
        {
            var comments = await _unitOfWork.Comment.AddCommentAsync(comment, movieId);
            return Ok(ResponseBase.Success(comments));
        }
    }
}
