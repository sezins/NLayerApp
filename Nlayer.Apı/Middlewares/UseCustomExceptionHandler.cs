namespace Nlayer.Apı.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UserCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context => 
                {
                    context.Response.ContentType = "applicaiton/json";
                })
            })
        }
    }
}
