namespace CaseStudyApi.Presentation.MiddleWares
{
    public class ExceptionHandlingMiddleWare
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string ErrorMessage = $"An error has accoured, error mesage: {ex.Message}";

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(new
                {
                    Title = "Server error",
                    Status = context.Response.StatusCode,
                    Message = ErrorMessage
                });
            }
        }
    }
}
