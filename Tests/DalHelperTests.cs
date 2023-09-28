using System;
using Core.RepositoryBase;
using Core.RepositoryBase.Model;
using Xunit;

namespace Tests;

public class DalHelperTests
{
    [Fact]
    public void TestUpdatePart()
    {
        var actualPart = DalHelper.GetFieldPart(typeof(SampleDal));
        var expectedPart = " \"Name\" = @Name, \"Age\" = @Age ";
        Assert.Equal(expectedPart, actualPart);
    }
    
    private class SampleDal : DalModelBase<Guid>
    {
        public string Name { get; set; }
        
        public int Age { get; set; }
    }
}