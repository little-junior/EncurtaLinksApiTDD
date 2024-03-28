using EncurtaLinks.Core;

namespace EncurtaLinks.API.Services
{
    public static class BuilderApplicator
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped(typeof(IEncurtaLinksService), typeof(EncurtaLinksService));
            builder.Services.AddScoped(typeof(IRandomizer), typeof(Randomizer));
        }
    }
}
