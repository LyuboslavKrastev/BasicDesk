using AutoMapper;
using BasicDesk.App.AutoMapping;

namespace BasicDesk.Tests.Utilities
{
    public static class TestsAutoMapper
    {
        static TestsAutoMapper()
        {
            Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
        }

        public static IMapper GetMapper()
        {
            return Mapper.Instance;
        }
    }
}
