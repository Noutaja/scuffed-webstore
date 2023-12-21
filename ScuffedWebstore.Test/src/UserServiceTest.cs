using AutoMapper;
using ScuffedWebstore.Service.src.Shared;
using Xunit;

namespace ScuffedWebstore.Test.src;
public class UserServiceTest
{
    private static IMapper _mapper;

    public UserServiceTest()
    {
        if (_mapper == null)
        {
            MapperConfiguration mappingConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MapperProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }
    }



    [Fact]
    public void Test1()
    {
        Assert.True(true);
    }
}