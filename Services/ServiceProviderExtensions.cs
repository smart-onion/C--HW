namespace hw4.Services
{
    public static class ServiceProviderExtensions
    {
        public static void AddDataService(this IServiceCollection services)
        {
            services.AddScoped<IDataService, DataCollectionService>();
        }
        public static void AddEFCoreService(this IServiceCollection services)
        {
            services.AddScoped<IDataService, EFCoreService>();
        }
    }
}
