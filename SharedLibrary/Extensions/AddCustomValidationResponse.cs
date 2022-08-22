using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.DTOs;

namespace SharedLibrary.Extensions
{
    public static class AddCustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {

                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage).ToList();
                    ErrorDto errorDto = new ErrorDto(errors, true);
                    var response = ResponseDto<NoContentDto>.Fail(errorDto, 400);
                    return new BadRequestObjectResult(response)
                    {
                        StatusCode = response.StatusCode
                    };
                };
            });
        }
    }
}
